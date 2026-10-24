import IconClose from "./icon-close.js";
import { useSiteStore } from "../store/index.js";

export default {
  name: "layout-editor-tab",
  template: `<div class="editor-tabs">
  <template v-for="tab in tabs" :key="tab">
    <button 
      type="button"
      class="editor-tab--item"
      :class="{'active': tab === activeTab}"
      @click="selectTab(tab)"> 
        {{ tab }} 
        <a href="#" @click.stop="closeTab(tab)">
          <IconClose class="icon"></IconClose>
        </a>
      </button> 
  </template>
  </div>`,
  components: { IconClose },
  computed: {
    tabs() {
      return useSiteStore().editorTabs;
    },
    activeTab() {
      return useSiteStore().activeTab;
    },
  },
  methods: {
    selectTab(tab = "") {
      useSiteStore().setActiveTab(tab);
    },
    closeTab(tab = "") {
      useSiteStore().removeTab(tab);
    },
  },
};
