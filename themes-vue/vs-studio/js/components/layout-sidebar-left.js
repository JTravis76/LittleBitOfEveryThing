import LayoutDockingPanel from "./layout-docking-panel.js";

export default {
  name: "LayoutSideBarLeft",
  template: `
  <aside class="left-sidebar"> 
    <LayoutDockingPanel>Server Explorer</LayoutDockingPanel> 
    <LayoutDockingPanel>Server Object Explorer</LayoutDockingPanel> 
  </aside>`,
  components: { LayoutDockingPanel },
};
