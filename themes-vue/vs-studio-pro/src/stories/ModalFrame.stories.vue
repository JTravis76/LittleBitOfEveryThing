<template>
  <div class="row">
    <div class="eight columns">
      <div>
        <label>Open Modal</label>
        <button type="button" class="button" @click="openModal">Open</button>
      </div>
      <ModalFrame ref="modal" :title="title" :size="size">
        <div class="row">
          <div class="twelve columns">
            <div>Some content goes here</div>
          </div>
        </div>
      </ModalFrame>
    </div>
    <div class="four columns">
      <fieldset>
        <legend>Info</legend>
        <p>This story demostrates how to use the modal frame component.</p>

        <div>
          <label>Title</label>
          <input type="text" v-model="title" />
        </div>
        <div>
          Modal Size:
          <label>
            <input type="radio" name="size" value="md" v-model="size" />
            <span>Medium</span>
          </label>
          <label>
            <input type="radio" name="size" value="lg" v-model="size" />
            <span>Large</span>
          </label>
        </div>
      </fieldset>
    </div>
  </div>
</template>

<script lang="ts">
import { defineComponent } from "vue";
import ModalFrame from "../components/ModalFrame.vue";

export default defineComponent({
  name: "ModalFrameStory",
  components: {
    ModalFrame,
  },
  data() {
    return {
      title: "My First Modal",
      size: "md",
    };
  },
  methods: {
    openModal() {
      const modal = this.$refs.modal as typeof ModalFrame;
      modal.open();

      // To close modal programmatically
      //modal.close();
    },
  },
});
</script>

<style lang="scss"></style>
