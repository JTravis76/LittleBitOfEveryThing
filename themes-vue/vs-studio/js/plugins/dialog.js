import { h, defineComponent } from "../deps.js";
import { mount } from "../lib/mount-vue-component.js";

export const dlgPlugin = {
  install: (app) => {
    app.config.globalProperties.$dialog = (options) => {
      // function showFallback(dlg: HTMLDialogElement, opt: DialogOptions) {
      //   if (!dlg) throw new Error("Dialog is not defined.");

      //   if (!dlg["showModal"]) {
      //     console.warn(
      //       "[WARN] your browser doesn't support native dialog api. Using fallback api.",
      //     );

      //     // below is design based on Chrome's dialog api
      //     dlg.remove = () => {
      //       const body = document.getElementsByTagName("body")[0];
      //       body.removeChild(dlg);
      //     };

      //     dlg.show = () => {
      //       dlg.setAttribute("open", "");
      //       dlg.style.width = "500px";
      //       dlg.style.display = "block";
      //       dlg.style.position = "absolute";
      //       dlg.style.margin = "auto";
      //       dlg.style.background = "white";
      //       dlg.style.color = "black";
      //       dlg.style.border = "solid";
      //       dlg.style.borderWidth = "2px";
      //       dlg.style.borderColor = "black";
      //       dlg.style.padding = "1em";
      //       //center both H & V
      //       dlg.style.top = "50%";
      //       dlg.style.left = "50%";
      //       dlg.style.transform = "translate(-50%, -50%)";
      //     };

      //     dlg.showModal = () => {
      //       dlg.show();

      //       const body = document.getElementsByTagName("body")[0];
      //       const div = document.createElement("div");
      //       div.classList.add("dialog-backdrop");
      //       div.style.position = "fixed";
      //       div.style.top = "0px";
      //       div.style.left = "0px";
      //       div.style.right = "0px";
      //       div.style.bottom = "0px";
      //       div.style.background = "rgba(0,0,0,0.2)";
      //       div.style.zIndex = "50";
      //       body.appendChild(div);
      //     };

      //     dlg.close = (v: string) => {
      //       dlg.removeAttribute("open");
      //       dlg.style.display = "none";
      //       // IE 11 support to remove backdrop element
      //       const body = document.getElementsByTagName("body")[0];
      //       const div = document.querySelector(".dialog-backdrop");
      //       if (div !== null) body.removeChild(div);
      //       //set the return value
      //       dlg.returnValue = v.toString();
      //     };

      //     dlg.open = dlg.hasAttribute("open");
      //   }
      // }

      let defaultOpt = {
        message: "",
        backdrop: true,
        title: "Message",
        type: "ok",
      };

      if (typeof options === "string") {
        defaultOpt.message = options;
      } else {
        defaultOpt = Object.assign(defaultOpt, options);
      }

      return new Promise((resolve) => {
        // const comp = createApp(Tmp);
        // const dlg = document.createElement("dialog");
        // comp.mount(dlg);
        // document.body.appendChild(dlg);

        const comp = defineComponent({
          data() {
            return {
              dlg: null,
            };
          },
          // beforeMount() {
          //   this.dlg = document.querySelector("dialog") as HTMLDialogElement;

          //   // No need to create them, they already exists
          //   if (this.dlg) return;

          //   // document.body.appendChild(document.createElement("dialog"));
          //   console.log("BEFOREMOUNT");
          // },
          mounted() {
            this.$nextTick(() => {
              // now get the current version once the DOM is patched
              this.dlg = document.querySelector("dialog");
              this.show();
            });
          },
          methods: {
            response(action) {
              const dlg = this.dlg;

              if (action) {
                if (defaultOpt.type === "ok") dlg.close("OK");
                if (defaultOpt.type === "yesno") dlg.close("YES");
                resolve(this.dlg.returnValue);
              } else {
                dlg.close("NO");
                resolve(this.dlg.returnValue);
                // reject(this.dlg.returnValue); // Really should only use reject for Errors
              }
              dlg.remove();
            },
            useFallback() {
              // TODO: !! Needs TESTING !!
              console.log("FALLBACK");
              // showFallback(this.dlg, defaultOpt);
            },
            show() {
              // fallback for IE 11 or older browsers
              if (!this.dlg["showModal"]) this.useFallback();

              if (defaultOpt.backdrop) this.dlg.showModal();
              else this.dlg.show();
            },
          },
          render() {
            // // == Ex 1: Several Root fragments
            // const fragments = [];
            // fragments.push(h("div", "Place text in div element"));
            // fragments.push(h("span", "Hi, I am a span"));
            // return fragments;

            // // == Ex 2: Load globally registered component
            // return h(resolveComponent('TestComp'));

            // // == Ex 3: Nested fragments
            // return h("div", [
            //   h("span", [
            //     h("i", "some text")
            //   ]),
            //   h("div")
            // ]);

            // // == EX 4: load template from .vue
            // // ?? not sure how to fetch emit response ??
            // const props = {
            //   title: defaultOpt.title,
            //   message: defaultOpt.message,
            //   type: defaultOpt.type,
            //   // "onUpdate:???": (v: boolean) => this.$emit("resp:response", v)
            //   class: {
            //     test: true
            //   }
            // };
            // // return createVNode(Dialog, props, null);
            // // -- OR --
            // return h(Dialog, props);

            // == EX 5: Inline text, Does not convert HTML :-/
            // return "This adds a simple text";

            // == EX 6: Building an array of fragments
            const frags = [];
            frags.push(
              h(
                "div",
                {
                  class: "title",
                  "aria-label": "",
                  style: "",
                },
                defaultOpt.title
              )
            );

            frags.push(
              h("div", { class: "alert", innerHTML: defaultOpt.message })
            );

            if (defaultOpt.type === "ok") {
              frags.push(
                h("div", { class: "footer" }, [
                  h(
                    "button",
                    {
                      type: "button",
                      class: "btn default",
                      onClick: () => this.response(true),
                    },
                    "OK"
                  ),
                ])
              );
            } else if (defaultOpt.type === "yesno") {
              frags.push(
                h("div", { class: "footer" }, [
                  h(
                    "button",
                    {
                      type: "button",
                      class: "btn primary",
                      onClick: () => this.response(true),
                    },
                    [h("span", { class: "fa fa-thumbs-up" }, " Yes")]
                  ),
                  h(
                    "button",
                    {
                      type: "button",
                      class: "btn default",
                      onClick: () => this.response(false),
                    },
                    [h("span", { class: "fa fa-thumbs-down" }, " No")]
                  ),
                ])
              );
            }
            return frags;
          },
        });

        const element = document.createElement("dialog");
        element.className = "confirmation";

        const { el } = mount(comp, {
          app,
          element,
        });
        document.querySelector("dialog")?.remove();
        document.body.appendChild(el);
      });
    };
  },
};

export default dlgPlugin;
