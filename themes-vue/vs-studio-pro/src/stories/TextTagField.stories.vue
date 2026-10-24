<template>
  <div class="row">
    <div class="eight columns">
      <TextTagField
        placeholder="Tag Ids"
        :tags="tags"
        @tag="tags.push($event)"
        @remove="removeTag($event)"
      ></TextTagField>
    </div>
    <div class="four columns">
      <fieldset>
        <legend>Info</legend>
        <p>
          This story displays how to use the text-tag field component. Tags are
          created by typing a word into the input field and either pressing the
          'Enter' key or clicking the add icon button.
        </p>
        <p>
          Enter text in the field below to simulate data provided from a server.
          <input
            v-model="serverData"
            type="text"
            placeholder=""
            style="margin: 0"
          />
          <small>This is a comma-separated string.</small>
        </p>
      </fieldset>
    </div>
  </div>
</template>

<script lang="ts">
import { defineComponent } from "vue";
import TextTagField from "../components/VTextTagField.vue";

export default defineComponent({
  name: "TextTagFieldStory",
  components: {
    TextTagField,
  },
  data() {
    return {
      tags: new Array<string>(),
      serverData: "item 1,Item 2,item3,item 04",
    };
  },
  created() {
    this.tags = this.serverData.split(",");
  },
  methods: {
    removeTag(tag: string) {
      for (let i = this.tags.length; i >= 0; i -= 1) {
        if (this.tags[i] == tag) {
          this.tags.splice(i, 1);
          break;
        }
      }
    },
  },
});
</script>

<style lang="scss"></style>
