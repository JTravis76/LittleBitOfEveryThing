<template>
  <img
    :title="name"
    :src="src"
    :style="{ 'border-radius': borderRadius }"
    :alt="name"
  />
</template>

<script lang="ts">
import { defineComponent, PropType } from "vue";
import { letterAvatar } from "../lib/letter-avatar.js";

export default defineComponent({
  name: "VAvatar",
  props: {
    name: {
      type: String,
      default: "?",
    },
    size: {
      type: Number,
      default: 100,
    },
    round: {
      type: Boolean,
      default: false,
    },
    url: {
      type: String as PropType<string | undefined>,
      default: undefined,
    },
  },
  data() {
    return {
      borderRadius: "0%",
      src: "",
    };
  },
  watch: {
    name(n) {
      this.borderRadius = this.round ? "50%" : "0%";
      this.src = this.url ?? letterAvatar(n, this.size);
    },
  },
  created() {
    this.borderRadius = this.round ? "50%" : "0%";
    this.src = this.url ?? letterAvatar(this.name, this.size);
  },
});
</script>

<style lang="scss"></style>
