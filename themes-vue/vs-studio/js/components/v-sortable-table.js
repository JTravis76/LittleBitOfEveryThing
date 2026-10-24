export default {
  name: "v-sortable-table",
  template: `
<table ref="table" class="sortable-table">
  <thead>
    <tr>
      <th></th>
      <slot name="thead"></slot>
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
          =
        </button>
      </td>
      <td v-for="(col, idx1) in cols" :key="idx1">
        <RouterLink
          v-if="idx1 === 0"
          :to="getLink(idx)"
          class="btn-link"
          >{{ getText(col, idx) }}</RouterLink
        >
        <label v-else>{{ getText(col, idx) }}</label>
      </td>
    </tr>
  </tbody>
</table>
<div v-if="rows.length == 0" class="no-record">No records</div>
  `,
  props: {
    cols: {
      type: Array, //  as PropType<string[]>,
      required: true,
      default: () => [],
    },
    rows: {
      type: Array, //  as PropType<any[]>, // eslint-disable-line
      required: true,
      default: () => [],
    },
  },
  emits: ["complete", "remove"],
  data() {
    return {
      currRow: {}, // as HTMLElement,
      dragElem: {}, // as HTMLElement,
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
    getText(name, idx) {
      return this.rows[idx][name];
    },
    getLink(idx) {
      return `/someroute/${this.rows[idx]}`;
    },
    mouseDown(e) {
      if (e.button != 0) return true;

      const target = this.getTargetRow(e.target);
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
    mouseMove(e) {
      if (!this.mouseDrag) return;

      const coords = this.getMouseCoords(e);
      this.mouseX = coords.x - this.mouseDownX;
      this.mouseY = coords.y - this.mouseDownY;

      this.moveRow(this.mouseX, this.mouseY);
    },
    mouseUp() {
      if (!this.mouseDrag) return;

      if (this.currRow) this.currRow.classList.remove("is-dragging");
      if (this.dragElem) this.$refs.table.removeChild(this.dragElem);

      this.dragElem = {};
      this.mouseDrag = false;

      this.reindexData();
    },
    swapRow(row, index) {
      const tbody = this.$refs.tbody;
      if (this.currRow) {
        const currIndex = Array.from(tbody.children).indexOf(this.currRow),
          row1 = currIndex > index ? this.currRow : row,
          row2 = currIndex > index ? row : this.currRow;

        tbody.insertBefore(row1, row2);
      }
    },
    moveRow(x, y) {
      this.dragElem.style.transform =
        "translate3d(" + x + "px, " + y + "px, 0)";

      const dPos = this.dragElem.getBoundingClientRect(),
        currStartY = dPos.y,
        currEndY = currStartY + dPos.height,
        rows = this.getRows();

      for (var i = 0; i < rows.length; i++) {
        const rowElem = rows[i],
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
    addDraggableRow(target) {
      this.dragElem = target.cloneNode(true);
      this.dragElem.classList.add("draggable-table__drag");
      this.dragElem.style.height = this.getStyle(target, "height");
      this.dragElem.style.background = this.getStyle(target, "backgroundColor");
      for (var i = 0; i < target.children.length; i++) {
        const oldTD = target.children[i];
        const newTD = this.dragElem.children[i];
        newTD.style.width = this.getStyle(oldTD, "width");
        newTD.style.height = this.getStyle(oldTD, "height");
        newTD.style.padding = this.getStyle(oldTD, "padding");
        newTD.style.margin = this.getStyle(oldTD, "margin");
      }

      this.$refs.table.appendChild(this.dragElem);

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
      return this.$refs.table.querySelectorAll("tbody tr");
    },
    getTargetRow(target) {
      if (!target) return;
      let elemName = target.tagName.toLowerCase();

      if (elemName == "tr") return target;
      return target.closest("tr");
    },
    getMouseCoords(event) {
      return {
        x: event.clientX,
        y: event.clientY,
      };
    },
    getStyle(target, styleName) {
      const compStyle = getComputedStyle(target);
      const style = compStyle[styleName];

      return style ? style : "";
    },
    isIntersecting(min0, max0, min1, max1) {
      return (
        Math.max(min0, max0) >= Math.min(min1, max1) &&
        Math.min(min0, max0) <= Math.max(min1, max1)
      );
    },
    reindexData() {
      // copy data to avoid mutation prop,
      let data = JSON.parse(JSON.stringify(this.rows));
      const tablerows = this.$refs.tbody.children;
      for (let i = 0; i < tablerows.length; i += 1) {
        const id = parseInt(tablerows[i].getAttribute("data-id") || "0", 10);
        data.find((x) => x.id == id).order = i;
      }

      data = data.sort((a, b) => (a.order > b.order ? 1 : -1));
      this.$emit("complete", data);
    },
  },
};
