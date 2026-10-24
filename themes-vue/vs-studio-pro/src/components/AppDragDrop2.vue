<template>
  <div>
    <drag :transfer-data="{ example: 'minimal' }">drag</drag>
    <drop @drop="handleDrop">drop</drop>
  </div>
</template>

<script lang="ts">
import { defineComponent } from "vue";
// this demo was provided here - Minimal.vue
// https://github.com/cameronhimself/vue-drag-drop-demo/tree/master/src
import Drag from "./VDrag.vue";
import Drop from "./VDrop.vue";

export default defineComponent({
  name: "AppDragDrop2",
  components: { Drag, Drop },
  methods: {
    handleDrop(data: unknown) {
      alert(`You dropped with data: ${JSON.stringify(data)}`);
    },
  },
});
</script>

<style lang="scss"></style>
