<template>
  <div v-if="template !== ''" class="app-editor">
    <component :is="template"></component>
  </div>
</template>

<script lang="ts">
import { defineComponent } from "vue";
import { useSiteStore } from "../stores";
import AppAboutMe from "./AppAboutMe.vue";
import AppResume from "./AppResume.vue";
import AppComponentBook from "./AppComponentBook.vue";
import AppBinaryClock from "./AppBinaryClock.vue";
import AppGameboard from "./AppGameboard.vue";
import AppMetronome from "./AppMetronome.vue";
// import AppBible from "./app-online-bible.js";
import AppAudioPlaylist from "./AppAudioPlaylist.vue";
import AppFloatingWindow from "./AppFloatingWindow.vue";
import AppPinterestGrid from "./AppPinterestGrid.vue";
import AppProgressBar from "./AppProgressBar.vue";
// import AppBubbleTip from "./app-bubble-tip.js";
import AppParseCsv from "./AppParseCsv.vue";
import AppDragDrop from "./AppDragDrop.vue";
import AppDragDrop2 from "./AppDragDrop2.vue";
import AppFishTank from "./AppFishTank.vue";
import AppSplitPane from "./AppSplitPane.vue";
import AppMarkdownHelp from "./AppMarkdownHelp.vue";
import AppBudgetDashboard from "./AppBudgetDashboard.vue";
import AppBubbleTip from "./AppBubbleTip.vue";
import AppRPG from "./AppRpg.vue";

export default defineComponent({
  name: "LayoutEditor",
  components: {
    AppAboutMe,
    AppResume,
    AppComponentBook,
    AppBinaryClock,
    AppGameboard,
    AppMetronome,
    // AppBible,
    AppAudioPlaylist,
    AppFloatingWindow,
    AppPinterestGrid,
    AppProgressBar,
    AppBubbleTip,
    AppParseCsv,
    AppDragDrop,
    AppDragDrop2,
    AppFishTank,
    AppSplitPane,
    AppMarkdownHelp,
    AppBudgetDashboard,
    AppRPG,
  },
  computed: {
    template() {
      const tab = useSiteStore().activeTab;
      if (tab.includes(".")) return `App${tab.split(".")[0]}`;
      return "";
    },
  },
});
</script>

<style lang="scss" scoped>
.app-editor {
  background-color: var(--white);
  width: calc(100vw - 330px);
  height: calc(100vh - 94px);
  border-top: 3px solid var(--panel-color1);
  border-left: 20px solid #e2e2e2;
  padding: 0;
  overflow-x: auto;

  @include xs {
    width: calc(100vw - 50px);
  }
}
</style>
