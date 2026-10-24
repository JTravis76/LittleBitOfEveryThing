export default {
  template: `
<div class="toggle-switch--container">
  <div>Off</div>
  <input id="switch" v-model="localState" type="checkbox" name="switch" />
  <label for="switch"></label>
  <div>On</div>
</div>
`,
  name: "v-toggle-switch",
  props: {
    state: {
      type: Boolean,
      default: false,
    },
  },
  emits: ["selection"],
  data() {
    return {
      localState: this.state,
    };
  },
  watch: {
    state: {
      immediate: true,
      deep: false,
      handler: function onState(val) {
        this.localState = val;
        this.$emit("selection", val);
      },
    },
    localState(val) {
      this.$emit("selection", val);
    },
  },
};
