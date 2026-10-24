<template>
  <div>
    <button type="button" @click="open = !open">
      <slot></slot>
    </button>
    <BoxTip :open="open" @close="() => (open = false)"
      ><slot name="tip"></slot
    ></BoxTip>
  </div>
</template>

<script lang="ts">
import { defineComponent } from "vue";
import BoxTip from "./VBoxTip.vue";

export default defineComponent({
  name: "VBoxTipButton",
  components: { BoxTip },
  data() {
    return {
      open: false,
    };
  },
});
</script>

<style lang="scss" scoped>
button {
  background-color: transparent;
  border: none;
  border-radius: 4px;
  cursor: pointer;

  &:hover {
    background-color: var(--color--gray3);
    color: var(--color--white);
  }
}
</style>
