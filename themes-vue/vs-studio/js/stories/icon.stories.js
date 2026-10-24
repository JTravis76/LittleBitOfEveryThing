import Icon from "../components/v-icon.js";

export default {
  template: `
<div class="row">
  <div class="col-lg-8">
    <Icon icon="material-symbols:thumb-up"></Icon>
  </div>
  <div class="col-lg-4">
    <fieldset>
      <legend>Info</legend>
      This story shows the use of the icon components which fetches from 'iconify.design'.
    </fieldset>
  </div>
</div>`,
  name: "icon-story",
  components: {
    Icon,
  },
};
