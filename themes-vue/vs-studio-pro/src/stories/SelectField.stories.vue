<template>
  <div class="row">
    <div class="eight columns">
      <SelectField
        ref="selectfield"
        :options="optionList"
        :selected="selectedValue"
        :disabled="disabled"
        placeholder="Favorite Color"
        @selection="setSelection($event)"
      ></SelectField>
    </div>
    <div class="four columns">
      <fieldset>
        <legend>Info</legend>
        This story displays how to use the select field component to set / get
        data selected by the user.
      </fieldset>
      <fieldset>
        <legend>Interactive States</legend>
        <input v-model="disabled" type="checkbox" /> Disabled
      </fieldset>
      <fieldset>
        <legend>Output</legend>
        <label>Value selected:</label> {{ selectedValue }}
      </fieldset>
    </div>
  </div>
</template>

<script lang="ts">
import { defineComponent } from "vue";
import SelectField, { ISelect } from "../components/VSelectField.vue";

export default defineComponent({
  name: "SelectFieldStory",
  components: {
    SelectField,
  },
  data() {
    return {
      optionList: new Array<ISelect>(),
      selectedValue: "",
      disabled: false,
    };
  },
  watch: {
    disabled(state: boolean) {
      // eslint-disable-next-line
      (this.$refs.selectfield as any).modifiers["select-field--disabled"] =
        state;
    },
  },
  created() {
    this.optionList = [
      { value: "0", text: "", selected: false, disabled: false },
      { value: "1", text: "Green", selected: false, disabled: false },
      { value: "2", text: "Red", selected: false, disabled: false },
      { value: "3", text: "Blue", selected: false, disabled: false },
      { value: "4", text: "Yellow", selected: false, disabled: false },
    ];
  },
  methods: {
    setSelection(opt: ISelect) {
      this.selectedValue = opt.value;
    },
  },
});
</script>

<style lang="scss"></style>
