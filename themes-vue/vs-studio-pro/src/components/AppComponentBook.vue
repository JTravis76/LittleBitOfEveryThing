<template>
  <div class="skeleton-ui app--component-book">
    <div class="row">
      <div class="two columns">
        <nav>
          <ul class="u-full-width">
            <template v-for="item in menu" :key="item">
              <li :class="{ active: active(item) }">
                <a href="#" @click.prevent="load(item)">{{ item }} </a>
              </li>
            </template>
          </ul>
        </nav>
      </div>
      <div class="ten columns">
        <div class="component-book--playground">
          <component :is="template"></component>
        </div>
      </div>
    </div>
  </div>
</template>

<script lang="ts">
import { defineComponent } from "vue";
import BlankStory from "../stories/Blank.stories.vue";
import AvatarStory from "../stories/Avatar.stories.vue";
import AccordionStory from "../stories/Accordion.stories.vue";
import AccordionGroupStory from "../stories/AccordionGroup.stories.vue";
import BoxTipStory from "../stories/BoxTip.stories.vue";
import ModalFrameStory from "../stories/ModalFrame.stories.vue";
import PageLoaderStory from "../stories/PageLoader.stories.vue";
import PaginatorStory from "../stories/Paginator.stories.vue";
import SearchableSelectStory from "../stories/SearchableSelect.stories.vue";
import SortableTableStory from "../stories/SortableTable.stories.vue";
import ToggleSwitchStory from "../stories/ToggleSwitch.stories.vue";
import ToolbarMenuStory from "../stories/ToolbarMenu.stories.vue";
import TextFieldStory from "../stories/TextField.stories.vue";
import TextTagFieldStory from "../stories/TextTagField.stories.vue";
import IconStory from "../stories/Icon.stories.vue";
import BadgeStatusStory from "../stories/BadgeStatus.stories.vue";
import SelectFieldStory from "../stories/SelectField.stories.vue";
import DateFieldStory from "../stories/DateField.stories.vue";
import DialogStory from "../stories/Dialog.stories.vue";
import BrowserNotificationStory from "../stories/BrowserNotification.stories.vue";
import FileUploaderStory from "../stories/FileUploader.stories.vue";
import MiniMenuStory from "../stories/MiniMenu.stories.vue";
import DataSlicerStory from "../stories/DataSlicer.stories.vue";
import RoadSignStory from "../stories/RoadSign.stories.vue";
import BubbleTipStory from "../stories/BubbleTip.stories.vue";
import DialWheelStory from "../stories/DialWheel.stories.vue";

export default defineComponent({
  name: "AppComponentBook",
  components: {
    BlankStory,
    AvatarStory,
    AccordionStory,
    AccordionGroupStory,
    BoxTipStory,
    ModalFrameStory,
    PageLoaderStory,
    PaginatorStory,
    SearchableSelectStory,
    SortableTableStory,
    ToggleSwitchStory,
    ToolbarMenuStory,
    TextFieldStory,
    TextTagFieldStory,
    IconStory,
    BadgeStatusStory,
    SelectFieldStory,
    DateFieldStory,
    DialogStory,
    BrowserNotificationStory,
    FileUploaderStory,
    MiniMenuStory,
    DataSlicerStory,
    RoadSignStory,
    BubbleTipStory,
    DialWheelStory,
  },
  data() {
    return {
      template: "BlankStory",
      menu: new Array<string>(),
    };
  },
  created() {
    this.menu = [
      "Avatar",
      "Accordion",
      "AccordionGroup",
      "BoxTip",
      "ModalFrame",
      "PageLoader",
      "Paginator",
      "SearchableSelect",
      "SortableTable",
      "ToggleSwitch",
      "ToolbarMenu",
      "TextField",
      "TextTagField",
      "BadgeStatus",
      "SelectField",
      "DateField",
      "Dialog",
      "Icon",
      "BrowserNotification",
      "FileUploader",
      "MiniMenu",
      "DataSlicer",
      "RoadSign",
      "BubbleTip",
      "DialWheel",
    ].sort();
  },
  methods: {
    load(name: string) {
      this.template = `${name}Story`;
    },
    active(name: string) {
      return `${name}Story` === this.template;
    },
  },
});
</script>

<style lang="scss" scoped>
.app--component-book {
  nav ul {
    margin-left: 1rem;
    font-size: 0.8rem;

    li {
      border-left: 3px solid transparent;
      list-style: none;
      margin-bottom: 0.3rem;
      padding-left: 0.3rem;

      &:hover {
        border-left: 3px solid var(--color--black);
        background-color: var(--color--gray1);
      }

      a {
        color: var(--color--black);
        text-decoration: none;
      }

      &.active {
        border-left: 3px solid var(--color--primary);
        background-color: var(--color--gray1);
        font-weight: bold;

        a {
          color: var(--color--primary);
        }
      }
    }
  }

  .component-book--playground {
    font-size: 1rem;
    height: calc(100vh - 130px);
    background-color: #f1f1f1;
    padding: 15px;
  }
}
</style>
