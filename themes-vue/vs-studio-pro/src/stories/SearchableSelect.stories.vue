<template>
  <div class="row">
    <div class="eight columns">
      <SearchableSelect
        :options="options"
        @selection="selected($event)"
      ></SearchableSelect>
    </div>
    <div class="four columns">
      <fieldset>
        <legend>Info</legend>
        <p>This story demostrates the searchable dropdown.</p>
      </fieldset>
      <fieldset>
        <legend>Selected Results</legend>
        Value: {{ option.value }}
        <br />
        Text: {{ option.text }}
      </fieldset>
    </div>
  </div>
</template>

<script lang="ts">
import { defineComponent } from "vue";
import SearchableSelect, {
  ISearchableSelectOption,
} from "../components/VSearchableSelect.vue";

export default defineComponent({
  name: "SearchableSelectStory",
  components: {
    SearchableSelect,
  },
  data() {
    return {
      options: new Array<ISearchableSelectOption>(),
      option: { text: "", value: "" },
    };
  },
  created() {
    for (let i = 0; i <= 30; i += 1) {
      this.options.push({ value: i.toString(), text: `Item${i}` });
    }
  },
  methods: {
    selected(option: ISearchableSelectOption) {
      this.option = option;
    },
  },
});
</script>

<style lang="scss"></style>
