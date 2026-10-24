import TopBanner from "../components/v-top-banner.js";

export default {
  template: `
<div class="row">
  <div class="col-lg-8">
    <TopBanner
      banner-style="dismissible"
      banner-type="info"
      message="Hello World"
      sub-message=""
      :hide="false"
    ></TopBanner>
  </div>
  <div class="col-lg-4">
    <fieldset>
      <legend>Info</legend>
      This story demos the top banner component.
    </fieldset>
  </div>
<div>`,
  name: "top-banner-story",
  components: {
    TopBanner,
  },
};
