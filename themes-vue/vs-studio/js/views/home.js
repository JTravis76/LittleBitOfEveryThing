import LayoutPanel from "../components/layout-panel.js";
import LayoutEditorTab from "../components/layout-editor-tab.js";
import LayoutEditor from "../components/layout-editor.js";

export default {
  name: "TheHome",
  template: ` 
  <div class="main-content"> 
    <div class="editor"> 
      <LayoutEditorTab></LayoutEditorTab> 
      <LayoutEditor></LayoutEditor> 
    </div>
    <div> 
      <LayoutPanel></LayoutPanel> 
    </div> 
  </div>`,
  components: { LayoutPanel, LayoutEditorTab, LayoutEditor },
};
