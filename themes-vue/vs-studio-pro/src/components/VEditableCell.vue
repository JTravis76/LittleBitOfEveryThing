<template>
  <textarea
    v-model="content"
    type="text"
    rows="1"
    autocomplete="off"
    :title="content"
    @blur="$emit('update', content)"
  ></textarea>
</template>

<script lang="ts">
import { defineComponent } from "vue";

export default defineComponent({
  name: "VEditableCell",
  props: {
    text: {
      type: String,
      default: "",
    },
  },
  emits: ["update"],
  data() {
    return {
      content: this.text,
    };
  },
});
</script>

<style lang="scss" scoped>
[type="text"] {
  border: 1px solid transparent;
  border-radius: 4px;
  background-color: inherit;
  margin: 0;
  width: 100%;

  &:focus,
  &:hover {
    background-color: var(--color--white);
    border: 1px solid var(--color--gray2);
  }
}
</style>
