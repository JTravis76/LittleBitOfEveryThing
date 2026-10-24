export default {
  template: `<svg ref="svg"></svg>`,
  name: "v-icon",
  props: {
    icon: {
      type: String,
      default: "{prefix}:{name}",
    },
  },
  mounted() {
    fetch(`https://api.iconify.design/${this.icon.replace(":", "/")}.svg`)
      .then(async (resp) => {
        return await resp.text();
      })
      .then((d) => (this.$refs.svg.outerHTML = d));
  },
};
