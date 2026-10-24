import SearchableSelect from "../components/v-searchable-select.js";

export default {
  template: `
<div class="row">
  <div class="col-md-8">
    <SearchableSelect
      :options="options"
      @selection="submit($event)"
    ></SearchableSelect>
  </div>
  <div class="col-md-4">
    <fieldset>
      <legend>Info</legend>
      <p>This story demostrates the searchable select.</p>
    </fieldset>
    <fieldset>
      <legend>Selected Results</legend>
      Value: {{ option.value }}
      <br />
      Text: {{ option.text }}
    </fieldset>
  </div>
</div>`,
  name: "searchable-select-story",
  components: {
    SearchableSelect,
  },
  data() {
    return {
      options: [{ value: "1", text: "Item 1" }],
      option: { text: "", value: "" },
    };
  },
  created() {
    this.options = [];
    for (let i = 1; i <= 30; i += 1) {
      this.options.push({ value: i.toString(), text: `Item${i}` });
    }
  },
  methods: {
    submit(option) {
      this.option = option;
    },
  },
};
