<template>
  <div class="app-resume">
    <div style="height: 30px; margin: 10px">
      <button type="button" title="print resume" @click="printMe()">
        <span class="fa fa-print"></span> Print
      </button>
    </div>
    <iframe ref="iframe" src="/resume.html" title="my resume"> </iframe>
  </div>
</template>

<script lang="ts">
import { defineComponent } from "vue";

export default defineComponent({
  name: "AppResume",
  mounted() {
    const iframe = this.$refs.iframe as HTMLIFrameElement;
    iframe.onload = function () {
      if (iframe.contentWindow) {
        const body = iframe.contentWindow.document.body;
        iframe.style.height = (body.scrollHeight + 20).toString() + "px";
        iframe.style.width = (body.scrollWidth + 20).toString() + "px";
      }
    };
  },
  methods: {
    printMe() {
      const iframe = this.$refs.iframe as HTMLIFrameElement;
      if (iframe.contentWindow) iframe.contentWindow.print();
    },
  },
});
</script>

<style lang="scss"></style>
