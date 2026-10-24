<template>
  <svg
    :width="size"
    :height="size"
    viewBox="0 0 48 48"
    version="1.1"
    xml:space="preserve"
    inkscape:version="1.2.2 (732a01da63, 2022-12-09)"
    sodipodi:docname="folder-closed.svg"
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
      inkscape:zoom="1.1893044"
      inkscape:cx="7.9878625"
      inkscape:cy="197.17408"
      inkscape:window-width="1720"
      inkscape:window-height="1361"
      inkscape:window-x="-9"
      inkscape:window-y="-9"
      inkscape:window-maximized="1"
      inkscape:current-layer="layer1"
    />
    <!-- <defs>
      <style>
        .light-yellow-10 {
          fill: #996f00;
          opacity: 0.1;
        }
        .light-yellow {
          fill: #996f00;
          opacity: 1;
        }
      </style>
    </defs> -->
    <g inkscape:label="Layer 1" inkscape:groupmode="layer">
      <g transform="matrix(3,0,0,3,0.23,-0.092)" style="display: inline">
        <g style="display: inline">
          <path
            class="light-yellow-10"
            d="m 14.5,3.5 v 9 h -13 v -10 h 6 l 1,1 z"
            style="display: inline"
          />
          <path
            class="light-yellow"
            d="M 14.5,3 H 8.707 L 7.854,2.146 7.5,2 h -6 L 1,2.5 v 10 L 1.5,13 h 13 L 15,12.5 v -9 z M 7.293,3 7.781,3.488 7.268,4 H 2 V 3 Z M 14,12 H 2 V 5 H 7.475 L 7.829,4.854 8.682,4 H 14 Z"
            style="display: inline"
          />
        </g>
      </g>
    </g>
  </svg>
</template>

<script lang="ts">
import { defineComponent } from "vue";

export default defineComponent({
  name: "IconFolderClosed",
  props: {
    size: {
      type: String,
      default: "48mm",
    },
  },
});
</script>

<style lang="scss" scoped>
.light-yellow-10 {
  fill: #996f00;
  opacity: 0.1;
}
.light-yellow {
  fill: #996f00;
  opacity: 1;
}
</style>
