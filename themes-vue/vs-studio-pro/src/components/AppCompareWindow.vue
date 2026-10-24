<template>
  <div class="compare-window">
    <template v-for="result in results" :key="result.lineNumber">
      <div
        :class="{
          modify: result.type === 'modify',
          add: result.type === 'add',
        }"
      >
        <span class="linenumber">
          {{ result.lineNumber }}
        </span>
        <template v-for="space in result.level" :key="space">
          <span :style="`margin-left:${space * 10}px`"></span>
        </template>
        <span>{{ result.text }}</span>
        <span v-if="result.comma">,</span>
      </div>
    </template>
  </div>
</template>

<script lang="ts">
import { defineComponent, PropType } from "vue";
import { DiffResult } from "../lib/json-diff-kit@1.0.19/typings";

export default defineComponent({
  name: "AppCompareWindow",
  props: {
    results: {
      type: Array as PropType<DiffResult[]>,
      default: new Array<DiffResult>(),
    },
  },
});
</script>

<style lang="scss" scoped>
.compare-window {
  font-size: 12px;
}
.linenumber {
  text-align: right;
  width: 20px;
  display: inline-block;
}

.modify {
  background-color: #ffe082;
}

.add {
  background-color: #a5d6a7;
}

.remove {
  background-color: #ef9a9a;
}
</style>
