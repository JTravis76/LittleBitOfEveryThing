import { letterAvatar } from "../lib/letter-avatar.js";

export default {
  name: "v-avatar",
  template: `
  <img
    :title="name"
    :src="src"
    :style="{ 'border-radius': borderRadius }"
    :alt="name"
  />`,
  props: {
    name: {
      type: String,
      default: "?",
    },
    size: {
      type: Number,
      default: 100,
    },
    round: {
      type: Boolean,
      default: false,
    },
    url: {
      type: [String, undefined],
      default: undefined,
    },
  },
  data() {
    return {
      borderRadius: "0%",
      src: "",
    };
  },
  watch: {
    name(n) {
      this.src = this.url ?? letterAvatar(n, this.size);
    },
  },
  created() {
    this.borderRadius = this.round ? "50%" : "0%";
    this.src = this.url ?? letterAvatar(this.name, this.size);
  },
};
