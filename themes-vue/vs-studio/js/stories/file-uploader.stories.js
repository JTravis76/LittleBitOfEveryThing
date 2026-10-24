import FileUploader from "../components/v-file-uploader.js";

export default {
  template: `
  <div class="row">
  <div class="col-md-8">
    <FileUploader></FileUploader>
  </div>
  <div class="col-md-4">
    <fieldset>
      <legend>Info</legend>
      This story displays how to use the file uploader component.
    </fieldset>
    <fieldset>
      <legend>??</legend>
    </fieldset>
  </div>
</div>
`,
  name: "file-uploader-story",
  components: {
    FileUploader,
  },
};
