<template>
  <div style="display: flex">
    <div style="width: 6%">
      <a
        v-if="item.type !== 'file'"
        href="#"
        class="menu-item"
        title="expand or collpase"
        @click.prevent="openFileDirectory(item.id)"
      >
        <IconCaretRight v-if="!item.expand" size="9px"> </IconCaretRight>
        <IconCaretRightOpen v-else class="icon-7"></IconCaretRightOpen>
      </a>
    </div>
    <div style="width: 6%">
      <IconCheckedIn size="9px" style="fill: #005dba"></IconCheckedIn>
    </div>
    <div style="width: 10%">
      <template v-if="item.type === 'directory'">
        <IconFolderClosed size="14px" style="fill: #996f00"> </IconFolderClosed>
      </template>
      <IconHtmlFile v-else size="14px"></IconHtmlFile>
    </div>
    <div style="width: 78%">
      <a href="#" class="menu-item" @click.prevent="openFileDirectory(item.id)">
        {{ item.name }}
      </a>
    </div>
  </div>
</template>

<script lang="ts">
import { defineComponent } from "vue";
import IconCaretRight from "./IconCaretRight.vue";
import IconCheckedIn from "./IconCheckedIn.vue";
import IconFolderClosed from "./IconFolderClosed.vue";
import IconHtmlFile from "./IconHtmlFile.vue";
import IconCaretRightOpen from "./IconCaretRightOpen.vue";

export default defineComponent({
  name: "LayoutPanelTreeItem",
  components: {
    IconCaretRight,
    IconCheckedIn,
    IconFolderClosed,
    IconHtmlFile,
    IconCaretRightOpen,
  },
  props: {
    item: {
      type: Object,
      default: () => ({
        id: 0,
        name: "",
        type: "",
      }),
    },
  },
  emits: ["action"],
  methods: {
    openFileDirectory(id = 0) {
      this.$emit("action", id);
    },
  },
});
</script>

<style lang="scss" scoped>
.menu-item {
  color: var(--black);
  text-decoration: none;

  .icon-7 {
    width: 7px;
    height: 7px;
  }
}
</style>
