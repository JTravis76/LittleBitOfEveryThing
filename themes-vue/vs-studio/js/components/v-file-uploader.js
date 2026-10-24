import IconDownload from "./icon-download.js";

export default {
  template: `
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

  <div class="box__uploading">Uploading…</div>
  <div class="box__success">
    Done! <a href="#" class="box__restart" role="button">Upload more?</a>
  </div>
  <div class="box__error">
    Error! <span></span>.
    <a href="#" class="box__restart" role="button">Try again!</a>
  </div>
  <!-- <slot :dropAzoneActive="active"></slot> -->
</div>
`,
  name: "v-file-uploader",
  components: {
    IconDownload,
  },
  data() {
    return {
      active: false,
    };
  },
  emits: ["files"],
  mounted() {
    // this.events.forEach((name) => {
    //   document.body.addEventListener(name, this.preventDefaults);
    // });
  },
  unmounted() {
    events.forEach((name) => {
      document.body.removeEventListener(name, this.preventDefaults);
    });
  },
  methods: {
    onDrop(e) {
      const files = e.dataTransfer?.files;
      if (files) {
        this.$emit("files", files);
      }
    },
    preventDefaults(e) {
      e.preventDefault();
    },
    setActive(state) {
      if (!state) {
        window.setTimeout(() => {
          this.active = state;
        }, 50);
      } else {
        this.active = state;
      }
    },
  },
};
