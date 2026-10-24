<template>
  <div class="row">
    <div class="eight columns">
      <FileUploader @files="process($event)"></FileUploader>
    </div>
    <div class="four columns">
      <fieldset>
        <legend>Info</legend>
        <p>This story displays how to use the file uploader component.</p>
        <p>
          <b>NOTE:</b> Currently the drag-n-drop feature has been implemented.
          Fallback to file selector is WIP.
        </p>
      </fieldset>
    </div>
  </div>
</template>

<script lang="ts">
import { defineComponent } from "vue";
import FileUploader from "../components/VFileUploader.vue";

export default defineComponent({
  name: "FileUploaderStory",
  components: {
    FileUploader,
  },
  methods: {
    process(files: FileList | undefined) {
      console.log(files);
    },
  },
});
</script>

<style lang="scss"></style>
