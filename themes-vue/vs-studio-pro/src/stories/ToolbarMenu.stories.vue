<template>
  <div class="row">
    <div class="eight columns">
      <div class="u-pull-right">
        <ToolbarMenu :controls="items" @action="doAction($event)"></ToolbarMenu>
      </div>
    </div>
    <div class="four columns">
      <fieldset>
        <legend>Info</legend>
        <p>This story demos the tool bar menu component.</p>
        <p>
          Menu items are build from an array providing the button title, icon,
          and disable state.
        </p>
      </fieldset>
    </div>
  </div>
</template>

<script lang="ts">
import { defineComponent } from "vue";
import ToolbarMenu, { IToolbarMenu } from "../components/VToolbarMenu.vue";

export default defineComponent({
  name: "ToolbarMenuStory",
  components: {
    ToolbarMenu,
  },
  data() {
    return {
      items: [
        { title: "btn 1", icon: "addNew", disabled: true },
        { title: "btn 2", icon: "equipmentconfig", disabled: false },
        { title: "btn 3", icon: "manage", disabled: false },
        { title: "btn 4", icon: "approve", disabled: true },
        { title: "btn 5", icon: "review", disabled: false },
      ] as IToolbarMenu[],
    };
  },
  methods: {
    doAction(name: string) {
      console.log(name);
    },
  },
});
</script>

<style lang="scss"></style>
