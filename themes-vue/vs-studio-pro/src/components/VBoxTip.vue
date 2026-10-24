<template>
  <div
    ref="boxtip"
    class="box-tip"
    :class="{ open }"
    :style="`transform: translate(${rect.left}px, ${rect.top}px)`"
  >
    <div class="box-tip--actions">
      <button type="button" @click="$emit('close')" title="close">
        <Icon icon="close"></Icon>
      </button>
    </div>
    <div class="box-tip--body"><slot></slot></div>
  </div>
</template>

<script lang="ts">
import { defineComponent, PropType } from "vue";
import Icon from "./VIcon.vue";

export default defineComponent({
  name: "VBoxTip",
  components: {
    Icon,
  },
  props: {
    open: {
      type: Boolean,
      default: false,
    },
    domrect: {
      type: Object as PropType<DOMRect>,
      required: false,
      default: () => ({}),
    },
  },
  emits: ["close"],
  data() {
    return {
      rect: {
        top: 0,
        left: 0,
      },
      cnt: 0,
    };
  },
  watch: {
    domrect() {
      // this.rect.left = this.domrect.left - 420;
      // this.rect.top = this.domrect.top - 364;
    },
  },
  updated() {
    this.$nextTick(() => {
      // == Fetch bounding Rect and adjust TOP position
      if (this.cnt < 3) {
        const rect = (
          this.$refs.boxtip as HTMLDivElement
        ).getBoundingClientRect();
        // 48 equals the height of button (32px) + caret(16)
        this.rect.top = -(rect.top - rect.height - 48);
        // 53.5 equals the half of the button width
        this.rect.left = -(rect.width / 2) + 53.5;
        this.cnt += 1;
      }
    });
  },
});
</script>

<style lang="scss" scoped>
.box-tip {
  visibility: hidden;
  position: absolute;
  min-width: 120px;
  max-width: 414px;
  padding: 4px 4px 10px 8px;
  background: var(--color--white);
  border: 1px solid #95989a;
  box-shadow: 0 0 10px 0 rgba(#000000, 0.2);
  color: var(--app-color--primary);
  font-size: 14px;
  z-index: 2;

  &::after {
    content: "";
    background-color: var(--color--white);
    border-left: 1px solid var(--color--white);
    border-right: 1px solid #95989a;
    border-top: 1px solid var(--color--white);
    border-bottom: 1px solid #95989a;
    position: absolute;
    left: 40%;
    bottom: -11px;
    width: 20px;
    height: 20px;
    transform: rotate(45deg);
  }

  &.open {
    visibility: visible;
  }

  .box-tip--actions {
    height: 14px;

    button {
      float: right;
      line-height: 0;
      padding: 0.1rem;
      background: transparent;
      border: none;
      border-radius: 50%;
      cursor: pointer;

      &:hover {
        background-color: var(--color--gray2);
      }
    }
  }

  .box-tip--body {
    font-size: 10px;
  }
}
</style>
