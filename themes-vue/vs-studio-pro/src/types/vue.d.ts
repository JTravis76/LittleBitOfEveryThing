import apiManager from "../api";
import { IFilter } from "../plugins/filters";
import { DialogOptions } from "../plugins/dialog";

declare module "@vue/runtime-core" {
  interface ComponentCustomProperties {
    /** http client */
    $http: typeof apiManager;

    $filters: IFilter;

    // /** Displays a toast message */
    // $toast: ToastConstructor;

    /** Displays a dialog */
    $dialog(options: string | DialogOptions): Promise<string>;
  }
}

import "vue-router";

declare module "vue-router" {
  interface RouteMeta {
    // requiresAuth?: boolean;
    // roles?: string[];
    // breadcrumbs?: string[];
  }
}
