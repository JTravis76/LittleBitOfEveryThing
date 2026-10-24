import Accordion from "../components/v-accordion.js";

export default {
  template: `
<div class="row">
  <div class="col-md-8">
    <div
      v-if="draggable"
      class="drop-zone"
      data-order="0"
      @drop="drop"
      @dragover="dragOver"
      @dragleave="dragLeave"
    ></div>
    <template v-for="(acc, idx) in accordions" :key="acc.id">
      <Accordion
        :id="acc.id"
        :title="acc.title"
        :expanded="acc.expanded"
        :draggable="draggable"
        :order="acc.order"
        @toggle="toggle($event, acc.id)"
        @drag-start="dragStart"
      >
        <template v-if="acc.id === 10">{{ acc.title }}</template>
        <template v-if="acc.id === 20">{{ acc.title }}</template>
        <template v-if="acc.id === 30">{{ acc.title }}</template>
        <template v-if="acc.id === 40">{{ acc.title }}</template>
      </Accordion>

      <div
        v-if="draggable"
        class="drop-zone"
        :data-order="idx + 1"
        @drop="drop"
        @dragover="dragOver"
        @dragleave="dragLeave"
      ></div>
    </template>
  </div>
  <div class="col-md-4">
    <fieldset>
      <legend>Info</legend>
      This story shows how to create a group of accordion panels with an
      optional sortable order.
    </fieldset>
    <fieldset>
      <legend>Interactive States</legend>
      <input v-model="draggable" type="checkbox" /> Draggable
      <input v-model="single" type="checkbox" /> Single/Multiple
    </fieldset>
  </div>
</div>
`,
  name: "accordion-group-story",
  components: {
    Accordion,
  },
  data() {
    return {
      draggable: true,
      single: true,
      accordions: [
        { id: 10, title: "Panel 1", expanded: false, order: 1 },
        { id: 20, title: "Panel 2", expanded: false, order: 2 },
        { id: 30, title: "Panel 3", expanded: false, order: 3 },
        { id: 40, title: "Panel 4", expanded: false, order: 4 },
      ],
    };
  },
  methods: {
    toggle(state, id) {
      if (this.single) {
        const acc = this.accordions.find((x) => x.id === id);
        if (acc) {
          acc.expanded = state;
        }
      } else {
        this.accordions.forEach((x) => {
          x.expanded = false;
        });
      }
    },
    sortAccordions() {
      this.accordions = this.accordions.sort((a, b) =>
        a.order > b.order ? 1 : -1
      );
    },
    drop(e) {
      e.preventDefault();
      const order = e.target.getAttribute("data-order") || "";
      const data = parseInt(e.dataTransfer?.getData("text") || "", 10);
      const idx = parseInt(order, 10);
      // do we have a valid move?
      if (data !== idx && data + 1 !== idx) {
        // console.log(`FROM: ${data} TO: ${idx}`);
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
      const elements = document.querySelectorAll(".drop-zone");
      elements.forEach((x) => {
        x.classList.remove("active");
        x.style.height = "0px";
      });
    },
    dragStart() {
      // this will act as a continuation of the drag-start method
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
