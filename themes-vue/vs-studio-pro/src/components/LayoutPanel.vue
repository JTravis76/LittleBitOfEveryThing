<template>
  <div class="panel">
    <div class="panel-title">
      Solution Explorer
      <button type="button" @click="collpase()">
        <IconClose size="10px"></IconClose>
      </button>
    </div>
    <div class="panel-toolbar">
      <IconConnectArrowRight class="icon left"></IconConnectArrowRight>
      <IconConnectArrowRight class="icon"></IconConnectArrowRight>
      <IconHome class="icon"></IconHome>
      <div class="divider"></div>
      <IconWrench class="icon active"></IconWrench>
    </div>
    <div class="panel-body">
      <div>
        <IconCheckedIn
          size="9px"
          style="fill: #005dba; margin: 0 4px 0 0"
        ></IconCheckedIn>
        <img alt="vs-solution" src="/img/vs-solution.png" style="width: 12px" />
        <span> Solution 'Portfolio' (1 of 1 projects) </span>
      </div>
      <ul style="margin-left: 0">
        <li>
          <button
            type="button"
            class="btn-icon"
            title="expand or collpase"
            @click="expand = !expand"
          >
            <IconCaretRightOpen
              v-if="expand"
              class="icon-7"
            ></IconCaretRightOpen>
            <IconCaretRight v-else size="9px"></IconCaretRight>
          </button>
          <IconCheckedIn
            size="9px"
            style="fill: #005dba; margin: 0 4px 0 0"
          ></IconCheckedIn>
          <IconWebApplication class="icon-14"></IconWebApplication>
          <span class="text-bold"> Portfolio </span>

          <ul :style="{ display }">
            <li
              v-for="item in menuItems.filter((x) => x.parent === 1)"
              :key="item.id"
            >
              <LayoutPanelTreeItem
                :item="item"
                @action="openFileDirectory($event)"
              >
              </LayoutPanelTreeItem>
              <template v-if="item.type !== 'file'">
                <ul :style="{ display: item.expand ? 'block' : 'none' }">
                  <li
                    v-for="subitem in menuItems.filter(
                      (x) => x.parent === item.id
                    )"
                    :key="item.id"
                  >
                    <LayoutPanelTreeItem
                      :item="subitem"
                      @action="openFileDirectory($event)"
                    >
                    </LayoutPanelTreeItem>
                  </li>
                </ul>
              </template>
            </li>
          </ul>
        </li>
      </ul>
    </div>
    <div class="tabs">
      <button type="button" class="tab-item active">Solution Explorer</button>
      <button type="button" class="tab-item">Properties</button>
      <button type="button" class="tab-item">Git Changes</button>
    </div>
  </div>
</template>

<script lang="ts">
import { defineComponent } from "vue";
import { useSiteStore } from "../stores";
import IconConnectArrowRight from "./IconConnectArrowRight.vue";
import IconHome from "./IconHome.vue";
import IconWrench from "./IconWrench.vue";
import IconCheckedIn from "./IconCheckedIn.vue";
import IconWebApplication from "./IconWebApplication.vue";
import IconCaretRight from "./IconCaretRight.vue";
import IconCaretRightOpen from "./IconCaretRightOpen.vue";
import LayoutPanelTreeItem from "./LayoutPanelTreeItem.vue";
import IconClose from "./IconClose.vue";

export default defineComponent({
  name: "LayoutPanel",
  components: {
    IconConnectArrowRight,
    IconHome,
    IconWrench,
    IconCheckedIn,
    IconCaretRight,
    IconCaretRightOpen,
    IconWebApplication,
    LayoutPanelTreeItem,
    IconClose,
  },
  data() {
    return {
      expand: true,
    };
  },
  computed: {
    display() {
      return this.expand ? "block" : "none";
    },
    menuItems() {
      return useSiteStore().menuItems;
    },
  },
  methods: {
    openFileDirectory(id = 0) {
      const item = this.menuItems.find((x) => x.id === id) || {
        name: "",
        type: "",
        expand: false,
      };
      if (item.type === "file") useSiteStore().setEditorTab(item.name);
      else {
        item.expand = !item.expand;
      }
    },
    collpase() {
      useSiteStore().setSolutionExplorer();
    },
  },
});
</script>

<style lang="scss" scoped>
.panel {
  border: none;
  width: 250px;
  font-size: 12px;
  font-family: var(--family1);

  // @include xs {
  //   display: none;
  // }

  .panel-title {
    background-color: var(--panel-color1);
    padding: 4px;
    font-size: 12px;
    display: flex;
    justify-content: space-between;

    button {
      border: none;
      background: transparent;
      margin-right: 8px;
      height: 14px;
      cursor: pointer;
    }
  }

  .panel-toolbar {
    background-color: var(--purple3);
    padding: 4px;
    height: 24px;
    display: flex;

    .divider {
      border-right: 2px solid var(--purple1);
      height: 20px;
      opacity: 0.5;
    }

    .icon {
      height: 16px;
      width: 16px;
      fill: var(--purple1);
      padding: 2px;
      margin: 0 4px;
      opacity: 0.5;
    }

    .icon.left {
      transform: rotate(180deg);
    }

    .icon.active {
      fill: var(--black);
      opacity: 1;
    }
  }

  .panel-body {
    background-color: var(--white);
    height: calc(100vh - 153px);
    padding: 4px;

    .btn-icon {
      background: transparent;
      border: none;
    }

    .icon-7 {
      width: 7px;
      height: 7px;
    }

    .icon-9 {
      width: 9px;
      height: 9px;
      margin: 0 4px;
    }

    .icon-14 {
      width: 14px;
      height: 14px;
      margin: 0 2px -3px 2px;
    }
  }

  ul {
    padding-inline-start: 0px;
    list-style: none;
    margin: 4px 0 2px 20px;

    li {
      margin: 2px 0;
    }
  }

  .tabs {
    overflow: hidden;

    .tab-item {
      border: none;
      outline: none;
      cursor: pointer;
      background-color: var(--purple2);
      color: var(--purple3);
      font-size: 12px;
      padding: 5px;

      &:hover {
        background-color: var(--purple3);
        color: var(--purple2);
      }

      &.active {
        background-color: var(--white);
        color: var(--black);
      }
    }
  }
}
</style>
