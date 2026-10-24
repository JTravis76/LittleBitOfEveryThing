<template>
  <transition
    :enter-active-class="transition.enter"
    :leave-active-class="transition.leave"
  >
    <div
      v-show="isActive"
      role="alert"
      class="toast"
      :class="['toast-' + type, 'is-' + position]"
      @click="whenClicked"
    >
      <div class="toast-icon"></div>
      <p class="toast-text">{{ message }}</p>
    </div>
  </transition>
</template>

<script lang="ts">
import { defineComponent } from "vue";
// == based on https://github.com/ankurk91/vue-toast-notification ==//
const removeElement = (el: Element) => {
  if (typeof el.remove !== "undefined") {
    el.remove();
  } else {
    el.parentNode?.removeChild(el);
  }
};

const hasWindow = () => typeof window !== "undefined";

const HTMLElement = hasWindow() ? window.HTMLElement : Object;

export default defineComponent({
  name: "VToastNotification",
  props: {
    message: {
      type: String,
      required: true,
    },
    type: {
      type: String,
      default: "default",
    },
    position: {
      type: String,
      default: "bottom-right",
    },
    duration: {
      type: Number,
      default: 3000,
    },
    dismissible: {
      type: Boolean,
      default: true,
    },
    onClose: {
      type: Function,
      default: () => ({}),
    },
    onClick: {
      type: Function,
      default: () => ({}),
    },
    queue: {
      type: Boolean,
      default: false,
    },
    container: {
      type: [Object, HTMLElement],
      default: null,
    },
  },
  data() {
    return {
      isActive: false,
      parentTop: null as unknown as Element | null,
      parentBottom: null as unknown as Element | null,
      timer: 0,
    };
  },
  computed: {
    correctParent(): Element | null | undefined {
      switch (this.position) {
        case "top-right":
        case "top":
        case "top-left":
          return this.parentTop;

        case "bottom-right":
        case "bottom":
        case "bottom-left":
          return this.parentBottom;
        default:
          return null;
      }
    },
    transition(): { enter: string; leave: string } {
      switch (this.position) {
        case "top-right":
        case "top":
        case "top-left":
          return {
            enter: "fadeInDown",
            leave: "fadeOut",
          };
        case "bottom-right":
        case "bottom":
        case "bottom-left":
          return {
            enter: "fadeInUp",
            leave: "fadeOut",
          };
        default:
          return {
            enter: "",
            leave: "",
          };
      }
    },
  },
  beforeMount() {
    this.setupContainer();
  },
  mounted() {
    this.showNotice();
    // EventBus.$on("toast.clear", this.close);
  },
  methods: {
    setupContainer() {
      this.parentTop = document.querySelector(".notices.is-top");
      this.parentBottom = document.querySelector(".notices.is-bottom");

      // No need to create them, they already exists
      if (this.parentTop && this.parentBottom) return;

      if (!this.parentTop) {
        this.parentTop = document.createElement("div");
        this.parentTop.className = "notices is-top";
      }

      if (!this.parentBottom) {
        this.parentBottom = document.createElement("div");
        this.parentBottom.className = "notices is-bottom";
      }

      const container = this.container || document.body;
      container.appendChild(this.parentTop);
      container.appendChild(this.parentBottom);
      const containerParentClass = "is-custom-parent";

      if (this.container) {
        this.parentTop.classList.add(containerParentClass);
        this.parentBottom.classList.add(containerParentClass);
      }
    },
    shouldQueue() {
      if (!this.queue) return false;
      if (this.parentTop === null || this.parentBottom === null) return false;
      return (
        this.parentTop.childElementCount > 0 ||
        this.parentBottom.childElementCount > 0
      );
    },
    close() {
      let _arguments = arguments;

      clearTimeout(this.timer);
      this.isActive = false; // Timeout for the animation complete before destroying

      setTimeout(() => {
        this.onClose.apply(null, _arguments);
        // vm.$destroy(); TODO: not available in Vue 3 ??
        removeElement(this.$el);
      }, 150);
    },
    showNotice() {
      if (this.shouldQueue()) {
        // Call recursively if should queue
        setTimeout(() => {
          return this.showNotice();
        }, 250);
        return;
      }

      if (this.correctParent) {
        this.correctParent.insertAdjacentElement("afterbegin", this.$el);
        this.isActive = true;
        this.timer = setTimeout(() => {
          return this.close();
        }, this.duration);
      }
    },
    whenClicked() {
      if (!this.dismissible) return;
      this.onClick.apply(null, arguments);
      this.close();
    },
  },
});
</script>

<style lang="scss"></style>
