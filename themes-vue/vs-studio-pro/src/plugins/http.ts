import { App } from "vue";
import apiManager from "../api";

export const httpPlugin = {
  install: (app: App) => {
    app.config.globalProperties.$http = apiManager;
  },
};
