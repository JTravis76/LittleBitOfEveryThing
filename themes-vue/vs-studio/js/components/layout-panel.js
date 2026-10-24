import { useSiteStore } from "../store/index.js";
import IconConnectArrowRight from "./icon-connect-arrow-right.js";
import IconHome from "./icon-home.js";
import IconWrench from "./icon-wrench.js";
import IconCheckedIn from "./icon-checked-in.js";
import IconWebApplication from "./icon-web-application.js";
import IconCaretRight from "./icon-caret-right.js";
import IconCaretRightOpen from "./icon-caret-right-open.js";
import LayoutPanelTreeItem from "./layout-panel-tree-item.js";

export default {
  name: "layout-panel",
  template: `
  <div class="panel">
    <div class="panel-title"> Solution Explorer </div>
    <div class="panel-toolbar">
      <IconConnectArrowRight class="icon left"></IconConnectArrowRight>
      <IconConnectArrowRight class="icon"></IconConnectArrowRight>
      <IconHome class="icon"></IconHome>
      <div class="divider"></div>
      <IconWrench class="icon active"></IconWrench>
    </div>
    <div class="panel-body">
      <div>
        <IconCheckedIn size="9px" style="fill: #005DBA; margin: 0 4px 0 0;"></IconCheckedIn> 
        <img alt="vs-solution" src="./img/vs-solution.png" style="width: 12px;" />
        <span> Solution 'Portfolio' (1 of 1 projects) </span>
      </div>
      <ul>
        <li> 
          <button type="button" class="btn-icon" title="expand or collpase" @click="expand = !expand">
            <IconCaretRightOpen v-if="expand" class="icon-7"></IconCaretRightOpen>
            <IconCaretRight v-else size="9px"></IconCaretRight>
          </button>
          <IconCheckedIn size="9px" style="fill: #005DBA; margin: 0 4px 0 0;"></IconCheckedIn> 
          <IconWebApplication class="icon-14"></IconWebApplication>
          <span class="text-bold"> Portfolio </span>

          <ul :style="{display}">
            <li v-for="item in menuItems.filter(x => x.parent === 1)" :key="item.id">
              <LayoutPanelTreeItem 
                :item="item" 
                @action="openFileDirectory($event)">
              </LayoutPanelTreeItem>
              <template v-if="item.type !== 'file'">
                <ul :style="{display: item.expand ? 'block' : 'none'}">
                  <li v-for="subitem in menuItems.filter(x => x.parent === item.id)" :key="item.id">
                    <LayoutPanelTreeItem 
                      :item="subitem" 
                      @action="openFileDirectory($event)">
                    </LayoutPanelTreeItem>
                  </li>
                </ul>
              </template>
            </li>
          </ul>

        </li>
      </ul>
    </div>
    <div class="tabs"> 
      <button type="button" class="tab-item active">Solution Explorer</button> 
      <button type="button" class="tab-item">Properties</button> 
      <button type="button" class="tab-item">Git Changes</button> 
    </div>
  </div>`,
  components: {
    IconConnectArrowRight,
    IconHome,
    IconWrench,
    IconCheckedIn,
    IconCaretRight,
    IconCaretRightOpen,
    IconWebApplication,
    LayoutPanelTreeItem,
  },
  data() {
    return {
      expand: true,
    };
  },
  computed: {
    display() {
      return this.expand ? "block" : "none";
    },
    menuItems() {
      return useSiteStore().menuItems;
    },
  },
  methods: {
    openFileDirectory(id = 0) {
      const item = this.menuItems.find((x) => x.id === id) || { name: "" };
      if (item.type === "file") useSiteStore().setEditorTab(item.name);
      else {
        item.expand = !item.expand;
      }
    },
  },
};
