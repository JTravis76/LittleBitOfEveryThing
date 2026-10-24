<template>
  <footer>
    <IconMessageBubbleSquare class="icon"></IconMessageBubbleSquare>
    Ready
  </footer>
</template>

<script lang="ts">
import { defineComponent } from "vue";
import IconMessageBubbleSquare from "./IconMessageBubbleSquare.vue";

export default defineComponent({
  name: "LayoutFooter",
  components: { IconMessageBubbleSquare },
});
</script>

<style lang="scss" scoped>
footer {
  position: fixed;
  bottom: 0;
  width: 100%;
  height: 18px;
  background-color: var(--purple2);
  color: var(--white);
  padding: 6px;
  z-index: 1;
  display: flex;

  .icon {
    width: 16px;
    height: 16px;
    fill: var(--white);
    margin: 2px 4px 0 0;
  }
}
</style>
