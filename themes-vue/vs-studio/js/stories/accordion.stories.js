import Accordion from "../components/v-accordion.js";

export default {
  template: `
<div class="row">
  <div class="col-md-8">
    <Accordion
      title="Panel 1"
      :expanded="expand"
      @toggle="($event) => (expand = $event)"
    >
      <div>
        <p>Hello World!</p>
      </div>
    </Accordion>
  </div>
  <div class="col-md-4">
    <fieldset>
      <legend>Info</legend>
      This story shows how to the accordion panels with collpase and expand
      actions.
    </fieldset>
  </div>
</div>
`,
  name: "accordion-story",
  components: {
    Accordion,
  },
  data() {
    return {
      expand: false,
    };
  },
};
