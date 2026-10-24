<template>
  <div class="editor-tabs">
    <template v-for="(tab, idx) in tabs" :key="idx">
      <span class="editor-tab--item" :class="{ active: tab === activeTab }">
        <button type="button" @click="selectTab(tab)">
          {{ tab }}
        </button>
        <a href="#" @click.stop="closeTab(idx)">
          <IconClose class="icon"></IconClose>
        </a>
      </span>
    </template>
  </div>
</template>

<script lang="ts">
import { defineComponent } from "vue";
import IconClose from "./IconClose.vue";
import { useSiteStore } from "../stores";

export default defineComponent({
  name: "LayoutEditorTab",
  components: { IconClose },
  computed: {
    tabs() {
      return useSiteStore().editorTabs;
    },
    activeTab() {
      return useSiteStore().activeTab;
    },
  },
  methods: {
    selectTab(tab = "") {
      useSiteStore().setActiveTab(tab);
    },
    closeTab(idx: number) {
      useSiteStore().removeTab(idx);
    },
  },
});
</script>

<style lang="scss" scoped>
.editor-tabs {
  overflow: hidden;

  .editor-tab--item {
    &:hover {
      button,
      a {
        background-color: var(--purple3);
        color: var(--purple2);
      }
    }

    button,
    a {
      border: none;
      outline: none;
      cursor: pointer;
      font-size: 12px;
      background-color: var(--purple2);
      color: var(--purple3);
    }

    button {
      padding: 5px;
    }

    a {
      padding: 5px 5px 6px 0;
    }

    .icon {
      font-weight: initial;
      margin-left: 8px;
      padding-bottom: 2px;
      fill: var(--purple2);
      width: 6px;
      height: 6px;
    }

    &.active {
      button,
      a {
        background-color: var(--panel-color1);
        color: var(--black);
        font-weight: bold;
      }
    }
  }
}
</style>
