<template>
  <div>
    <button
      :key="`bubble-${id}`"
      type="button"
      class="bubble"
      title="toggle tip"
      :style="{
        top: `${bubble.top}px`,
        left: `${bubble.left}px`,
        'z-index': 0,
      }"
      @click="toggle()"
      zzmouseup="fetchPosition($event, 1, bubble)"
    >
      <span>{{ bubble.label }}</span>
    </button>
    <div v-show="bubble.showTip" :key="`tips-${id}`">
      <template v-for="(tip, idx2) in bubble.tips" :key="`tip-${idx2}`">
        <div
          class="bubble-tip"
          :style="{
            top: `${tip.top}px`,
            left: `${tip.left}px`,
            width: `${tip.width}px`,
            'z-index': idx2 + 1,
          }"
          zzmouseup="fetchPosition($event, 2, undefined, idx2)"
        >
          <div v-html="tip.text"></div>
          <img v-if="tip.base64 !== null" alt="image" :src="tip.base64" />
        </div>
      </template>
    </div>
  </div>
</template>

<script lang="ts">
import { defineComponent, PropType } from "vue";

export interface IBubbleTip {
  text: string;
  base64: string | null;
  image: string | null;
  top: number;
  left: number;
  width: number;
}

export interface IBubble {
  id: number;
  label: string;
  top: number;
  left: number;
  showTip: boolean;
  tips: IBubbleTip[];
}

export class Bubble implements IBubble {
  constructor(init?: Partial<Bubble>) {
    this.id = -1;
    this.label = "1";
    this.top = 0;
    this.left = 0;
    this.showTip = false;
    this.tips = new Array<IBubbleTip>();

    if (init) Object.assign(this, init);
  }
  id: number;
  label: string;
  top: number;
  left: number;
  showTip: boolean;
  tips: IBubbleTip[];
}

export default defineComponent({
  name: "VBubbleTip",
  props: {
    id: {
      type: Number,
      default: 0,
    },
    bubble: {
      type: Object as PropType<IBubble>,
      default: new Bubble({
        tips: [
          {
            text: "",
            base64: null,
            image: null,
            top: 0,
            left: 0,
            width: 0,
          },
        ],
      }),
    },
  },
  methods: {
    toggle() {
      this.bubble.showTip = !this.bubble.showTip;
    },
  },
});
</script>

<style lang="scss" scoped>
$green: #29a429;
$white: #ffffff;

.bubble {
  position: absolute;
  background-color: $white;
  border: 3px solid $green;
  border-radius: 50%;
  width: 48px;
  height: 48px;
  cursor: pointer;
  box-shadow: 2px 15px 15px 0px rgba(0, 0, 0, 0.2);

  span {
    border-radius: 50%;
    background-color: $green;
    color: $white;
    height: 35px;
    width: 35px;
    display: flex;
    align-items: center;
    justify-content: center;
    margin-left: -2px;
    font-size: 20px;
    font-weight: bold;
  }
}

.bubble-tip {
  position: absolute;
  border: 1px solid #6e6e6e;
  border-radius: 0%;
  min-width: 250px;
  color: var(--color--black);
  background-color: white;
  box-shadow: 10px 15px 35px 0px rgba(0, 0, 0, 0.2);
  font-size: 14px;
  padding: 10px;
}
</style>
