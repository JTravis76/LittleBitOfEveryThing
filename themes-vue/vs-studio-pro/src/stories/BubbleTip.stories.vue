<template>
  <div class="row">
    <div class="eight columns">
      <p></p>
      <div class="overlay">
        <BubbleTip :bubble="bubble"></BubbleTip>
      </div>
    </div>
    <div class="four columns">
      <fieldset>
        <legend>Info</legend>
        <p>
          This story demostrates the bubble tip component. Bubble tip are meant
          to float over a section of the background. When clicked, a popup tip
          appears with information related to the topic.
        </p>
      </fieldset>
    </div>
  </div>
</template>

<script lang="ts">
import { defineComponent } from "vue";
import BubbleTip, { Bubble } from "../components/VBubbleTip.vue";

export default defineComponent({
  name: "BubbleTipStory",
  components: {
    BubbleTip,
  },
  data() {
    return {
      bubble: new Bubble({
        tips: [
          {
            text: "Tip #1",
            base64: null,
            image: null,
            top: -20,
            left: 55,
            width: 0,
          },
          {
            text: "Tip #2",
            base64: null,
            image: null,
            top: 60,
            left: 10,
            width: 0,
          },
        ],
      }),
    };
  },
});
</script>

<style lang="scss" scoped>
.overlay {
  position: absolute;
}
</style>
