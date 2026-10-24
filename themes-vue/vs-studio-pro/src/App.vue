<template>
  <div>
    <LayoutHeader></LayoutHeader>
    <main>
      <router-view></router-view>
    </main>
    <LayoutSidebarLeft></LayoutSidebarLeft>
    <LayoutSidebarRight></LayoutSidebarRight>
    <LayoutFooter></LayoutFooter>
  </div>
</template>

<script lang="ts">
import { defineComponent } from "vue";
import LayoutHeader from "./components/LayoutHeader.vue";
import LayoutFooter from "./components/LayoutFooter.vue";
import LayoutSidebarLeft from "./components/LayoutSidebarLeft.vue";
import LayoutSidebarRight from "./components/LayoutSidebarRight.vue";

export default defineComponent({
  name: "TheApp",
  components: {
    LayoutHeader,
    LayoutFooter,
    LayoutSidebarLeft,
    LayoutSidebarRight,
  },
});
</script>

<style lang="scss" scoped>
main {
  margin: 32px 25px 30px 30px;

  & .main-content {
    display: flex;
    justify-content: space-between;

    & .editor {
      background: inherit;
      width: 100%;
      margin-right: 4px;
    }
  }
}
</style>
