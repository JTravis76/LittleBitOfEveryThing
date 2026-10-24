<template>
  <div class="toggle-switch--container">
    <div>{{ labels.off }}</div>
    <input
      :id="`switch${$.uid}`"
      v-model="localState"
      type="checkbox"
      :name="`switch${$.uid}`"
    />
    <label :for="`switch${$.uid}`"></label>
    <div>{{ labels.on }}</div>
  </div>
</template>

<script lang="ts">
import { defineComponent, PropType } from "vue";

export default defineComponent({
  name: "VToggleSwitch",
  props: {
    state: {
      type: Boolean,
      default: false,
    },
    labels: {
      type: Object as PropType<{ on: string; off: string }>,
      default: () => ({ on: "", off: "" }),
    },
  },
  emits: ["selection"],
  data() {
    return {
      localState: this.state,
    };
  },
  watch: {
    state: {
      immediate: true,
      deep: false,
      handler: function onState(val: boolean) {
        this.localState = val;
        this.$emit("selection", val);
      },
    },
    localState(val: boolean) {
      this.$emit("selection", val);
    },
  },
});
</script>

<style lang="scss" scoped>
/*== Toggle Switch ==
 * credit goes https://dev.to/alvaromontoro/how-to-create-a-progress-bar-with-html-and-css-1fl5
*/
$width: 50;
$height: 30;
.toggle-switch--container {
  display: flex;
  justify-content: center;
  align-items: center;

  div {
    margin: 0 4px;
  }

  input[type="checkbox"] {
    width: 0;
    height: 0;
    visibility: hidden;
  }

  label {
    width: $width + px;
    height: $height + px;
    background-color: var(--color--black);
    border-radius: 100px;
    position: relative;
    cursor: pointer;
    transition: 0.5s;
    box-shadow: inset 0 0 10px #00000050;

    &::after {
      content: "";
      width: 26px;
      height: 26px;
      background-color: var(--color--white);
      position: absolute;
      border-radius: 70px;
      top: 2px;
      left: 2px;
      transition: 0.5s;
    }
  }

  input:checked + label:after {
    left: calc(100% - 2px);
    transform: translateX(-100%);
  }

  input:checked + label {
    background-color: var(--color--primary);
  }

  label:active:after {
    width: 30px;
  }
}
</style>
