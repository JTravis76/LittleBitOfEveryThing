<template>
  <div>
    <div class="app-container">
      <div class="panel">
        <button type="button" title="previous page">&lt;</button>
      </div>
      <div class="main-content">
        <div class="overlay">
          <template
            v-for="(bubble, idx) in product.bubbles"
            :key="`bubble-${idx}`"
          >
            <BubbleTip :id="idx" :bubble="bubble"></BubbleTip>
          </template>
        </div>
        <img class="main-content__img" alt="image" :src="product.img" />
      </div>
      <div class="panel">
        <button type="button" title="next page">&gt;</button>
      </div>
    </div>
    <BubbleTipDesigner v-show="false"></BubbleTipDesigner>
  </div>
</template>

<script lang="ts">
import { defineComponent } from "vue";
import BubbleTip, { IBubble, Bubble } from "./VBubbleTip.vue";
import BubbleTipDesigner from "./VBubbleTipDesigner.vue";

export default defineComponent({
  name: "AppBubbleTip",
  components: {
    BubbleTip,
    BubbleTipDesigner,
  },
  data() {
    return {
      product: {
        img: "/img/stockphoto1.jpg",
        bubbles: new Array<IBubble>(),
      },
    };
  },
  created() {
    this.product.bubbles.push(
      new Bubble({
        id: 1,
        label: "1",
        top: 224,
        left: 105,
        tips: [
          {
            text: "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book.",
            base64: null,
            image: null,
            top: 96,
            left: 154,
            width: 0,
          },
        ],
      })
    );

    this.product.bubbles.push(
      new Bubble({
        id: 2,
        label: "2",
        top: 294,
        left: 495,
        tips: [
          {
            text: "Tip #1",
            base64: null,
            image: null,
            top: 252,
            left: 515,
            width: 0,
          },
          {
            text: "Tip #2",
            base64: null,
            image: null,
            top: 352,
            left: 415,
            width: 0,
          },
        ],
      })
    );
  },
});
</script>

<style lang="scss" scoped>
.app-container {
  display: flex;
  justify-content: space-around;
  margin-top: 20px;

  .panel {
    display: flex;
    align-items: center;
    justify-content: center;
    width: 100px;
  }

  .main-content {
    width: 842px; //add 8px to allow for border;

    .overlay {
      position: absolute;
    }

    .main-content__img {
      border: 4px solid gray;
      min-width: 830px;
    }
  }
}
</style>
