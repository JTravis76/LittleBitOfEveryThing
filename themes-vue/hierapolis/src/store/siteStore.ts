import { defineStore } from "pinia";

export const useSiteStore = defineStore("site", {
  state: () => {
    return {
      // General
      pageLoading: false,
      httpConnections: 0,
      sidebar: false,
      // Configuration
      appName: "Hierapolis Theme",
      description: "",
      baseApi: `${import.meta.env.VITE_API_URL}`,
      version: "1.0.0.0",
      versionDate: "2023-02-22T16:17:27.151",
      environment: `${import.meta.env.MODE}`,
      // Profile
      user: {},
      roles: ["Admin", "NCManager"],
      token: window.localStorage.getItem("token") || null,
    };
  },
  actions: {
    toggleSidebar() {
      this.sidebar = !this.sidebar;
    },
    pageLoader(loading = true) {
      this.pageLoading = loading;
    },
    addConnection() {
      this.httpConnections += 1;
    },
    removeConnection() {
      this.httpConnections -= 1;
    },
  },
  getters: {
    baseURL: (state) => state.baseApi,
  },
});
