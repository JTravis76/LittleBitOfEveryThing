<template>
  <div v-show="loading" class="loading-background">
    <button type="button" @click="close">&times;</button>
    <div class="loading-wrapper">
      <div class="loader spin"></div>
    </div>
  </div>
</template>

<script lang="ts">
import { defineComponent } from "vue";
import { useSiteStore } from "../stores/siteStore";

export default defineComponent({
  name: "LayoutPageLoader",
  computed: {
    loading(): boolean {
      return useSiteStore().pageLoading;
    },
  },
  methods: {
    close() {
      useSiteStore().pageLoader(false);
    },
  },
});
</script>

<style lang="scss" scoped>
.loading-background {
  background: none repeat scroll 0 0 rgb(40 30 0 / 30%);
  height: 100%;
  width: 100%;
  position: fixed;
  top: 0;
  left: 0;
  margin: 0;
  padding: 0;
  z-index: 10;

  button {
    border: none;
    background: transparent;
    color: rgba(0, 0, 25, 0.2);
    width: 1rem;
    cursor: pointer;
  }

  div.loading-wrapper {
    background: none repeat scroll 0 0 rgba(0, 0, 0, 0);
    border: none;
    height: 100%;
    width: 100%;
    display: flex;
    align-items: center;
    justify-content: center;

    .loader {
      border: 10px solid var(--color--gray1);
      border-top: 10px solid var(--color--primary);
      border-radius: 50%;
      width: 100px;
      height: 100px;
    }
  }
}
</style>
