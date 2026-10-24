<template>
  <div
    class="file-uploader"
    :data-active="active"
    @drop.prevent="onDrop"
    @dragenter.prevent="setActive(true)"
    @dragover.prevent="setActive(true)"
    @dragleave.prevent="setActive(false)"
  >
    <div class="box__input">
      <IconDownload class="box__icon"></IconDownload>
      <input
        type="file"
        name="files[]"
        id="file"
        class="box__file"
        data-multiple-caption="{count} files selected"
        multiple
      />
      <label for="file"
        ><strong>Choose a file</strong
        ><span class="box__dragndrop"> or drag it here</span>.</label
      >
      <button type="submit" class="box__button">Upload</button>
    </div>

    <!-- <div class="box__uploading">Uploading…</div> -->
    <!-- <div class="box__success">
      Done! <a href="#" class="box__restart" role="button">Upload more?</a>
    </div> -->
    <!-- <div class="box__error">
      Error! <span></span>.
      <a href="#" class="box__restart" role="button">Try again!</a>
    </div> -->
    <!-- <slot :dropAzoneActive="active"></slot> -->
  </div>
</template>

<script lang="ts">
import { defineComponent } from "vue";
import IconDownload from "./IconDownload.vue";

export default defineComponent({
  name: "FileUploader",
  components: {
    IconDownload,
  },
  data() {
    return {
      active: false,
      events: ["drop", "dragenter", "dragover", "dragleave"],
    };
  },
  emits: ["files"],
  mounted() {
    this.events.forEach((name) => {
      document.body.addEventListener(name, this.preventDefaults);
    });
  },
  unmounted() {
    this.events.forEach((name: string) => {
      document.body.removeEventListener(name, this.preventDefaults);
    });
  },
  methods: {
    onDrop(e: DragEvent) {
      const files = e.dataTransfer?.files;
      if (files) {
        this.$emit("files", files);
        this.setActive(false);
      }
    },
    preventDefaults(e: Event) {
      e.preventDefault();
    },
    setActive(state: boolean) {
      if (!state) {
        window.setTimeout(() => {
          this.active = state;
        }, 50);
      } else {
        this.active = state;
      }
    },
  },
});
</script>

<style lang="scss" scoped>
.file-uploader {
  max-width: 280px;
  font-size: 1.25rem;
  background-color: #c8dadf;
  position: relative;
  padding: 100px 20px;

  outline: 2px dashed #92b0b3;
  outline-offset: -10px;
  transition: outline-offset 0.15s ease-in-out, background-color 0.15s linear;

  &[data-active="true"] {
    background-color: #9ed5e4;
  }
}

.box__input {
  text-align: center;
}
</style>
