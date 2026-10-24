import LayoutHeader from "./components/layout-header.js";
import LayoutFooter from "./components/layout-footer.js";
import LayoutSidebarLeft from "./components/layout-sidebar-left.js";
import LayoutSidebarRight from "./components/layout-sidebar-right.js";

export default {
  name: "TheApp",
  components: {
    LayoutHeader,
    LayoutFooter,
    LayoutSidebarLeft,
    LayoutSidebarRight,
  },
  template: `
  <div> 
    <LayoutHeader></LayoutHeader> 
    <main> 
      <router-view></router-view> 
    </main> 
    <LayoutSidebarLeft></LayoutSidebarLeft> 
    <LayoutSidebarRight></LayoutSidebarRight> 
    <LayoutFooter></LayoutFooter> 
  </div>`,
  // created() {
  //   fetch("./data/menuItems.json", {
  //     method: "GET",
  //   }).then(async (resp) => {
  //     const txt = await resp.json();
  //     console.log(txt);
  //     return txt;
  //   });
  // },
};
