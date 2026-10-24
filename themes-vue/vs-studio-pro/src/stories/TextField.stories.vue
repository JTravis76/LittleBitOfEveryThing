<template>
  <div class="row">
    <div class="eight columns">
      <TextField v-model="firstName" placeholder="First Name"></TextField>
      <TextField v-model="lastName" placeholder="Last Name"></TextField>
    </div>
    <div class="four columns">
      <fieldset>
        <legend>Info</legend>
        <p>This story displays how to use the text field component.</p>
        <p>
          Enter text in the field below to simulate data provided from a server.
          <input v-model="firstName" type="text" placeholder="first name" />
        </p>
      </fieldset>
    </div>
  </div>
</template>

<script lang="ts">
import { defineComponent } from "vue";
import TextField from "../components/VTextField.vue";

export default defineComponent({
  name: "TextFieldStory",
  components: {
    TextField,
  },
  data() {
    return {
      firstName: "",
      lastName: "",
    };
  },
});
</script>

<style lang="scss"></style>
