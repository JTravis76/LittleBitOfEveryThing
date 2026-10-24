<template>
  <div class="row">
    <div class="eight columns">
      <DialWheel></DialWheel>
    </div>
    <div class="four columns"></div>
  </div>
</template>

<script lang="ts">
import { defineComponent } from "vue";
import DialWheel from "../components/VDialWheel.vue";

export default defineComponent({
  name: "DialWheelStory",
  components: {
    DialWheel,
  },
});
</script>

<style lang="scss"></style>
