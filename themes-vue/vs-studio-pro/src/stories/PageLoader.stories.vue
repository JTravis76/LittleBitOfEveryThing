<template>
  <div class="row">
    <div class="eight columns">
      <PageLoader></PageLoader>
      <button type="button" class="button" @click="toggleLoader()">
        Show page loader
      </button>
    </div>
    <div class="four columns">
      <fieldset>
        <legend>Info</legend>
        <p>This story demos the use of the page loader component.</p>
        <p><b>Note:</b> This component makes use of the Pinia store.</p>
        <p>
          <small
            >Tip: you can close the loader by clicking the hidden X in the
            upper-left corner.</small
          >
        </p>
      </fieldset>
    </div>
  </div>
</template>

<script lang="ts">
import { defineComponent } from "vue";
import PageLoader from "../components/LayoutPageLoader.vue";
import { useSiteStore } from "../stores/siteStore";

export default defineComponent({
  name: "PageLoaderStory",
  components: {
    PageLoader,
  },
  methods: {
    toggleLoader() {
      useSiteStore().pageLoader(true);
    },
  },
});
</script>

<style lang="scss"></style>
