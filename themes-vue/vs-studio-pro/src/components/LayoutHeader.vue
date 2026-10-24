<template>
  <header>
    <IconVisualStudio class="icon"></IconVisualStudio>
    <LayoutMainMenu></LayoutMainMenu>
    <input text="text" class="search-box" placeholder="Search (Ctrl+Q)" />
    <div class="project-box">Portfolio</div>
    <div class="application-bar">
      <div class="avatar">JT</div>
      <div class="application-controls">
        <a href="#" title="minimize">
          <IconMinimize size="12" style="margin: 2px 8px"></IconMinimize>
        </a>
        <a href="#" title="maximize">
          <IconMaximize size="12" style="margin: 2px 8px"></IconMaximize>
        </a>
        <a href="#" title="close">
          <IconClose
            size="12"
            style="fill: #4c5671; margin: 2px 8px"
          ></IconClose>
        </a>
      </div>
    </div>
  </header>
</template>

<script lang="ts">
import { defineComponent } from "vue";
import LayoutMainMenu from "./LayoutMainMenu.vue";
import IconVisualStudio from "./IconVisualStudio.vue";
import IconMinimize from "./IconMinimize.vue";
import IconMaximize from "./IconMaximize.vue";
import IconClose from "./IconClose.vue";

export default defineComponent({
  name: "Layoutheader",
  components: {
    LayoutMainMenu,
    IconVisualStudio,
    IconMinimize,
    IconMaximize,
    IconClose,
  },
});
</script>

<style lang="scss" scoped>
header {
  overflow: hidden;
  background-color: var(--purple3);
  position: fixed;
  top: 0;
  width: 100%;
  padding: 2px 4px;
  z-index: 1;
  display: flex;
  max-height: 22px;

  & .avatar {
    font-size: 9px;
    letter-spacing: 0.5px;
    border: 1px solid var(--purple1);
    border-radius: 50%;
    background-color: var(--purple1);
    color: var(--purple3);
    padding: 4px;
    font-weight: bold;
  }

  .icon {
    width: 20px;
    height: 20px;
    margin: 0 4px 0 0;
    fill: var(--purple2);
  }

  .search-box {
    border: 1px solid #8591a2;
    padding: 2px;
    font-size: 12px;
  }

  .project-box {
    background-color: var(--white1);
    color: var(--purple2);
    padding: 0 6px;
    font-size: 12px;
    margin-left: 16px;
    line-height: 1.8;
  }

  .application-bar {
    display: flex;
    width: 40%;
    justify-content: flex-end;
    align-items: center;
  }

  .application-controls {
    width: 150px;
    display: flex;
    justify-content: flex-end;

    a {
      border: 1px solid var(--purple3);
      text-decoration: none;
      padding-top: 2px;

      &:hover {
        border: 1px solid var(--purple3);
        background: #ecf4ff;
        color: var(--black);
      }
    }
  }
}
</style>
