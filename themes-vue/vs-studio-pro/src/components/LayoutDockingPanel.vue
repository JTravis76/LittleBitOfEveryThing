<template>
  <div class="docking-panel" :class="side">
    <div class="docking-panel__block" @click="$emit('action')">
      <span class="docking-panel__title">
        <slot></slot>
      </span>
    </div>
  </div>
</template>

<script lang="ts">
import { defineComponent } from "vue";

export default defineComponent({
  name: "LayoutDockingPanel",
  props: {
    side: {
      type: String,
      default: "left",
    },
  },
  emits: ["action"],
});
</script>

<style lang="scss" scoped>
.docking-panel {
  .docking-panel__block {
    border: none;
    border-bottom: 8px solid var(--purple2);
    width: -moz-fit-content;
    width: fit-content;
    cursor: pointer;

    &:hover {
      border-bottom: 8px solid var(--purple3);
    }

    .docking-panel__title {
      white-space: nowrap;
      font-size: 12px;
      font-family: sans-serif;
      letter-spacing: 0.5px;
    }
  }
}

.left {
  transform: rotate(90deg);
  margin-top: 75px;
}

.right {
  transform: rotate(-90deg);
  margin-top: 145px;
}
</style>
