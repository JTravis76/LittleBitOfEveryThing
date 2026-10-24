<template>
  <svg
    :width="size"
    :height="size"
    viewBox="0 0 48 48"
    version="1.1"
    xml:space="preserve"
    inkscape:version="1.2.2 (732a01da63, 2022-12-09)"
    sodipodi:docname="minimize.svg"
    xmlns:inkscape="http://www.inkscape.org/namespaces/inkscape"
    xmlns:sodipodi="http://sodipodi.sourceforge.net/DTD/sodipodi-0.dtd"
    xmlns="http://www.w3.org/2000/svg"
    xmlns:svg="http://www.w3.org/2000/svg"
  >
    <sodipodi:namedview
      pagecolor="#ffffff"
      bordercolor="#000000"
      borderopacity="0.25"
      inkscape:showpageshadow="2"
      inkscape:pageopacity="0.0"
      inkscape:pagecheckerboard="0"
      inkscape:deskcolor="#d1d1d1"
      inkscape:document-units="mm"
      showgrid="false"
      inkscape:zoom="1.6819304"
      inkscape:cx="5.3509943"
      inkscape:cy="91.858735"
      inkscape:window-width="1720"
      inkscape:window-height="1361"
      inkscape:window-x="-9"
      inkscape:window-y="-9"
      inkscape:window-maximized="1"
      inkscape:current-layer="layer1"
    />
    <defs />
    <g inkscape:label="Layer 1" inkscape:groupmode="layer">
      <path
        style="fill: #4c5671; stroke-width: 1.62068; fill-opacity: 1"
        d="M 0.593,24.161136 V 21.730102 H 24.092997 47.593 v 2.431034 2.431034 H 24.092997 0.593 Z"
      />
    </g>
  </svg>
</template>

<script lang="ts">
import { defineComponent } from "vue";

export default defineComponent({
  name: "IconMinimize",
  props: {
    size: {
      type: String,
      default: "48mm",
    },
  },
});
</script>

<style lang="scss"></style>
