<template>
  <div class="text-field" :class="modifiers">
    <div class="text-field__eyebrow">
      <label>{{ placeholder }}</label>
    </div>
    <div class="text-field__input-wrap">
      <input
        :value="modelValue"
        type="text"
        :placeholder="placeholder"
        @input="onInput"
      />
    </div>
  </div>
</template>

<script lang="ts">
import { PropType, defineComponent } from "vue";

export default defineComponent({
  name: "TextField",
  props: {
    modelValue: {
      type: [String, Number] as PropType<string | number | null>,
      default: "",
    },
    placeholder: {
      type: String,
      default: "",
    },
  },
  emits: ["update:modelValue"],
  data() {
    return {
      modifiers: {
        "text-field__input--active": false,
        "text-field__input--valid": false,
        "text-field__input--invalid": false,
        "text-field__input--dirty": false,
        "text-field__input--disabled": false,
        "text-field--valid": false,
        "text-field--invalid": false,
        "text-field--dirty": false,
      },
    };
  },
  watch: {
    modelValue: {
      immediate: true,
      deep: false,
      handler: function onModelValue(val?: string | number | null) {
        this.modifiers["text-field--dirty"] = false;
        if (val) {
          if (
            typeof val === "number" ||
            (typeof val === "string" && val.trim() !== "")
          )
            this.modifiers["text-field--dirty"] = true;
        }
      },
    },
  },
  methods: {
    onInput(e: Event) {
      const el = e.target as HTMLInputElement;
      this.$emit("update:modelValue", el.value);
    },
  },
});
</script>

<style lang="scss" scoped>
.text-field {
  height: 54px;
  margin: 4px 0px;
  width: 100%;

  &__eyebrow {
    height: 14px;

    label {
      padding-left: 4px;
      font-size: 12px;
      position: relative;
      top: -4px;
      color: var(--color--gray3);
      transition: opacity 0.3s ease-in-out;
      opacity: 0;
    }
  }

  &__input-wrap {
    border: 1px solid var(--color--gray2);
    border-radius: 4px;
    background-color: var(--color--white);
    width: 100%;
    height: 34px;
    margin: 6px 4px 4px 0;
  }

  input {
    margin: 0px;
    height: 32px;
    border: 1px solid transparent;
    border-right: none;
    border-radius: 4px;
    padding: 0 0 0 4px;
    width: 100%;
    font-size: 14px;
  }

  &--dirty {
    .text-field__eyebrow {
      label {
        top: -5px;
        opacity: 1;
      }
    }
  }
}
</style>
