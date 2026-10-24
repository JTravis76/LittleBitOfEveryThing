<template>
  <div class="row">
    <div class="eight columns">
      <ToggleSwitch
        :state="selected"
        @selection="($event) => (selected = $event)"
      ></ToggleSwitch>
    </div>
    <div class="four columns">
      <fieldset>
        <legend>Info</legend>
        <p>This story displays how to use the toggle switch component.</p>
        <p>
          Sets the toggled state from an external source.
          <label>
            <input v-model="selected" type="checkbox" />
            <span> Off/On</span>
          </label>
        </p>
      </fieldset>
    </div>
  </div>
</template>

<script lang="ts">
import { defineComponent } from "vue";
import ToggleSwitch from "../components/VToggleSwitch.vue";

export default defineComponent({
  name: "ToggleSwitchStory",
  components: {
    ToggleSwitch,
  },
  data() {
    return {
      selected: false,
    };
  },
});
</script>

<style lang="scss"></style>
