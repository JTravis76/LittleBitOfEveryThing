import { useSiteStore } from "../store/index.js";
import AppAboutMe from "./app-about-me.js";
import AppResume from "./app-resume.js";
import AppBinaryClock from "./app-binary-clock.js";
import AppGameboard from "./app-gameboard.js";
import AppMetronome from "./app-metronome.js";
import AppBible from "./app-online-bible.js";
import AppAudioPlaylist from "./app-audio-playlist.js";
import AppFloatingWindow from "./app-floating-window.js";
import AppPinterestGrid from "./app-pinterest-grid.js";
import AppProgressBar from "./app-progress-bar.js";
import AppBubbleTip from "./app-bubble-tip.js";
import AppParseCsv from "./app-parse-csv.js";
import AppDragDrop from "./app-drag-drop.js";
import AppDragDrop2 from "./app-drag-drop2.js";
import AppFishTank from "./app-fish-tank.js";
import AppComponentBook from "./app-component-book.js";

export default {
  name: "layout-editor",
  template: `
  <div v-show="template !== ''" class="app-editor">
    <component :is="template"></component>
  </div>`,
  components: {
    AppAboutMe,
    AppResume,
    AppBinaryClock,
    AppGameboard,
    AppMetronome,
    AppBible,
    AppAudioPlaylist,
    AppFloatingWindow,
    AppPinterestGrid,
    AppProgressBar,
    AppBubbleTip,
    AppParseCsv,
    AppDragDrop,
    AppDragDrop2,
    AppFishTank,
    AppComponentBook,
  },
  computed: {
    template() {
      const tab = useSiteStore().activeTab;
      if (tab.includes("."))
        return `App${useSiteStore().activeTab.split(".")[0]}`;
      return "";
    },
  },
};
