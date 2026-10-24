<template>
  <div class="pane-wrapper">
    <div :class="{ 'pane-top': showCompare }">
      <div>Some other content goes here</div>
    </div>
    <div class="pane-bottom">
      <ComparisonPane v-if="showCompare"></ComparisonPane>
    </div>
  </div>
</template>

<script lang="ts">
import { defineComponent } from "vue";
import ComparisonPane from "./AppComparisonPane.vue";

export default defineComponent({
  name: "AppSplitPane",
  components: {
    ComparisonPane,
  },
  data() {
    return {
      showCompare: true,
    };
  },
});
</script>

<style lang="scss" scoped>
.pane-wrapper {
  position: relative;
  left: 0;
  top: 0;
  width: 100%;
  height: calc(100vh - 118px);
  background-color: transparent;
  overflow: auto;
  display: flex;
  flex-direction: column;
}

.pane-top {
  overflow-y: auto;
  height: calc(100vh - 45%);
}

.pane-bottom {
  background-color: var(--color--white);
  position: relative;
}
</style>
