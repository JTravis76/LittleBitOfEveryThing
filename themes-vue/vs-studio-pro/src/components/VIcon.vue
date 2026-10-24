<template>
  <Icon :icon="i" :size="size"></Icon>
</template>

<script lang="ts">
import { defineComponent } from "vue";
import { Icon } from "@iconify/vue";
import icons from "../data/icons";

export default defineComponent({
  name: "VIcon",
  components: {
    Icon,
  },
  props: {
    icon: {
      type: String,
      required: true,
      default: "",
    },
    size: {
      type: Number,
      default: 24,
    },
  },
  computed: {
    i() {
      const obj = icons as { [key: string]: string };
      return obj[this.icon];
    },
  },
});
</script>

<style lang="scss"></style>
