<template>
  <nav class="mini-menu" role="navigation">
    <button type="button" aria-label="menu">...</button>
    <!-- <img src="./img/menu-caret.png" alt="caret" class="menu-caret" /> -->
    <ul class="drop-menu">
      <template v-for="(header, idx) in menu" :key="`item-${idx}`">
        <li class="menu-group">
          <div class="menu-group-title">
            {{ header.title }}
          </div>
          <ul class="menu-items">
            <template
              v-for="(page, idx2) in header.pages"
              :key="`link-${idx}${idx2}`"
            >
              <li
                :key="`divider-${idx}${idx2}`"
                v-if="idx2 === 0"
                class="divider"
              ></li>
              <li class="sub-item">
                {{ page }}
              </li>
              <li
                :key="`divider2-${idx}${idx2}`"
                v-if="idx2 === header.pages.length - 1"
                class="divider"
              ></li>
            </template>
          </ul>
        </li>
      </template>
    </ul>
  </nav>
</template>

<script lang="ts">
import { defineComponent, PropType } from "vue";

export interface IMiniMenu {
  title: string;
  pages: string[];
  expanded: boolean;
}

export default defineComponent({
  name: "VMiniMenu",
  props: {
    menu: {
      type: Array as PropType<IMiniMenu[]>,
      default: new Array<IMiniMenu>(),
    },
  },
});
</script>

<style lang="scss" scoped>
.mini-menu {
  position: relative;
  left: 0px;
  width: 25px;
  z-index: 100;

  &:hover {
    .drop-menu {
      display: block;
      width: 450px;
    }

    // .menu-caret {
    //   display: block;
    //   width: 17px;
    //   height: 26px;
    // }
  }

  button {
    border: none;
    background: transparent;
    font-weight: bold;
    font-size: 24px;
  }

  // .menu-caret {
  //   display: none;
  //   position: absolute;
  //   left: 27px;
  //   top: 0px;
  //   z-index: 2;
  // }

  .drop-menu {
    position: absolute;
    top: 0px;
    left: 20px;
    z-index: 1;
    display: none;
    float: left;
    min-width: 450px;
    padding: 0px 0;
    font-size: 16px;
    text-align: left;
    list-style: none;
    background-color: #fff;
    background-clip: padding-box;
    border: 1px solid #636363;
    border-radius: 4px;
    box-shadow: 0 6px 12px lightgray;
    max-height: 400px;
    overflow-y: auto;

    li {
      margin-bottom: 0px;
      cursor: pointer;

      ul {
        list-style: none;
      }
    }

    .menu-group {
      padding: 0px 0px;

      &:hover {
        .menu-items {
          display: block;
        }
      }

      .menu-group-title {
        padding: 0px 10px;
        height: 32px;
        line-height: 2;
      }

      .menu-items {
        display: none;
        margin: 0 0 0 -15px;

        li {
          padding: 5px 0px 5px 15px;
        }

        .sub-item {
          font-weight: bold;
          margin-left: 0px;
          padding-left: 24px;

          &:hover {
            background-color: lightgray;
          }
        }

        .divider {
          padding: 0px;
          height: 1px;
          margin: 0px 0;
          overflow: hidden;
          background-color: rgba(0, 0, 0, 0.2);
        }
      }
    }
  }
}
</style>
