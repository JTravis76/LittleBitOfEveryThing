<template>
  <aside class="left-sidebar">
    <LayoutDockingPanel>Server Explorer</LayoutDockingPanel>
    <LayoutDockingPanel>Server Object Explorer</LayoutDockingPanel>
  </aside>
</template>

<script lang="ts">
import { defineComponent } from "vue";
import LayoutDockingPanel from "./LayoutDockingPanel.vue";

export default defineComponent({
  name: "LayoutSidebarLeft",
  components: { LayoutDockingPanel },
});
</script>

<style lang="scss" scoped>
aside.left-sidebar {
  height: 100%;
  width: 26px;
  position: fixed;
  top: 0;
  left: 0px;
  background-color: var(--purple1);
  overflow-x: hidden;
  color: var(--white);
}
</style>
