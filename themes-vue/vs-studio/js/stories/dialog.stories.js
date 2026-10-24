export default {
  template: `
<div class="row">
  <div class="col-lg-8">
    <h4>Dialogs<h4>
    <div>
      <button type="button" @click="openOkDlg()">ok dialog</button>
    </div>
    <div style="margin: 10px 0">
      <button type="button" @click="openYesNoDlg()">Yes No dialog</button>
    </div>
  </div>
  <div class="col-lg-4">
    <fieldset>
      <legend>Info</legend>
      This story demostrates the dialog component.
    </fieldset>
  </div>
</div>`,
  name: "dialog-story",
  methods: {
    openYesNoDlg() {
      this.$dialog({
        title: "Confirm?",
        message: "This is a confirmation dialog. Are you sure?",
        backdrop: true,
        type: "yesno",
      }).then((result) => console.log(result));
    },
    openOkDlg() {
      this.$dialog({
        title: "Message",
        message: "This is a message dialog.",
        backdrop: true,
        type: "ok",
      }).then((result) => console.log(result));
    },
  },
};
