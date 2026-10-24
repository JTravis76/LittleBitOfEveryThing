<template>
  <div class="app-rpg">
    <div style="height: 30px; margin: 10px">
      Here is a role-playing-game written in JavaScript. Play
      <a href="/rpg/index.html" target="_blank">fullscreen.</a>
    </div>
    <iframe ref="iframe" src="/rpg/index.html" title="role playing game">
    </iframe>
  </div>
</template>

<script lang="ts">
import { defineComponent } from "vue";

export default defineComponent({
  name: "AppRpg",
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
});
</script>

<style lang="scss"></style>
