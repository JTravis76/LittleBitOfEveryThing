<template>
  <div class="dial-wheel--wrap">
    <ul>
      <li v-for="(item, idx) in list" :key="item">
        <label @click="$emit('selection', idx)">
          {{ item }}
        </label>
      </li>
    </ul>
  </div>
</template>

<script lang="ts">
import { defineComponent, PropType } from "vue";

export default defineComponent({
  name: "VDialWheel",
  props: {
    list: {
      type: Array as PropType<string[]>,
      default: new Array<string>(),
    },
  },
});
</script>

<style lang="scss" scoped>
.dial-wheel--wrap {
  border: 1px solid;
  border-top: 4px solid;
  border-bottom: 4px solid;
  overflow-y: scroll;
  height: 90px;
  width: 200px;

  &::-webkit-scrollbar {
    width: 0px;
  }

  ul {
    margin: 0;
    padding: 0;

    li {
      margin: 0;
      list-style: none;
      border: 0.5px solid;
      height: 25px;
      padding: 0px 4px;
      cursor: pointer;

      label {
        cursor: pointer;
      }
    }
  }
}
</style>
