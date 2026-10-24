<template>
  <div class="table-responsive">
    <table ref="table" class="u-full-width">
      <thead>
        <tr>
          <th></th>
          <slot name="thead"></slot>
          <th>
            <button
              type="button"
              class="btn-action"
              title="remove selections"
              @click="removeSelected()"
            >
              <Icon icon="close"></Icon>
            </button>
          </th>
        </tr>
      </thead>
      <tbody ref="tbody">
        <tr
          v-for="(row, idx) in rows"
          :key="row.id || idx"
          :data-id="row.id || 0"
        >
          <td>
            <button
              type="button"
              class="draggable"
              title="drag this row"
              @mousedown="mouseDown"
            >
              <Icon icon="drag"></Icon>
            </button>
          </td>
          <td v-for="(col, idx1) in cols" :key="idx1">
            {{ getText(col, idx) }}
          </td>
          <td>
            <input type="checkbox" :data-id="row.id" :value="row.id" />
          </td>
        </tr>
      </tbody>
    </table>
    <div v-if="rows.length == 0" class="no-record">No records</div>
  </div>
</template>

<script lang="ts">
import { defineComponent, PropType } from "vue";
import Icon from "./VIcon.vue";

export default defineComponent({
  name: "VSortableTable",
  components: {
    Icon,
  },
  props: {
    id: {
      type: Number,
      default: 0,
    },
    cols: {
      type: Array as PropType<string[]>,
      required: true,
      default: () => [],
    },
    rows: {
      type: Array as PropType<any[]>, // eslint-disable-line
      required: true,
      default: () => [],
    },
  },
  emits: ["complete", "remove"],
  data() {
    return {
      currRow: {} as HTMLElement,
      dragElem: {} as HTMLElement,
      mouseDownX: 0,
      mouseDownY: 0,
      mouseX: 0,
      mouseY: 0,
      mouseDrag: false,
    };
  },
  mounted() {
    this.$nextTick(() => {
      //document.addEventListener("mousedown", this.mouseDown);
      document.addEventListener("mousemove", this.mouseMove);
      document.addEventListener("mouseup", this.mouseUp);
    });
  },
  beforeUnmount() {
    //document.removeEventListener("mousedown", this.mouseDown);
    document.removeEventListener("mousemove", this.mouseMove);
    document.removeEventListener("mouseup", this.mouseUp);
  },
  methods: {
    getText(name: string, idx: number) {
      const d = this.rows[idx] as unknown as { [key: string]: string | number };
      const t = d[name];
      if (typeof t === "number") {
        return t.toString();
      }
      return t;
    },
    getLink(idx: number) {
      return `/equipmentdetail/${this.rows[idx]["equipmentDefinitionId"]}`;
    },
    saveComment(comment: string, id: number) {
      console.log(comment, id);
      // this.$http.equipmentInstance
      //   .saveComment(id, comment)
      //   .then(() => {
      //     this.$toast.success("comment saved successfully");
      //   })
      //   .catch((err) => this.$toast.error(err.toString()));
    },
    mouseDown(e: MouseEvent) {
      if (e.button != 0) return true;

      const target = this.getTargetRow(e.target as HTMLElement);
      if (target) {
        this.currRow = target;
        this.addDraggableRow(target);
        this.currRow.classList.add("is-dragging");

        const coords = this.getMouseCoords(e);
        this.mouseDownX = coords.x;
        this.mouseDownY = coords.y;

        this.mouseDrag = true;
      }
    },
    mouseMove(e: MouseEvent) {
      if (!this.mouseDrag) return;

      const coords = this.getMouseCoords(e);
      this.mouseX = coords.x - this.mouseDownX;
      this.mouseY = coords.y - this.mouseDownY;

      this.moveRow(this.mouseX, this.mouseY);
    },
    mouseUp() {
      if (!this.mouseDrag) return;

      if (this.currRow) this.currRow.classList.remove("is-dragging");
      if (this.dragElem)
        (this.$refs.table as HTMLTableElement).removeChild(this.dragElem);

      this.dragElem = {} as HTMLElement;
      this.mouseDrag = false;

      this.reindexData();
    },
    swapRow(row: HTMLElement, index: number) {
      const tbody = this.$refs.tbody as HTMLTableElement;
      if (this.currRow) {
        const currIndex = Array.from(tbody.children).indexOf(this.currRow),
          row1 = currIndex > index ? this.currRow : row,
          row2 = currIndex > index ? row : this.currRow;

        tbody.insertBefore(row1, row2);
      }
    },
    moveRow(x: number, y: number) {
      this.dragElem.style.transform =
        "translate3d(" + x + "px, " + y + "px, 0)";

      const dPos = this.dragElem.getBoundingClientRect(),
        currStartY = dPos.y,
        currEndY = currStartY + dPos.height,
        rows = this.getRows();

      for (var i = 0; i < rows.length; i++) {
        const rowElem = rows[i] as HTMLElement,
          rowSize = rowElem.getBoundingClientRect(),
          rowStartY = rowSize.y,
          rowEndY = rowStartY + rowSize.height;

        if (
          this.currRow !== rowElem &&
          this.isIntersecting(currStartY, currEndY, rowStartY, rowEndY)
        ) {
          if (Math.abs(currStartY - rowStartY) < rowSize.height / 2)
            this.swapRow(rowElem, i);
        }
      }
    },
    addDraggableRow(target: HTMLElement) {
      this.dragElem = target.cloneNode(true) as HTMLElement;
      this.dragElem.classList.add("draggable-table__drag");
      this.dragElem.style.height = this.getStyle(target, "height");
      this.dragElem.style.background = this.getStyle(target, "backgroundColor");
      for (var i = 0; i < target.children.length; i++) {
        const oldTD = target.children[i] as HTMLElement;
        const newTD = this.dragElem.children[i] as HTMLElement;
        newTD.style.width = this.getStyle(oldTD, "width");
        newTD.style.height = this.getStyle(oldTD, "height");
        newTD.style.padding = this.getStyle(oldTD, "padding");
        newTD.style.margin = this.getStyle(oldTD, "margin");
      }

      (this.$refs.table as HTMLTableElement).appendChild(this.dragElem);

      const tPos = target.getBoundingClientRect(),
        dPos = this.dragElem.getBoundingClientRect();
      this.dragElem.style.bottom = dPos.y - tPos.y - tPos.height + "px";
      this.dragElem.style.left = "-1px";

      document.dispatchEvent(
        new MouseEvent("mousemove", {
          view: window,
          cancelable: true,
          bubbles: true,
        })
      );
    },
    getRows() {
      return (this.$refs.table as HTMLElement).querySelectorAll("tbody tr");
    },
    getTargetRow(target: HTMLElement | null) {
      if (!target) return;
      let elemName = target.tagName.toLowerCase();

      if (elemName == "tr") return target;
      return target.closest("tr");
    },
    getMouseCoords(event: MouseEvent) {
      return {
        x: event.clientX,
        y: event.clientY,
      };
    },
    getStyle(target: HTMLElement, styleName: string) {
      const compStyle = getComputedStyle(target) as unknown as {
          [key: string]: string;
        },
        style = compStyle[styleName];

      return style ? style : "";
    },
    isIntersecting(min0: number, max0: number, min1: number, max1: number) {
      return (
        Math.max(min0, max0) >= Math.min(min1, max1) &&
        Math.min(min0, max0) <= Math.max(min1, max1)
      );
    },
    reindexData() {
      // copy data to avoid mutation prop,
      let data = JSON.parse(JSON.stringify(this.rows)) as any[]; // eslint-disable-line
      const tablerows = (this.$refs.tbody as HTMLTableElement).children;
      for (let i = 0; i < tablerows.length; i += 1) {
        const id = parseInt(tablerows[i].getAttribute("data-id") || "0", 10);
        data.find((x) => x.id == id).order = i;
      }

      data = data.sort((a, b) => (a.order > b.order ? 1 : -1));
      this.$emit("complete", data);
    },
    removeSelected() {
      const ids = new Array<number>();
      const tb = this.$refs.tbody as HTMLTableElement;
      const selected = tb.querySelectorAll("input[data-id]:checked");
      selected.forEach((x) => {
        const id = x.getAttribute("data-id");
        if (id) ids.push(parseInt(id, 10));
      });

      this.$emit("remove", ids);
    },
  },
});
</script>

<style lang="scss" scoped>
table {
  position: relative;

  thead tr th {
    text-transform: capitalize;
    user-select: none;
  }

  tbody {
    tr {
      &:nth-child(even) {
        background-color: var(--color--white);
      }

      &:nth-child(odd) {
        background-color: var(--color--gray1);
      }

      td {
        color: var(--color--black);
      }
    }
  }
}

.is-dragging {
  background: var(--color--primary);
}

.btn-action {
  color: var(--color--primary);
  background: transparent;
  border: 1px solid;
  border-radius: 50%;
  line-height: 0;
  height: 28px;
  width: 30px;
  cursor: pointer;

  &:hover {
    color: var(--color--white);
    background: var(--color--black);
  }
}

.draggable {
  cursor: move;
  border: none;
  border-radius: 50%;
  line-height: 0;
  padding: 2px;

  &:hover {
    background: var(--color--gray2);
  }
}

.draggable-table__drag {
  position: absolute;
  width: 100%;
  border: 1px solid var(--color--primary);
  font-size: 1rem;
  cursor: grabbing;
  -webkit-box-shadow: 2px 2px 3px 0px rgba(0, 0, 0, 0.05);
  box-shadow: 2px 2px 3px 0px rgba(0, 0, 0, 0.05);
  opacity: 1;
  z-index: 10;
}

.no-record {
  height: 45px;
  width: 100%;
  text-align: left;
  padding: 10px 2px;
  background-color: var(--color--white);
}
</style>
