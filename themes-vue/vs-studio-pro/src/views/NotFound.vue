<template>
  <section>
    <div>
      <h1><span class="fa fa-file"></span> OOP! We broken a link.</h1>
      <h2>This page was not found.</h2>
      <p>&nbsp;</p>
      <router-link to="/">
        <span class="fa fa-home"></span> Take me home.
      </router-link>
    </div>
  </section>
</template>

<script lang="ts">
import { defineComponent } from "vue";

export default defineComponent({
  name: "NotFound",
});
</script>

<style lang="scss"></style>
