import { defineStore } from "../deps.js";

// type bannerType = "dismissible" | "persistent";
// type bannerStyle = "default" | "success" | "warning" | "error";

// export interface IBanner {
//   type: bannerType;
//   style: bannerStyle;
//   message: string;
//   subMessage?: string | null;
//   timer?: number;
// }

export const useBannerStore = defineStore("banner", {
  state: () => {
    return {
      banners: [], // new Array<IBanner>()
    };
  },
  actions: {
    success() {
      this.set({
        type: "dismissible",
        style: "success",
        message: "data saved successfully!",
        timer: 3500,
      });
    },
    set(banner) {
      if (this.banners.length < 2) {
        this.banners.push(banner);
      }
    },
    remove(idx) {
      this.banners.splice(idx, 1);
    },
    clear() {
      this.banners = [];
    },
  },
  getters: {},
});
