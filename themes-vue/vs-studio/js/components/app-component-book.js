import BlankStory from "../stories/blank-story.stories.js";
import TextFieldStory from "../stories/text-field.stories.js";
import AccordionStory from "../stories/accordion.stories.js";
import AccordionGroupStory from "../stories/accordion-group.stories.js";
import DialogStory from "../stories/dialog.stories.js";
import IconStory from "../stories/icon.stories.js";
import BrowserNotificationStory from "../stories/browser-notification.stories.js";
import TopBannerStory from "../stories/top-banner.stories.js";
import AvatarStory from "../stories/avatar.stories.js";
import SearchableSelectStory from "../stories/searchable-select.stories.js";
import ToggleSwitchStory from "../stories/toggle-switch.stories.js";
import SortableTableStory from "../stories/sortable-table.stories.js";
import FileUploaderStory from "../stories/file-uploader.stories.js";

export default {
  template: `
<div class="app--component-book">
  <div class="component-book--menu">
    <nav>
      <ul>
        <template v-for="item in menu" :key="item">
          <li>
            <a
              href="#"
              :class="{
                'active': active(item),
              }"
              @click.prevent="load(item)"
              >{{ item }}</a
            >
          </li>
        </template>
      </ul>
    </nav>
  </div>
  <div class="component-book--playground">
    <component :is="template"></component>
  </div>
</div>`,
  name: "app-component-book",
  components: {
    BlankStory,
    TextFieldStory,
    AccordionStory,
    AccordionGroupStory,
    DialogStory,
    IconStory,
    BrowserNotificationStory,
    TopBannerStory,
    AvatarStory,
    SearchableSelectStory,
    ToggleSwitchStory,
    SortableTableStory,
    FileUploaderStory,
  },
  data() {
    return {
      template: "BlankStory",
      menu: [],
    };
  },
  created() {
    this.menu = [
      "TextField",
      "Accordion",
      "AccordionGroup",
      "Dialog",
      "Icon",
      "BrowserNotification",
      "TopBanner",
      "Avatar",
      "SearchableSelect",
      "ToggleSwitch",
      "SortableTable",
      "FileUploader",
    ].sort();
  },
  methods: {
    load(name) {
      this.template = `${name}Story`;
    },
    active(name) {
      return `${name}Story` === this.template;
    },
  },
};
