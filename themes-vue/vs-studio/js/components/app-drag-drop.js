import DND from "../lib/drag-drop-table.js";

export default {
  name: "app-drag-drop",
  template: `
<div class="app--drag-drop">
  <div class="drop-zone"
    @drop="drop"
    @dragover="dragOver"
    @dragleave="dragLeave"
    data-position="0">
  </div>

  <template v-for="(item, idx) in accordions" :key="idx">
    <div :id="item.id" class="drag-drop--panel">
      <div class="drag-drop--panel-header" @click="toggle(item.id)">
        <div class="drag-drop--panel-title"> {{ item.title }} </div>
        <div>
          <button
            type="button"
            title="drag this panel"
            draggable="true"
            @dragstart="dragStart"
            :data-position="item.order"  
          >---</button>
        </div>
      </div>
      <div class="drag-drop--panel-body" :class="{'open': item.expand}">
        <table :id="'table' + item.id" class="draggable-table">
          <thead>
            <tr>
              <th></th>
              <th>Name</th>
            </tr>
          </thead>
          <tbody>
            <tr v-for="row in rows" :key="item.id" :data-position="row.order">
              <td>
                <button
                  type="button"
                  title="drag this row"
                >---</button>
              </td>
              <td> {{ row.name }} </td>
            </tr>
          </tbody>
        </table>
      </div>
    </div>

    <div class="drop-zone"
      @drop="drop"
      @dragover="dragOver"
      @dragleave="dragLeave"
      :data-position="idx + 1">
    </div>
  </template>
</div>
`,
  data() {
    return {
      accordions: [
        { id: 11, title: "Panel 1", order: 0, expand: false },
        { id: 12, title: "Panel 2", order: 1, expand: false },
        { id: 13, title: "Panel 3", order: 2, expand: false },
      ],
      rows: [
        { id: 20, name: "Johnny Blaze", order: 0 },
        { id: 21, name: "Tony Stark", order: 1 },
        { id: 22, name: "Thor", order: 3 },
      ],
    };
  },
  mounted() {
    this.$nextTick(() => {
      DND.init();
    });
  },
  methods: {
    toggle(id) {
      // == allow accordion to open/close
      // const acc = this.accordions.find((x) => x.id === id);
      // if (acc) {
      //   acc.expand = !acc.expand;
      // }

      // == ONLY allow one accordion open at a time
      this.accordions.forEach((acc) => {
        if (acc.id === id) {
          acc.expand = !acc.expand;
        } else acc.expand = false;
      });
    },
    sortAccordions() {
      this.accordions = this.accordions.sort((a, b) =>
        a.order > b.order ? 1 : -1
      );
    },
    drop(e) {
      e.preventDefault();
      console.log(e);
      const data = parseInt(e.dataTransfer.getData("text"), 10);
      const idx = parseInt(e.target.getAttribute("data-position"), 10);
      // do we have a valid move?
      if (data !== idx && data + 1 !== idx) {
        //console.log(`FROM: ${data} TO: ${idx}`);
        const acc = this.accordions.find((x) => x.order === data);
        if (acc) {
          let startIdx = 0;
          // moving UP!
          if (acc.order > idx) {
            acc.order = idx;
            startIdx = idx;
          }
          // moving DOWN!
          if (acc.order < idx) {
            acc.order = idx - 1;
            startIdx = 0;
          }

          // sort here to group 'order' field
          this.sortAccordions();

          for (let i = startIdx; i < this.accordions.length; i += 1) {
            this.accordions[i].order = i;
          }
        }

        // final sorting
        this.sortAccordions();
      }

      // Cleanup ALL drop-zones
      document.querySelectorAll(".drop-zone").forEach((x) => {
        x.classList.remove("active");
        x.style.height = "0px";
      });
    },
    dragStart(e) {
      e.dataTransfer.setData("text", e.target.getAttribute("data-position"));
      document
        .querySelectorAll(".drop-zone")
        .forEach((x) => x.classList.add("active"));
    },
    dragOver(e) {
      e.preventDefault();
      e.target.style.height = "42px";
    },
    dragLeave(e) {
      e.preventDefault();
      e.target.style.height = "0px";
    },
  },
};
