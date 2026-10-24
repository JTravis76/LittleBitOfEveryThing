export default {
  name: "icon-checked-in",
  template: `
  <svg
  :width="size"
  :height="size"
  viewBox="0 0 48 48"
  version="1.1"
  xml:space="preserve"
  inkscape:version="1.2.2 (732a01da63, 2022-12-09)"
  sodipodi:docname="checked-in.svg"
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
  <defs></defs>
  <g inkscape:label="Layer 1" inkscape:groupmode="layer">
    <g
      transform="matrix(5.75,0,0,5.75,-21.427,-27.611)"
      inkscape:highlight-color="#000000"
    >
      <g>
        <path
          d="M 10.5,8 H 10 V 7 A 2,2 0 0 0 6,7 V 8 H 5.5 L 5,8.5 v 4 L 5.5,13 h 5 L 11,12.5 v -4 z M 7,7 A 1,1 0 0 1 9,7 V 8 H 7 Z m 3,5 H 6 V 9 h 4 z"
        />
      </g>
    </g>
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
