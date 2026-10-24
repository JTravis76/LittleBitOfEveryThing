<template>
  <div class="toolbar-menu">
    <div v-for="control in controls" :key="control.title">
      <button
        type="button"
        :title="control.title"
        :disabled="control.disabled"
        @click="$emit('action', control.icon)"
      >
        <Icon :icon="control.icon"></Icon>
      </button>
    </div>
  </div>
</template>

<script lang="ts">
import { defineComponent, PropType } from "vue";
import Icon from "./VIcon.vue";

export interface IToolbarMenu {
  title: string;
  icon: string;
  disabled: boolean;
}

export default defineComponent({
  name: "ToolbarMenu",
  components: {
    Icon,
  },
  props: {
    controls: {
      type: Array as PropType<IToolbarMenu[]>,
      default: new Array<IToolbarMenu>(),
    },
  },
  emits: ["action"],
});
</script>

<style lang="scss" scoped>
.toolbar-menu {
  display: flex;
  border: 1px solid var(--color--gray3);
  border-radius: 4px;

  button {
    background-color: transparent;
    border: none;
    border-right: 0.5px dotted var(--color--gray3);
    height: 40px;
    width: 40px;
    font-size: 24px;
    display: block;
    text-align: center;

    &:hover {
      background-color: var(--color--black);
      color: var(--color--white);
    }

    &[disabled] {
      cursor: not-allowed;
      background-color: var(--color--gray1);
      color: var(--color--gray3);
    }
  }
}
</style>
