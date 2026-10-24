<template>
  <div class="app-banner-notification" :class="modifiers" @click="toggle">
    <div class="app-banner-notification__wrapper">
      <span class="app-banner-notification__content">
        <span class="app-banner-notification__text" v-html="message"></span>
      </span>
      <div
        v-if="subMessage && bannerType === 'persistent' && expanded"
        class="mt-2 app-banner-notification__text"
        role="region"
        v-html="subMessage"
      ></div>
    </div>
    <button
      v-if="subMessage && bannerType === 'persistent'"
      type="button"
      class="app-banner-notification__close-button"
      :class="{ 'app-banner-expanded': expanded }"
      aria-label="Toggle notification"
      :aria-expanded="expanded"
    >
      <span class="app-icon app-icon-chevron-down"></span>
    </button>
    <button
      v-else-if="bannerType === 'dismissible'"
      type="button"
      class="app-banner-notification__close-button app-icon-clear app-icon"
      aria-label="Close notification"
      @click="dismiss"
    ></button>
  </div>
</template>

<script lang="ts">
import { defineComponent, PropType } from "vue";

export default defineComponent({
  name: "TopBanner",
  props: {
    bannerType: {
      type: String,
      default: "dismissible", // dismissible, persistent
    },
    bannerStyle: {
      type: String,
      default: "default", // default, success, warning, error
    },
    message: {
      type: String,
      default: "",
    },
    subMessage: {
      type: String as PropType<string | null>,
      default: null,
    },
    hide: {
      type: Boolean,
      default: false,
    },
    timer: {
      type: Number,
      default: 0,
    },
  },
  emits: ["dismiss"],
  data() {
    return {
      expanded: false,
      modifiers: {
        "app-banner-notification__type--dismissible": false,
        "app-banner-notification__type--persistent": false,
        "app-banner-notification__style--default": false,
        "app-banner-notification__style--success": false,
        "app-banner-notification__style--warning": false,
        "app-banner-notification__style--error": false,
        "app-banner-notification--show": !this.hide,
        "app-banner-notification--hide": this.hide,
      },
    };
  },
  watch: {
    hide: {
      immediate: true,
      deep: false,
      handler: function onHide(val: boolean) {
        this.modifiers["app-banner-notification--show"] = !val;
      },
    },
  },
  created() {
    this.modifiers["app-banner-notification__type--dismissible"] = false;
    this.modifiers["app-banner-notification__type--persistent"] = false;
    switch (this.bannerType) {
      case "dismissible":
        this.modifiers["app-banner-notification__type--dismissible"] = true;
        break;
      case "persistent":
        this.modifiers["app-banner-notification__type--persistent"] = true;
        break;
      default:
    }

    this.modifiers["app-banner-notification__style--default"] = false;
    this.modifiers["app-banner-notification__style--success"] = false;
    this.modifiers["app-banner-notification__style--warning"] = false;
    this.modifiers["app-banner-notification__style--error"] = false;
    switch (this.bannerStyle) {
      case "default":
        this.modifiers["app-banner-notification__style--default"] = true;
        break;
      case "success":
        this.modifiers["app-banner-notification__style--success"] = true;
        break;
      case "warning":
        this.modifiers["app-banner-notification__style--warning"] = true;
        break;
      case "error":
        this.modifiers["app-banner-notification__style--error"] = true;
        break;
      default:
    }

    if (this.timer > 0) {
      // Set the auto-dismiss timer
      window.setTimeout(() => {
        this.$emit("dismiss");
      }, this.timer);
    }
  },
  methods: {
    dismiss() {
      this.$emit("dismiss");
    },
    toggle() {
      if (this.subMessage) {
        this.expanded = !this.expanded;
      }
    },
  },
});
</script>

<style lang="scss" scoped>
$app-banner-notification__bottom-border--height: fds-rem(8px);

.app-banner-notification {
  width: 100%;
  overflow: hidden;
  box-shadow: $app-elevation__box-shadow--layer2;

  button {
    cursor: pointer;
    border: 0;
    background-color: transparent;
    font-weight: inherit;
    color: inherit;
    & .app-icon {
      transition: transform 0.3s ease-in-out;
      display: inherit;
    }
  }

  .app-banner-expanded {
    & .app-icon-chevron-down {
      transform: rotate(-180deg);
    }
  }
}

.app-banner-notification__close-button--focused {
  outline: 1px solid var(--app-color--primary) !important;
}

.app-banner-notification__type--dismissible {
  z-index: 9999;
}

.app-banner-notification__type--dismissible,
.app-banner-notification__type--persistent {
  position: relative;
}

.app-banner-notification--show {
  & .app-banner-notification__wrapper {
    margin-top: 0;
  }

  & .app-banner-notification__content,
  & .app-banner-notification__close-button::before {
    animation-name: app-banner-notification__icon-and-text;
  }

  &.app-banner-notification__style--default {
    & .app-banner-notification__wrapper {
      animation-name: app-banner-notification__wrapper--show,
        app-banner-notification__wrapper--information;
    }

    & .app-banner-notification__content::before {
      animation-name: app-banner-notification__icon-and-text;
    }
  }

  &.app-banner-notification__style--success {
    & .app-banner-notification__wrapper {
      animation-name: app-banner-notification__wrapper--show,
        app-banner-notification__wrapper--success;
    }

    & .app-banner-notification__content::before {
      animation-name: app-banner-notification__icon-and-text;
    }
  }

  &.app-banner-notification__style--warning {
    & .app-banner-notification__wrapper {
      animation-name: app-banner-notification__wrapper--show,
        app-banner-notification__wrapper--warning;
    }

    & .app-banner-notification__content::before {
      animation-name: app-banner-notification__icon-and-text;
    }
  }

  &.app-banner-notification__style--error {
    & .app-banner-notification__wrapper {
      animation-name: app-banner-notification__wrapper--show,
        app-banner-notification__wrapper--error;
    }

    & .app-banner-notification__content::before {
      animation-name: app-banner-notification__icon-and-text;
    }
  }
}

.app-banner-notification--hide {
  & .app-banner-notification__wrapper {
    animation-duration: 0.3s;
    animation-name: app-banner-notification__wrapper--hide;
    margin-top: fds-rem(-9999px);
  }
}

.app-banner-notification__style--default {
  & .app-banner-notification__wrapper {
    background-image: linear-gradient(
      to bottom,
      var(--app-color--white) 50%,
      var(--app-color--primary) 50%
    );
  }

  & .app-banner-notification__content::before {
    content: $app-icon-info-filled;
    color: var(--app-color--primary);
  }
}

.app-banner-notification__style--success {
  & .app-banner-notification__wrapper {
    background-image: linear-gradient(
      to bottom,
      var(--app-color--white) 50%,
      var(--app-color--success1) 50%
    );
  }

  & .app-banner-notification__content::before {
    content: $app-icon-check-filled;
    color: var(--app-color--success1);
  }
}

.app-banner-notification__style--warning {
  & .app-banner-notification__wrapper {
    background-image: linear-gradient(
      to bottom,
      var(--app-color--white) 50%,
      var(--app-color--caution2) 50%
    );
  }

  & .app-banner-notification__content::before {
    content: $app-icon-warning-filled;
    color: var(--app-color--caution2);
  }
}

.app-banner-notification__style--error {
  & .app-banner-notification__wrapper {
    background-image: linear-gradient(
      to bottom,
      var(--app-color--white) 50%,
      var(--app-color--error1) 50%
    );
  }

  & .app-banner-notification__content::before {
    content: $app-icon-warning-filled;
    color: var(--app-color--error1);
  }
}

.app-banner-notification__wrapper {
  padding: fds-rem(15px 30px 23px);
  margin-top: fds-rem(-9999px);
  width: 100%;
  background-size: 100% 200%;
  background-position: 0%
    calc(0% - #{$app-banner-notification__bottom-border--height});
  animation-duration: 0.6s;
  animation-timing-function: linear;
}

a.app-banner-notification__wrapper {
  display: block;

  & .app-banner-notification__text::after {
    font-family: $app-icons;
    font-size: fds-rem(16px);
    line-height: 1;
    font-style: normal;
    font-weight: 400;
    font-variant: normal;
    text-transform: none;
    letter-spacing: normal;
    font-feature-settings: normal;
    -webkit-font-smoothing: antialiased;
    -moz-osx-font-smoothing: grayscale;
    content: $app-icon-chevron-right;
    font-size: fds-rem(12px);
    padding-left: fds-rem(15px);
  }
}

.app-banner-notification__content {
  padding-right: fds-rem(25px);
  animation-duration: 0.6s;
  animation-timing-function: linear;
  display: flex;
  align-items: top;

  &::before {
    font-family: $app-icons;
    font-size: fds-rem(16px);
    line-height: 1;
    font-style: normal;
    font-weight: 400;
    font-variant: normal;
    text-transform: none;
    letter-spacing: normal;
    font-feature-settings: normal;
    -webkit-font-smoothing: antialiased;
    -moz-osx-font-smoothing: grayscale;
    font-size: fds-rem(22px);
    line-height: fds-rem(18px);
    padding-right: fds-rem(15px);
    animation-duration: 0.6s;
    animation-timing-function: linear;
  }
}

// .app-banner-notification__text {
//   // @extend %fmc-type--body1;
// }

.app-banner-notification__close-button {
  position: absolute;
  top: fds-rem(16px);
  right: fds-rem(18px);
  font-size: fds-rem(16px);
  line-height: fds-rem(18px);
  padding: fds-rem(12px);

  &::before {
    animation-duration: 0.6s;
    animation-timing-function: linear;
  }
}

@keyframes app-banner-notification__wrapper--show {
  0% {
    margin-top: fds-rem(-58px);
    background-position: 0% 0%;
  }

  50% {
    margin-top: 0;
    background-position: 0% -100%;
  }

  100% {
    background-position: 0%
      calc(-200% - #{$app-banner-notification__bottom-border--height});
  }
}

@keyframes app-banner-notification__wrapper--hide {
  0% {
    margin-top: 0;
  }

  100% {
    margin-top: fds-rem(-58px);
  }
}

@keyframes app-banner-notification__wrapper--information {
  0% {
    background-image: linear-gradient(
      to bottom,
      var(--app-color--primary) 50%,
      var(--app-color--primary) 50%
    );
  }
}

@keyframes app-banner-notification__wrapper--success {
  0% {
    background-image: linear-gradient(
      to bottom,
      var(--app-color--success1) 50%,
      var(--app-color--success1) 50%
    );
  }
}

@keyframes app-banner-notification__wrapper--warning {
  0% {
    background-image: linear-gradient(
      to bottom,
      var(--app-color--caution2) 50%,
      var(--app-color--caution2) 50%
    );
  }
}

@keyframes app-banner-notification__wrapper--error {
  0% {
    background-image: linear-gradient(
      to bottom,
      var(--app-color--error1) 50%,
      var(--app-color--error1) 50%
    );
  }
}

@keyframes app-banner-notification__icon-and-text {
  0% {
    opacity: 0;
    color: var(--app-color--white);
  }

  50% {
    opacity: 0;
  }

  51% {
    opacity: 1;
  }

  75% {
    color: var(--app-color--white);
  }
}
</style>
