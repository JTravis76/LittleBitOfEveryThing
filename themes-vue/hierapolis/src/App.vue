<template>
  <div>
    <NavBar></NavBar>
    <MainContent></MainContent>
  </div>
</template>

<script lang="ts">
import { defineComponent } from "vue";
import NavBar from "./components/NavBar.vue";
import MainContent from "./components/MainContent.vue";
import { BootstrapNative } from "./deps";

export default defineComponent({
  name: "TheApp",
  components: {
    NavBar,
    MainContent,
  },
  created() {
    console.log(BootstrapNative);
  },
});
</script>

<style lang="scss" scoped></style>
