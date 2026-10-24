<template>
  <div id="wrapper">
    <MainSidebar></MainSidebar>
    <section id="tools">
      <BreadCrumb></BreadCrumb>
      <div id="toolbar"><Toolbar></Toolbar></div>
    </section>
    <div id="content"><router-view></router-view></div>
  </div>
</template>

<script lang="ts">
import { defineComponent } from "vue";
import MainSidebar from "./MainSidebar.vue";
import BreadCrumb from "./BreadCrumbs.vue";
import Toolbar from "./Toolbar.vue";

export default defineComponent({
  name: "MainContent",
  components: {
    MainSidebar,
    BreadCrumb,
    Toolbar,
  },
});
</script>

<style lang="scss"></style>
