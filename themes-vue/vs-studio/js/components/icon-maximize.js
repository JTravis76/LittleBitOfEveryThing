export default {
  name: "icon-maximize",
  template: `
  <svg
  :width="size"
  :height="size"
  viewBox="0 0 48 48"
  version="1.1"
  xml:space="preserve"
  inkscape:version="1.2.2 (732a01da63, 2022-12-09)"
  sodipodi:docname="maximize.svg"
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
  <defs/>
  <g inkscape:label="Layer 1" inkscape:groupmode="layer">
    <path
      style="fill: #4c5671; stroke-width: 1.1148; fill-opacity: 1"
      d="M 0.73612294,28.048791 V 8.5397397 H 4.6379338 8.5397407 V 4.6379328 0.73612168 H 28.048792 47.557839 V 20.245169 39.75422 h -3.901811 -3.901807 v 3.901808 3.901811 H 20.24517 0.73612294 Z m 35.67369006,0 V 11.884148 H 20.24517 4.0805311 v 16.164643 16.16464 H 20.24517 36.409813 Z m 7.803618,-7.803622 V 4.08053 H 28.048792 11.884149 V 6.310137 8.5397397 H 25.819185 39.754221 V 22.474776 36.409812 h 2.229603 2.229607 z"
    />
  </g>
</svg>
`,
  props: {
    size: {
      type: String,
      default: "48mm",
    },
  },
};
