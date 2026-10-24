import { defineStore } from "../deps.js";
import menuItems from "../../data/menuItems.js";

export const useSiteStore = defineStore("site", {
  state: () => {
    return {
      editorTabs: ["ComponentBook.html"],
      activeTab: "ComponentBook.html",
      menuItems,
    };
  },
  actions: {
    setEditorTab(name = "") {
      if (name.length > 0 && !this.editorTabs.includes(name)) {
        this.editorTabs.push(name);
        this.setActiveTab(name);
      }
    },
    setActiveTab(name = "") {
      this.activeTab = name;
    },
    removeTab(name = "") {
      const tabCount = this.editorTabs.length;
      for (let i = tabCount - 1; i > -1; i--) {
        if (this.editorTabs[i] === name) {
          this.editorTabs.splice(i, 1);

          if (this.editorTabs.length === 0) this.activeTab = "";
          else {
            this.activeTab = this.editorTabs[i > 0 ? i - 1 : 0];
          }
          break;
        }
      }
    },
  },
  getters: {},
});
