<template>
  <div style="width: 50%; margin: 25px">
    <div class="gameboard">
      <template v-for="cell in totalCells">
        <div class="cell" :data-cell="cell"></div>
      </template>
    </div>
  </div>
</template>

<script lang="ts">
import { defineComponent } from "vue";

export default defineComponent({
  name: "AppGameboard",
  data() {
    return {
      totalCells: 144,
    };
  },
});
</script>

<style lang="scss" scoped>
.gameboard {
  display: grid;
  grid-template-columns: auto auto auto auto auto auto auto auto auto auto auto auto;

  .cell {
    border: 1px solid var(--black);
    width: 100%;
    /* padding-top: 100%; 1:1 ratio */
    padding-top: 75%; /* 4:3 ratio */
  }
}
</style>
