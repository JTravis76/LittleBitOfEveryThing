<template>
  <li class="dropdown">
    <a class="dropdown-toggle" data-toggle="dropdown" href="#">
      <i class="fa fa-envelope"></i> Messages <span class="badge">5</span>
      <b class="caret"></b>
    </a>
    <ul class="dropdown-menu">
      <li><a href="#">New message</a></li>
      <li><a href="#">Inbox</a></li>
      <li><a href="#">Out box</a></li>
      <li><a href="#">Trash</a></li>
    </ul>
  </li>
</template>

<script lang="ts">
import { defineComponent } from "vue";

export default defineComponent({
  name: "NavMessage",
});
</script>

<style lang="scss"></style>
