import LayoutMainMenu from "./layout-main-menu.js";
import IconVisualStudio from "./icon-visual-studio.js";
import IconMinimize from "./icon-minimize.js";
import IconMaximize from "./icon-maximize.js";
import IconClose from "./icon-close.js";

export default {
  name: "LayoutHeader",
  template: `
  <header> 
    <IconVisualStudio class="icon"></IconVisualStudio> 
    <LayoutMainMenu></LayoutMainMenu> 
    <input text="text" class="search-box" placeholder="Search (Ctrl+Q)" /> 
    <div class="project-box"> Portfolio </div>
    <div class="application-bar">
      <div class="avatar">JT</div>
      <div class="application-controls">
        <a href="#" title="minimize">
          <IconMinimize :size="12" style="margin: 2px 8px;"></IconMinimize>
        </a>
        <a href="#" title="maximize">
          <IconMaximize :size="12" style="margin: 2px 8px;"></IconMaximize>
        </a>
        <a href="#" title="close">
          <IconClose :size="12" style="fill: #4c5671; margin: 2px 8px;"></IconClose>
        </a>      
      </div>
    </div>
  </header>`,
  components: {
    LayoutMainMenu,
    IconVisualStudio,
    IconMinimize,
    IconMaximize,
    IconClose,
  },
};
