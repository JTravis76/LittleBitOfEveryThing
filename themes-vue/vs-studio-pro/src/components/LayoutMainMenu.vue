<template>
  <div class="navbar">
    <a href="#"> File </a>
    <a href="#"> Edit </a>
    <a href="#"> Git </a>
    <a href="#"> Project </a>
    <a href="#"> Debug </a>
    <a href="#"> Analyze </a>
    <a href="#"> Tools </a>
    <a href="#"> Extensions </a>
    <a href="#"> Window </a>
    <a href="#"> Help </a>
  </div>
</template>

<script lang="ts">
import { defineComponent } from "vue";

export default defineComponent({
  name: "LayoutMainMenu",
});
</script>

<style lang="scss" scoped>
.navbar {
  a {
    float: left;
    display: block;
    border: 1px solid var(--purple3);
    color: var(--primary-font-color);
    text-align: center;
    padding: 2px 10px;
    text-decoration: none;
    font-size: 12px;
    font-family: var(--family1);
    letter-spacing: 0.3px;

    &:hover {
      border: 1px solid var(--purple1);
      background: #ecf4ff;
      color: var(--black);
    }
  }
}
</style>
