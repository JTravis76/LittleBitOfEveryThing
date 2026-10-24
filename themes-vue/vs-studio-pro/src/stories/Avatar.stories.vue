<template>
  <div class="row">
    <div class="eight columns">
      <Avatar :name="name" :size="size" :round="round"></Avatar>
    </div>
    <div class="four columns">
      <fieldset>
        <legend>Info</legend>
        <p>This story shows the use of the Avatar component.</p>
        <p>Name prop can be formatted either 'First Last' or 'Last, First'</p>
        <p>
          <b>Note: </b>Only the name is reactive. To change size, adjust the
          field and then edit the name field.
        </p>
        <div>
          <label>Name</label>
          <input type="text" v-model="name" />
        </div>
        <div>
          <label>Size</label>
          <input type="number" v-model.number="size" />
        </div>
        <label>
          <input type="checkbox" v-model="round" />
          <span class="label-body">Round</span>
        </label>
      </fieldset>
    </div>
  </div>
</template>

<script lang="ts">
import { defineComponent } from "vue";
import Avatar from "../components/VAvatar.vue";

export default defineComponent({
  name: "AvatarStory",
  components: {
    Avatar,
  },
  data() {
    return {
      name: "Johnny Blaze",
      size: 92,
      round: true,
    };
  },
});
</script>

<style lang="scss"></style>
