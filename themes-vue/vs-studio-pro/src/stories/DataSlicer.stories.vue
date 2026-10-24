<template>
  <div class="row">
    <div class="eight columns">
      <template v-for="slicer in slicers" :key="slicer.title">
        <DataSlicer :slicer="slicer" @selection="output($event)"></DataSlicer>
      </template>
    </div>
    <div class="four columns">
      <fieldset>
        <legend>Info</legend>
        <p>This story demostrates the used of the data slicer component.</p>
        <p>
          Data slicer are meant to assist in data manipulation by filtering the
          master dataset. They can be either a single input field or a
          collection of checkboxes.
        </p>
      </fieldset>
    </div>
  </div>
</template>

<script lang="ts">
import { defineComponent } from "vue";
import DataSlicer, { ISlicer, Slicer } from "../components/VDataSlicer.vue";

export default defineComponent({
  name: "DataSlicerStory",
  components: {
    DataSlicer,
  },
  data() {
    return {
      slicers: new Array<ISlicer>(),
    };
  },
  created() {
    this.slicers.push(new Slicer({ title: "GRP 1", type: "input" }));
    this.slicers.push(
      new Slicer({
        title: "GRP 2",
        type: "checkbox",
        data: [
          { text: "checkbox 1", value: "1", selected: false },
          { text: "checkbox 2", value: "2", selected: false },
          { text: "checkbox 3", value: "3", selected: false },
          { text: "checkbox 4", value: "4", selected: false },
        ],
      })
    );
  },
  methods: {
    output(e: any) {
      console.log(e);
    },
  },
});
</script>

<style lang="scss"></style>
