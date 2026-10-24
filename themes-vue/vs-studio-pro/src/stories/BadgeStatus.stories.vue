<template>
  <div class="row">
    <div class="eight columns">
      <BadgeStatus class="new">New</BadgeStatus>
      <BadgeStatus class="draft">Draft</BadgeStatus>
      <BadgeStatus class="review">Review</BadgeStatus>
      <BadgeStatus class="pending">Pending</BadgeStatus>
      <BadgeStatus class="emergency">Emergency</BadgeStatus>
      <BadgeStatus class="complete">Complete</BadgeStatus>
    </div>
    <div class="four columns">
      <fieldset>
        <legend>Info</legend>
        <p>This story demostrate the use of the badge status component.</p>
      </fieldset>
    </div>
  </div>
</template>

<script lang="ts">
import { defineComponent } from "vue";
import BadgeStatus from "../components/VBadgeStatus.vue";

export default defineComponent({
  name: "BadgeStatusStory",
  components: {
    BadgeStatus,
  },
});
</script>

<style lang="scss"></style>
