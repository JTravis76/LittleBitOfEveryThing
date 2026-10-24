<template>
  <div class="row">
    <div class="eight columns">
      <div class="row" style="margin-top: 150px">
        <div class="four columns">
          <BoxTip :open="open" @close="open = false">
            <div>Name: ACME I</div>
            <div>Type: Air Bushing</div>
            <div>Group: Group 1</div>
          </BoxTip>
          <button type="button" class="button" @click="open = !open">
            123456
          </button>
          <p>Standard open/close and slot usage</p>
        </div>
        <div class="four columns">
          <button
            type="button"
            class="button button-primary"
            @click="toggle($event)"
          >
            789012
          </button>
          <p>
            Adjusting box tip via DOMRect. The Box-Tip component is located
            elsewhere within the DOM.
          </p>
        </div>
        <div class="four columns">
          <BoxTipButton>
            <template #tip>
              <div>Name: ACME II</div>
              <div>Type: Air Bushing</div>
              <div>Group: Group 3</div>
            </template>
            987654</BoxTipButton
          >
          <p>Box-Tip-Button component for cleaner use.</p>
        </div>
      </div>
    </div>
    <div class="four columns">
      <fieldset>
        <legend>Info</legend>
        This story displays how to use the box tip.
      </fieldset>

      <fieldset>
        <legend>Interactive States</legend>
        <label>
          <input v-model="open" type="checkbox" />
          <span> Opened</span>
        </label>
      </fieldset>
    </div>
  </div>
  <BoxTip :open="open1" :domrect="clientRect" @close="open1 = !open1">
    <div>Name: ACME III</div>
    <div>Type: Air Bushing</div>
    <div>Group: Group 2</div>
  </BoxTip>
</template>

<script lang="ts">
import { defineComponent } from "vue";
import BoxTip from "../components/VBoxTip.vue";
import BoxTipButton from "../components/VBoxTopButton.vue";

export default defineComponent({
  name: "BoxTipStory",
  components: {
    BoxTip,
    BoxTipButton,
  },
  data() {
    return {
      open: false,
      open1: false,
      clientRect: {
        bottom: 0,
        height: 0,
        left: 0,
        right: 0,
        top: 0,
        width: 0,
        x: 0,
        y: 0,
      } as DOMRect,
    };
  },
  methods: {
    toggle(e: MouseEvent) {
      const el = e.target as HTMLButtonElement;
      //console.log(e.clientX, e.clientY);
      this.clientRect = el.getBoundingClientRect();
      this.open1 = !this.open1;
    },
  },
});
</script>

<style lang="scss" scoped></style>
