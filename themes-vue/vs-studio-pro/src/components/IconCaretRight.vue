<template>
  <svg
    :width="size"
    :height="size"
    viewBox="0 0 48 48"
    version="1.1"
    xml:space="preserve"
    inkscape:version="1.2.2 (732a01da63, 2022-12-09)"
    sodipodi:docname="caret-right.svg"
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
      inkscape:cx="117.12732"
      inkscape:cy="108.80355"
      inkscape:window-width="1443"
      inkscape:window-height="1022"
      inkscape:window-x="1895"
      inkscape:window-y="145"
      inkscape:window-maximized="0"
      inkscape:current-layer="layer1"
    />
    <defs />
    <g inkscape:label="Layer 1" inkscape:groupmode="layer">
      <path
        style="stroke-width: 1.38235"
        d="M 11.339,24.072001 V 0.572 h 1.067284 1.067285 L 25.162532,12.322004 36.851491,24.072001 25.162532,35.821999 13.473569,47.572 H 12.406284 11.339 Z M 22.72959,15.446119 14.103707,6.8202368 V 24.072001 41.323765 l 8.625883,-8.625882 8.625881,-8.625882 z"
      />
    </g>
  </svg>
</template>

<script lang="ts">
import { defineComponent } from "vue";

export default defineComponent({
  name: "IconCaretRight",
  props: {
    size: {
      type: String,
      default: "48mm",
    },
  },
});
</script>

<style lang="scss"></style>
