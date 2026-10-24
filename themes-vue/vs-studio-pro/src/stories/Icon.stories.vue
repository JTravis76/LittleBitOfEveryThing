<template>
  <div class="row">
    <div class="eight columns">
      <Icon icon="copyright"></Icon> {{ new Date().getFullYear().toString() }}
    </div>
    <div class="four columns">
      <fieldset>
        <legend>Info</legend>
        <p>
          This story shows the use of the icon components which fetches from
          'iconify.design'.
        </p>
      </fieldset>
    </div>
  </div>
</template>

<script lang="ts">
import { defineComponent } from "vue";
import Icon from "../components/VIcon.vue";

export default defineComponent({
  name: "IconStory",
  components: {
    Icon,
  },
});
</script>

<style lang="scss"></style>
