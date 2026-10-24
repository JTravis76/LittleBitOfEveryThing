<template>
  <svg
    :width="size"
    :height="size"
    viewBox="0 0 48 48"
    version="1.1"
    xml:space="preserve"
    inkscape:version="1.2.2 (732a01da63, 2022-12-09)"
    sodipodi:docname="html-file.svg"
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
      inkscape:cx="215.67229"
      inkscape:cy="134.11201"
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
        style="fill: #595959; fill-opacity: 1; stroke-width: 0.386292"
        d="m 34.994534,45.082795 1.508863,-1.545221 h 3.205191 3.205164 V 28.664918 13.792273 H 37.891825 32.869872 V 8.7703461 3.7484216 H 21.087657 9.3054274 V 15.597975 27.44754 L 8.3593182,26.558739 C 7.8138978,26.046334 7.1594927,25.767281 6.8141168,25.899809 6.2808298,26.10444 6.215,24.822689 6.215,14.233819 V 2.3378846 L 7.1090926,1.4979444 C 7.9918723,0.66860254 8.173735,0.658 21.516094,0.658 h 13.512922 l 5.487566,5.5347457 5.4876,5.5347493 v 16.610319 16.610305 l -0.894093,0.839948 C 44.280722,46.56722 43.827699,46.628 38.85085,46.628 h -5.365167 z"
      />
      <path
        style="fill: #000000; stroke-width: 0.398983"
        d="m 4.2559933,43.16712 c -4.74711574,-4.769217 -4.74441534,-5.036684 0.099463,-9.858123 l 3.5821676,-3.565549 1.213789,1.162861 1.2137951,1.162903 -3.0909658,3.109173 -3.0909607,3.109203 3.0909607,3.109214 3.0909658,3.109173 -1.1272851,1.080012 C 8.6179074,46.179998 8.0269341,46.666 7.9246515,46.666 7.8224211,46.666 6.171477,45.091494 4.2559863,43.16712 Z m 23.9374707,2.410678 -1.088253,-1.088254 3.082811,-3.100973 3.0828,-3.100983 -3.09097,-3.109203 -3.090956,-3.109173 1.214016,-1.1631 1.213999,-1.163112 3.781432,3.765711 c 5.011054,4.990223 5.012837,5.073917 0.204533,9.70916 -1.967301,1.896468 -3.721867,3.448129 -3.899049,3.448129 -0.177182,0 -0.811846,-0.489704 -1.410363,-1.088202 z M 16.41591,42.808595 c -3.078357,-1.517173 -3.533709,-6.017567 -0.838352,-8.285581 1.416321,-1.191776 4.074154,-1.390678 5.908315,-0.442199 3.331107,1.722583 2.95979,7.348147 -0.582682,8.828278 -1.740452,0.727192 -2.858677,0.702146 -4.487281,-0.100363 z m 3.5373,-3.353326 c 0.744269,-0.744269 0.769483,-0.914233 0.25157,-1.695649 -0.691841,-1.043772 -1.704173,-1.308821 -2.49431,-0.653054 -0.796844,0.661326 -0.727954,2.25173 0.118739,2.744981 0.985642,0.574066 1.192034,0.535531 2.123852,-0.396278 z"
      />
    </g>
  </svg>
</template>

<script lang="ts">
import { defineComponent } from "vue";

export default defineComponent({
  name: "IconHtmlFile",
  props: {
    size: {
      type: String,
      default: "48mm",
    },
  },
});
</script>

<style lang="scss"></style>
