<template>
  <div class="row">
    <div class="eight columns">
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
    <div class="four columns">
      <fieldset>
        <legend>Info</legend>
        This story shows how to the accordion panels with collpase and expand
        actions. Click the panel heading to perform the action.
      </fieldset>
    </div>
  </div>
</template>

<script lang="ts">
import { defineComponent } from "vue";
import Accordion from "../components/VAccordion.vue";

export default defineComponent({
  name: "AccordionStory",
  components: {
    Accordion,
  },
  data() {
    return {
      expand: false,
    };
  },
});
</script>

<style lang="scss"></style>
