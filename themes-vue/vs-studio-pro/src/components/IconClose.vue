<template>
  <svg
    xmlns:inkscape="http://www.inkscape.org/namespaces/inkscape"
    xmlns:sodipodi="http://sodipodi.sourceforge.net/DTD/sodipodi-0.dtd"
    xmlns="http://www.w3.org/2000/svg"
    xmlns:svg="http://www.w3.org/2000/svg"
    xmlns:rdf="http://www.w3.org/1999/02/22-rdf-syntax-ns#"
    xmlns:cc="http://creativecommons.org/ns#"
    xmlns:dc="http://purl.org/dc/elements/1.1/"
    :width="size"
    :height="size"
    viewBox="0 0 48 48"
    version="1.1"
    inkscape:version="1.2.2 (732a01da63, 2022-12-09)"
    sodipodi:docname="close.svg"
  >
    <title>close</title>
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
      inkscape:cx="-50.239891"
      inkscape:cy="170.33998"
      inkscape:window-width="1720"
      inkscape:window-height="1361"
      inkscape:window-x="-9"
      inkscape:window-y="-9"
      inkscape:window-maximized="1"
      inkscape:current-layer="layer1"
    />
    <defs />
    <g inkscape:label="Layer 1" inkscape:groupmode="layer">
      <g transform="matrix(4.0143492,0,0,4.0143492,-6.0876188,-6.0276188)">
        <path
          d="m 8.207,7.5 5.147,5.146 -0.708,0.708 L 7.5,8.207 2.354,13.354 1.646,12.646 6.793,7.5 1.646,2.354 2.354,1.646 7.5,6.793 12.646,1.646 13.354,2.354 Z"
          style="opacity: 1"
        />
      </g>
    </g>
    <metadata>
      <rdf:RDF>
        <cc:Work rdf:about="">
          <dc:title>close</dc:title>
        </cc:Work>
      </rdf:RDF>
    </metadata>
  </svg>
</template>

<script lang="ts">
import { defineComponent } from "vue";

export default defineComponent({
  name: "IconClose",
  props: {
    size: {
      type: String,
      default: "48mm",
    },
  },
});
</script>

<style lang="scss"></style>
