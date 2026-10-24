import IconCaretRight from "./icon-caret-right.js";
import IconCheckedIn from "./icon-checked-in.js";
import IconFolderClosed from "./icon-folder-closed.js";
import IconHtmlFile from "./icon-html-file.js";
import IconCaretRightOpen from "./icon-caret-right-open.js";

export default {
  name: "layout-panel-tree-item",
  template: `
<div style="display: flex">
  <div style="width: 8%;">
    <a
      v-if="item.type !== 'file'"
      href="#"
      class="menu-item"
      title="expand or collpase"
      @click.prevent="openFileDirectory(item.id)">
        <IconCaretRight
          v-if="!item.expand"  
          size="9px"
        >
        </IconCaretRight>
        <IconCaretRightOpen v-else class="icon-7"></IconCaretRightOpen>
    </a>
  </div>
  <div style="width: 10%">
    <IconCheckedIn
      size="9px"
      style="fill: #005dba;"
    ></IconCheckedIn>
  </div>
  <div style="width: 10%">
    <template v-if="item.type === 'directory'">
      <IconFolderClosed
        size="14px"
        style="fill: #996f00;"
      >
      </IconFolderClosed>
    </template>
    <IconHtmlFile v-else size="14px"></IconHtmlFile>
  </div>
  <div style="width: 72%">
    <a href="#" class="menu-item" @click.prevent="openFileDirectory(item.id)">
      {{ item.name }}
    </a>
  </div>
</div>
  `,
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
};
