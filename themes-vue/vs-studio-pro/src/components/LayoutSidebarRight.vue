<template>
  <aside class="right-sidebar">
    <LayoutDockingPanel v-if="show" side="right" @action="toggle()"
      >Solution Explorer</LayoutDockingPanel
    >
  </aside>
</template>

<script lang="ts">
import { defineComponent } from "vue";
import LayoutDockingPanel from "./LayoutDockingPanel.vue";
import { useSiteStore } from "../stores";

export default defineComponent({
  name: "LayoutSidebarRight",
  components: { LayoutDockingPanel },
  computed: {
    show() {
      return !useSiteStore().solutionExplorer;
    },
  },
  methods: {
    toggle() {
      useSiteStore().setSolutionExplorer();
    },
  },
});
</script>

<style lang="scss" scoped>
aside.right-sidebar {
  height: 100%;
  width: 26px;
  position: fixed;
  top: 0;
  right: 0;
  background-color: var(--purple1);
  overflow-x: hidden;
  color: var(--white);
}
</style>
