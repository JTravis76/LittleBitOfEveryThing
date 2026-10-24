<template>
  <div class="main-content">
    <div class="editor">
      <LayoutEditorTab></LayoutEditorTab>
      <LayoutEditor></LayoutEditor>
    </div>
    <div>
      <LayoutPanel v-if="showPanel"></LayoutPanel>
    </div>
  </div>
</template>

<script lang="ts">
import { defineComponent } from "vue";
import LayoutPanel from "../components/LayoutPanel.vue";
import LayoutEditorTab from "../components/LayoutEditorTab.vue";
import LayoutEditor from "../components/LayoutEditor.vue";
import { useSiteStore } from "../stores";

export default defineComponent({
  name: "TheHome",
  components: { LayoutPanel, LayoutEditorTab, LayoutEditor },
  computed: {
    showPanel() {
      return useSiteStore().solutionExplorer;
    },
  },
});
</script>

<style lang="scss"></style>
