<template>
  <span><slot></slot></span>
</template>

<script lang="ts">
import { defineComponent } from "vue";

export default defineComponent({
  name: "VBadgeStatus",
});
</script>

<style lang="scss" scoped>
span {
  border: 1px solid var(--color--white);
  background-color: var(--color--white);
  color: var(--color--white);
  font-size: 9px;
  font-weight: bold;
  border-radius: 8px;
  padding: 2px 10px;
  text-transform: capitalize;
  margin: 0 1px 0 1px;

  &.new {
    border: 1px solid #8a2be2;
    background-color: #8a2be2;
  }

  &.draft {
    border: 1px solid var(--color--gray3);
    background-color: var(--color--gray3);
  }

  &.emergency {
    border: 1px solid var(--color--error2);
    background-color: var(--color--error2);
  }

  &.review,
  &.pending {
    border: 1px solid var(--color--warning1);
    background-color: var(--color--warning1);
  }

  &.rejected {
    border: 1px solid var(--color--black);
    background-color: var(--color--black);
  }

  &.complete {
    border: 1px solid var(--color--success);
    background-color: var(--color--success);
  }
}
</style>
