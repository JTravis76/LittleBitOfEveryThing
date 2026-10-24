import Icon from "./v-icon.js";

export default {
  template: `
  <div class="accordion--panel">
    <button
      type="button"
      class="accordion--panel-header"
      aria-label="collpase or expand section"
      @click="toggle()"
    >
      <div class="accordion--panel-title">{{ title }}</div>
      <div class="accordion--panel-control">
        <RouterLink :to="'/group/' + id" class="control-button"
          >Manage Group</RouterLink
        >
        <a
          v-if="draggable"
          href="#"
          class="btn btn-icon control-button-drag"
          title="drag this panel"
          :data-order="order"
          @dragstart="dragStart"
        >
          <Icon icon="material-symbols:drag-handle"></Icon>
        </a>
      </div>
    </button>
    <div class="accordion--panel-body" :class="{ open: expanded }">
      <slot></slot>
    </div>
  </div>
`,
  name: "v-accordion",
  components: {
    Icon,
  },
  props: {
    id: {
      type: Number,
      default: 0,
    },
    title: {
      type: String,
      default: "",
    },
    expanded: {
      type: Boolean,
      default: false,
    },
    draggable: {
      type: Boolean,
      default: false,
    },
    order: {
      type: Number,
      default: 0,
    },
  },
  emits: ["toggle", "drag-start"],
  methods: {
    toggle() {
      this.$emit("toggle", !this.expanded);
    },
    dragStart(e) {
      const order = e.target.getAttribute("data-order") || "";
      e.dataTransfer?.setData("text", order);
      this.$emit("drag-start");
    },
  },
};
