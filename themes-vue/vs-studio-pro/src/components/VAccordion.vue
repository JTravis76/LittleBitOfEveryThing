<template>
  <div class="accordion--panel">
    <div
      role="button"
      tabindex="0"
      class="accordion--panel-header"
      aria-label="collpase or expand section"
      @click="toggle()"
      @keyup="handleEvent"
    >
      <div class="accordion--panel-title">{{ title }}</div>
      <div class="accordion--panel-control">
        <RouterLink v-if="circuitId !== 0" to="#" class="control-button"
          >Add Equipment</RouterLink
        >
        <RouterLink v-if="circuitId !== 0" to="#" class="control-button"
          >Manage Circuit Group</RouterLink
        >
        <a
          v-if="draggable"
          href="#"
          class="btn btn-icon control-button-drag"
          title="drag this panel"
          :data-order="order"
          @dragstart="dragStart"
          @dragend="dragEnd"
        >
          <Icon icon="drag"></Icon>
        </a>
      </div>
    </div>
    <div class="accordion--panel-body" :class="{ open: expanded }">
      <slot></slot>
    </div>
  </div>
</template>

<script lang="ts">
import { defineComponent } from "vue";
import Icon from "./VIcon.vue";

export default defineComponent({
  name: "VAccordion",
  components: {
    Icon,
  },
  props: {
    id: {
      type: Number,
      default: 0,
    },
    circuitId: {
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
  emits: ["toggle", "drag-start", "drag-end"],
  methods: {
    toggle() {
      this.$emit("toggle", !this.expanded);
    },
    handleEvent(e: KeyboardEvent) {
      const keys = "* *Enter";
      if (keys.includes(e.key)) {
        this.toggle();
      }
    },
    dragStart(e: DragEvent) {
      (e.target as HTMLAnchorElement).classList.add("dragging");
      const order = (e.target as HTMLElement).getAttribute("data-order") || "";
      e.dataTransfer?.clearData();
      e.dataTransfer?.setData("text/plain", order);
      this.$emit("drag-start");
    },
    dragEnd(e: DragEvent) {
      (e.target as HTMLAnchorElement).classList.remove("dragging");
      this.$emit("drag-end");
    },
  },
});
</script>

<style lang="scss" scoped>
.accordion--panel {
  min-height: 49px;
  width: 100%;
  margin: 2px 0;

  .accordion--panel-header {
    width: 100%;
    height: 48px;
    background-color: var(--color--gray2);
    border: none;
    border-top-left-radius: 4px;
    border-top-right-radius: 4px;
    padding: 10px;

    cursor: pointer;
    display: flex;
    justify-content: space-between;
    align-items: center;

    &:hover {
      background-color: var(--color--primary);
      color: var(--color--white);
    }

    .accordion--panel-title {
      font-size: 14px;
    }

    .accordion--panel-control {
      display: flex;

      a {
        text-decoration: none;
        color: var(--color--black);

        &.dragging {
          color: var(--color--primary);
          background-color: var(--color--primary);
        }
      }

      .control-button {
        background-color: var(--color--gray3);
        color: var(--color--white);
        font-size: 10px;
        line-height: 2.4;
        white-space: nowrap;
        border-radius: 20px;
        padding: 0 14px;
        margin: 0 4px;

        &:hover {
          background-color: var(--color--black);
        }
      }

      .control-button-drag {
        display: inline-block;
        width: 24px;
        height: 24px;
        margin-left: 4px;
        cursor: move;
      }
    }
  }

  .accordion--panel-body {
    background-color: transparent;
    min-height: 100px;
    border-bottom-left-radius: 4px;
    border-bottom-right-radius: 4px;
    display: none;
  }

  .accordion--panel-body.open {
    display: block;
  }
}
</style>
