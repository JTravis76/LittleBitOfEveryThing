export default {
  template: `
<div class="banner-notification" :class="modifiers" @click="toggle">
  <div class="banner-notification__wrapper">
    <span class="banner-notification__content">
      <span class="banner-notification__text" v-html="message"></span>
    </span>
    <div
      v-if="subMessage && bannerType === 'persistent' && expanded"
      class="mt-2 banner-notification__text"
      role="region"
      v-html="subMessage"
    ></div>
  </div>
  <button
    v-if="subMessage && bannerType === 'persistent'"
    type="button"
    class="banner-notification__close-button"
    :class="{ 'banner-expanded': expanded }"
    aria-label="Toggle notification"
    :aria-expanded="expanded"
  >
    <span class="icon icon-chevron-down"></span>
  </button>
  <button
    v-else-if="bannerType === 'dismissible'"
    type="button"
    class="banner-notification__close-button icon-clear icon"
    aria-label="Close notification"
    @click="dismiss"
  ></button>
</div>
`,
  name: "v-top-banner",
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
      type: [String | null],
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
        "banner-notification__type--dismissible": false,
        "banner-notification__type--persistent": false,
        "banner-notification__style--default": false,
        "banner-notification__style--success": false,
        "banner-notification__style--warning": false,
        "banner-notification__style--error": false,
        "banner-notification--show": !this.hide,
        "banner-notification--hide": this.hide,
      },
    };
  },
  watch: {
    hide: {
      immediate: true,
      deep: false,
      handler: function onHide(val) {
        this.modifiers["banner-notification--show"] = !val;
      },
    },
  },
  created() {
    this.modifiers["banner-notification__type--dismissible"] = false;
    this.modifiers["banner-notification__type--persistent"] = false;
    switch (this.bannerType) {
      case "dismissible":
        this.modifiers["banner-notification__type--dismissible"] = true;
        break;
      case "persistent":
        this.modifiers["banner-notification__type--persistent"] = true;
        break;
      default:
    }

    this.modifiers["banner-notification__style--default"] = false;
    this.modifiers["banner-notification__style--success"] = false;
    this.modifiers["banner-notification__style--warning"] = false;
    this.modifiers["banner-notification__style--error"] = false;
    switch (this.bannerStyle) {
      case "default":
        this.modifiers["banner-notification__style--default"] = true;
        break;
      case "success":
        this.modifiers["banner-notification__style--success"] = true;
        break;
      case "warning":
        this.modifiers["banner-notification__style--warning"] = true;
        break;
      case "error":
        this.modifiers["banner-notification__style--error"] = true;
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
};
