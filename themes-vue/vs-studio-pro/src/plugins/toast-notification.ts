/* eslint-disable @typescript-eslint/no-explicit-any */
import { App } from "vue";
import { mount } from "../lib/mount-vue-component";
import ToastNotification from "../components/ToastNotification.vue";

//== based on https://github.com/ankurk91/vue-toast-notification ==//
// Usages: app.use(toastPlugin, { position: 'top' });
export const toastPlugin = {
  install: (app: App, ops: any) => {
    app.config.globalProperties.$toast = {
      open(options: any) {
        let message = "";
        if (typeof options === "string") message = options;

        const defaultOptions = {
          message,
        };

        const propData = Object.assign({}, defaultOptions, ops, options);

        const { vNode } = mount(ToastNotification, {
          props: propData,
          app,
        });
        return vNode;
      },
      success(message: string, options: any = {}) {
        return this.open(
          Object.assign(
            {},
            {
              message,
              type: "success",
            },
            options,
          ),
        );
      },
      info(message: string, options: any = {}) {
        return this.open(
          Object.assign(
            {},
            {
              message,
              type: "info",
            },
            options,
          ),
        );
      },
      warning(message: string, options: any = {}) {
        return this.open(
          Object.assign(
            {},
            {
              message,
              type: "warning",
            },
            options,
          ),
        );
      },
      error(message: string, options: any = {}) {
        return this.open(
          Object.assign(
            {},
            {
              message,
              type: "error",
            },
            options,
          ),
        );
      },
      default(message: string, options: any = {}) {
        return this.open(
          Object.assign(
            {},
            {
              message,
              type: "default",
            },
            options,
          ),
        );
      },
      clear() {
        // TOOD:
      },
    };
  },
};

export default toastPlugin;
