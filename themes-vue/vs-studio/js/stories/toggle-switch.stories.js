import ToggleSwitch from "../components/v-toggle-switch.js";

export default {
  template: `
<div class="row">
  <div class="col-md-8">
    <ToggleSwitch
      :state="selected"
      @selection="($event) => (selected = $event)"
    ></ToggleSwitch>
  </div>
  <div class="col-md-4">
    <fieldset>
      <legend>Info</legend>
      This story displays how to use the toggle switch component.
    </fieldset>
    <fieldset>
      <legend>Options</legend>
      <p>Sets the toggled state from an external source.</p>
      <input v-model="selected" type="checkbox" /> off/on state
    </fieldset>
  </div>
</div>
`,
  name: "toggle-switch-story",
  components: {
    ToggleSwitch,
  },
  data() {
    return {
      selected: false,
    };
  },
};
