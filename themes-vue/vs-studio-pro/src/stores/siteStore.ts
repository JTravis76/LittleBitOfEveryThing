import { defineStore } from "pinia";
import menuItems from "../data/menuItems";

export interface INet6ErrorReponseData {
  errors?: Record<string, string[]>;
  status?: number;
  title?: string;
  traceId?: string;
  type?: string;
}

export interface IMvc4ErrorReponseData {
  errorMessage: string;
  exception?: string;
}

export type IErrorResponseData = IMvc4ErrorReponseData[] &
  INet6ErrorReponseData;

export const useSiteStore = defineStore("site", {
  state: () => {
    return {
      // General
      pageLoading: false,
      // Configuration
      appName: `${import.meta.env.VITE_APP_NAME}`,
      description: "",
      baseApi: `${import.meta.env.VITE_API_URL}`,
      version: "1.0.0.0",
      versionDate: "2023-04-21T10:30:00.000",
      environment: `${import.meta.env.MODE}`,
      // Validation-Summary
      validations: new Array<string>(),
      // Feature
      features: [
        { id: 1, name: "", active: false }, // could set prior to api loading
      ],
      // General
      editorTabs: ["AboutMe.html"],
      activeTab: "AboutMe.html",
      menuItems,
      solutionExplorer: true,
    };
  },
  actions: {
    pageLoader(show: boolean) {
      this.pageLoading = show;
    },
    setSolutionExplorer() {
      this.solutionExplorer = !this.solutionExplorer;
    },
    setValidations(payload?: string | string[] | IErrorResponseData) {
      this.validations = new Array<string>();
      if (typeof payload === "undefined") {
        return;
      } else if (payload) {
        if (typeof payload === "string") {
          // use for single validation messages ONLY
          this.validations = [payload];
        } else {
          // determine which type we have
          if (Array.isArray(payload)) {
            // == MVC 4 type
            const p = payload as IMvc4ErrorReponseData[];
            p.forEach((x) => {
              if (typeof x === "string") {
                this.validations.push(x);
              } else this.validations.push(x.errorMessage);
            });
          } else {
            // == Net Core 6 type
            const p = payload as INet6ErrorReponseData;
            const errors = p.errors as Record<string, string[]>;
            Object.keys(errors).forEach((key) => {
              errors[key].forEach((e) => {
                this.validations.push(e);
              });
            });
          }
        }
      }
    },
    setEditorTab(name = "") {
      if (name.length > 0 && !this.editorTabs.includes(name)) {
        this.editorTabs.push(name);
      }

      this.setActiveTab(name);
    },
    setActiveTab(name = "") {
      this.activeTab = name;
    },
    removeTab(idx: number) {
      const tab = this.editorTabs.splice(idx, 1)[0];
      if (this.editorTabs.length === 0) this.activeTab = "";
      else if (tab === this.activeTab) {
        // next one to the left
        this.activeTab = this.editorTabs[idx - 1];
      }
    },
  },
  getters: {},
});