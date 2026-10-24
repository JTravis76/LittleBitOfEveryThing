<template>
  <div class="row">
    <div class="eight columns">
      <h5>Dialogs</h5>
      <div>
        <button type="button" @click="openOkDlg()">Message dialog</button>
      </div>
      <div style="margin: 10px 0">
        <button type="button" @click="openYesNoDlg()">
          Confirmation dialog
        </button>
      </div>
    </div>
    <div class="four columns">
      <fieldset>
        <legend>Info</legend>
        <p>
          This story demostrates the dialog component. There are two types of
          dialogs; Message and Confirmation.
        </p>

        <div>
          <label>Dialog Result</label>
          <input type="text" v-model="result" />
        </div>
      </fieldset>
    </div>
  </div>
</template>

<script lang="ts">
import { defineComponent } from "vue";

export default defineComponent({
  name: "DialogStory",
  data() {
    return {
      result: "",
    };
  },
  methods: {
    openYesNoDlg() {
      this.$dialog({
        title: "Confirm?",
        message: "This is a confirmation dialog. Are you sure?",
        backdrop: true,
        type: "yesno",
      }).then((result) => (this.result = result));
    },
    openOkDlg() {
      this.$dialog({
        title: "Message",
        message: "This is a message dialog.",
        backdrop: true,
        type: "ok",
      }).then((result) => (this.result = result));
    },
  },
});
</script>

<style lang="scss"></style>
