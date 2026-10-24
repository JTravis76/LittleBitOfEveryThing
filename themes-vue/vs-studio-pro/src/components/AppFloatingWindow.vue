<template>
  <div>
    <p style="margin-left: 20px">
      This page demostrates a floating assistance window in the lower right
      corner. Upon clicking will open a popup container where you can display
      about anything
    </p>
    <div id="hero-iframe-container" class="position-overridden">
      <div
        class="hero-launcher-container active hwp-smart-container"
        style="transform: translate(-5px, -40px)"
      >
        <div class="hero-launcher-avatar-container active standard">
          <section data-qa="launcher-icon-iframe" class="hero-launcher-avatar">
            <div class="frame-root">
              <div class="frame-content">
                <div class="launcher standard">
                  <button
                    id="btn1"
                    data-qa="launcher-icon-button"
                    class="launcher-button"
                    type="button"
                    title="need assistance"
                    style="background-color: rgb(0, 0, 0)"
                  >
                    <span>
                      <svg
                        width="40"
                        height="40"
                        fill="none"
                        class="launcher-button-cross"
                        style="stroke: white"
                      >
                        <path opacity="0.01" d="M6 6h28v28H6z"></path>
                        <path d="M25 25l-5-5 5-5M15 25l5-5-5-5"></path>
                      </svg>
                      <div class="launcher-button-content">
                        <div class="launcher-button-icon">
                          <svg
                            width="40"
                            height="40"
                            fill="none"
                            class=""
                            style="fill: white"
                          >
                            <path opacity="0.01" d="M6 6h28v28H6z"></path>
                            <path
                              clip-rule="evenodd"
                              d="M25.243 21.28c-.783 0-1.418-.655-1.418-1.463s.635-1.464 1.418-1.464c.783 0 1.418.655 1.418 1.464 0 .808-.635 1.464-1.418 1.464zm-5.02 0c-.783 0-1.418-.655-1.418-1.463s.635-1.464 1.418-1.464c.783 0 1.417.655 1.417 1.464 0 .808-.634 1.464-1.417 1.464zm-5.02 0c-.784 0-1.418-.655-1.418-1.463s.634-1.464 1.418-1.464c.782 0 1.417.655 1.417 1.464 0 .808-.635 1.464-1.418 1.464zm12.015-9.126h-13.99c-2.305 0-4.173 1.885-4.173 4.21V28.99c0 .662.765 1.025 1.27.602l1.997-1.669a3.972 3.972 0 012.549-.927H27.218c2.305 0 4.172-1.885 4.172-4.21v-6.423c0-2.325-1.867-4.21-4.172-4.21z"
                            ></path>
                          </svg>
                        </div>
                      </div>
                    </span>
                  </button>
                </div>
              </div>
            </div>
          </section>
        </div>
      </div>
      <div
        class="hero-plugin-container inactive"
        style="background-color: white"
      >
        <section class="hero-plugin active">
          <div style="height: 80px; background-color: black">
            <button
              type="button"
              style="color: white; margin: 4px"
              title="close window"
            >
              &times;
            </button>
          </div>
        </section>
      </div>
    </div>
  </div>
</template>

<script lang="ts">
import { defineComponent } from "vue";

export default defineComponent({
  name: "AppFloatingWindow",
  mounted() {
    this.$nextTick(() => {
      const btns = document.querySelectorAll("button");
      btns.forEach((b) => {
        b.addEventListener("click", () => {
          // e.stopPropagation();
          const con = document.querySelector(".hero-plugin-container");
          if (con) {
            con.classList.toggle("active");
            con.classList.toggle("inactive");
          }
        });
      });
    });
  },
});
</script>

<style lang="scss" scoped>
/*== Floating Window ==*/
#hero-iframe-container div.hero-launcher-avatar-container {
  position: absolute;
  right: 0;
  bottom: 0;
  width: 62px;
  height: 62px;
  box-shadow: 10px 10px 100px 0 rgba(0, 0, 0, 0.25),
    0 0 4px 0 rgba(0, 0, 0, 0.1);
  border-radius: 50%;
}

body .frame-root,
body .frame-root .frame-content {
  height: 100%;
}

div.launcher {
  width: 100%;
  height: 100%;
}

div.launcher button.launcher-button {
  width: 100%;
  height: 100%;
  border-radius: 50%;
}

#hero-iframe-container div.hero-launcher-container {
  bottom: 14px;
  right: 14px;
}

#hero-iframe-container div.hero-launcher-container {
  position: fixed;
  z-index: 2147483646 !important;
  bottom: 11px;
  right: 11px;
}

#hero-iframe-container div.hero-launcher-container.active,
#hero-iframe-container section.hero-launcher-avatar-container.active,
#hero-iframe-container section.hero-plugin.active,
#hero-iframe-container section.hero-shoppable-stories.active,
#hero-iframe-container section.hero-media.active,
#hero-iframe-container section.hero-opted-in.active {
  display: block;
}

#hero-iframe-container section.hero-launcher-avatar {
  width: 100%;
  height: 100%;
  height: 62px;
}

#hero-iframe-container section {
  z-index: 2147483646 !important;
  margin: 0;
  padding: 0;
  border: 0;
  font-size: 100%;
  font: inherit;
  vertical-align: baseline;
}

section {
  max-width: 100%;
}

div.launcher.standard button.launcher-button span div.launcher-button-content,
div.launcher.standard button.launcher-button span svg.launcher-button-cross {
  transition-property: opacity;
  transition-duration: 1s;
  transition-timing-function: cubic-bezier(0.645, 0.045, 0.355, 1);
}

div.launcher button.launcher-button span svg.launcher-button-cross {
  visibility: hidden;
  opacity: 0;
}

div.launcher
  button.launcher-button
  span
  div.launcher-button-content
  div.launcher-button-icon,
div.launcher button.launcher-button span svg.launcher-button-cross {
  position: absolute;
  top: 0;
  right: 0;
  margin: 11px;
}

div.launcher button.launcher-button span div.launcher-button-content,
div.launcher
  button.launcher-button
  span
  div.launcher-button-content
  div.launcher-button-icon,
div.launcher button.launcher-button span svg.launcher-button-cross {
  width: 40px;
  height: 40px;
}

svg {
  overflow: hidden;
  vertical-align: middle;
}

[type="button"]:not(:disabled),
[type="reset"]:not(:disabled),
[type="submit"]:not(:disabled),
button:not(:disabled) {
  cursor: pointer;
}

.hero-plugin-container button {
  margin: 0;
  padding: 0;
  background-color: transparent;
  border: none;
  cursor: pointer;
}

.hero-plugin-container button,
.hero-plugin-container select {
  text-transform: none;
}

[type="button"],
[type="reset"],
[type="submit"],
button {
  -webkit-appearance: button;
}

#hero-iframe-container.position-overridden div.hero-plugin-container {
  bottom: 14px;
}

#hero-iframe-container div.hero-launcher-push-container.inactive,
#hero-iframe-container div.hero-plugin-container.inactive {
  visibility: hidden;
  opacity: 0;
  -webkit-transform: translateY(8px);
  transform: translateY(8px);
  pointer-events: none;
}

#hero-iframe-container div.hero-plugin-container,
#hero-iframe-container section.hero-shoppable-stories {
  position: fixed;
  z-index: 2147483646 !important;
  bottom: 0px;
  right: 0px;
  width: 100%;
  height: 100%;
}

#hero-iframe-container div.hero-plugin-container,
#hero-iframe-container section.hero-shoppable-stories {
  bottom: 0px;
  right: 0px;
  width: 375px;
  height: 80vh;
  max-height: 600px;
  border-radius: 6px;
  border-bottom-right-radius: 0;
  box-shadow: 0 2px 10px 0 rgba(0, 0, 0, 0.1), 0 2px 4px 0 rgba(0, 0, 0, 0.1);
  overflow: hidden;
}

#hero-iframe-container div.hero-plugin-container,
#hero-iframe-container section.hero-shoppable-stories {
  bottom: 96px;
  right: 14px;
  border-bottom-right-radius: 4px;
}

#hero-iframe-container div.hero-plugin-container {
  transition-property: -webkit-transform opacity;
  transition-property: transform opacity;
  transition-property: transform opacity, -webkit-transform opacity;
  transition-duration: 0.5s;
  transition-timing-function: cubic-bezier(0.215, 0.61, 0.355, 1);
}

#hero-iframe-container div.hero-launcher-push-container.active,
#hero-iframe-container div.hero-plugin-container.active {
  visibility: visible;
  opacity: 1;
  -webkit-transform: translateX(0);
  transform: translateX(0);
  pointer-events: all;
}
</style>
