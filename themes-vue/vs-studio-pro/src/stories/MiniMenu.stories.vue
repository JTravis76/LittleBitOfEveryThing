<template>
  <div class="row">
    <div class="eight columns">
      <div style="height: 50px">&nbsp;</div>
      <MiniMenu :menu="items"></MiniMenu>
    </div>
    <div class="four columns">
      <fieldset>
        <legend>Info</legend>
        <p>This story demos the mini menu component.</p>
        <p>
          Upon mouse-over, a small menu will appear. The menu itself is design
          for parent and child relationship. Hovering over a parent item will
          expand the child items.
        </p>
      </fieldset>
    </div>
  </div>
</template>

<script lang="ts">
import { defineComponent } from "vue";
import MiniMenu, { IMiniMenu } from "../components/VMiniMenu.vue";

export default defineComponent({
  name: "MiniMenuStory",
  components: {
    MiniMenu,
  },
  data() {
    return {
      items: [
        { title: "Title 1", expanded: false, pages: ["Page 1"] },
        { title: "Title 2", expanded: false, pages: ["Page 2", "Page 3"] },
      ] as IMiniMenu[],
    };
  },
});
</script>

<style lang="scss"></style>
