<template>
  <div class="row">
    <div class="eight columns">
      <RoadSign id="0" text="Checkout"></RoadSign>
    </div>
    <div class="four columns">
      <fieldset>
        <legend>Info</legend>
        <p></p>
      </fieldset>
    </div>
  </div>
</template>

<script lang="ts">
import { defineComponent } from "vue";
import RoadSign from "../components/VRoadSign.vue";

export default defineComponent({
  name: "RoadSignStory",
  components: {
    RoadSign,
  },
});
</script>

<style lang="scss"></style>
