<template>
  <div class="date-field" :class="modifiers">
    <div class="date-field__eyebrow">
      <span>{{ placeholder }}</span>
    </div>
    <div class="date-field__input-wrap">
      <input
        :value="modelValue"
        type="date"
        :max="$filters.dateTypeString(max)"
        :placeholder="placeholder"
        :disabled="disabled"
        @input="onInput"
      />
    </div>
  </div>
</template>

<script lang="ts">
import { PropType, defineComponent } from "vue";

export default defineComponent({
  name: "DateField",
  props: {
    modelValue: {
      type: String as PropType<string | null>,
      default: "",
    },
    placeholder: {
      type: String,
      default: "",
    },
    max: {
      type: String,
      default: "",
    },
    disabled: {
      type: Boolean,
      default: false,
    },
  },
  emits: ["update:modelValue"],
  data() {
    return {
      modifiers: {
        "date-field__input--active": false,
        "date-field__input--valid": false,
        "date-field__input--invalid": false,
        "date-field__input--dirty": false,
        "date-field__input--disabled": this.disabled,
        "date-field--valid": false,
        "date-field--invalid": false,
        "date-field--dirty": true,
      },
    };
  },
  watch: {
    disabled(v: boolean) {
      this.modifiers["date-field__input--disabled"] = v;
    },
    //   modelValue: {
    //     immediate: true,
    //     deep: false,
    //     handler: function onModelValue(val: string) {
    //       this.modifiers["date-field--dirty"] = false;
    //       if (val && val.trim() !== "") {
    //         this.modifiers["date-field--dirty"] = true;
    //       }
    //     },
    //   },
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
// .date-field {
//   // height: 54px;
//   // margin: 4px 0px;
//   // width: 100%;

//   &__eyebrow {
//     //height: 14px;

//     label {
//       // padding-left: 4px;
//       // font-size: 12px;
//       // position: relative;
//       // top: -4px;
//       // color: var(--color--gray3);
//       // transition: opacity 0.3s ease-in-out;
//       // opacity: 0;
//     }
//   }

//   &__input-wrap {
//     // border: 1px solid var(--color--gray2);
//     // border-radius: 4px;
//     // background-color: var(--color--white);
//     // width: 100%;
//     // height: 34px;
//     // margin: 6px 4px 4px 0;
//   }

//   input {
//     // margin: 0px;
//     // height: 32px;
//     // border: 1px solid transparent;
//     // border-right: none;
//     // border-radius: 4px;
//     // padding: 0 0 0 4px;
//     // width: 100%;
//     // font-size: 14px;
//   }

// &--dirty {
//   .date-field__eyebrow {
//     label {
//       top: -5px;
//       opacity: 1;
//     }
//   }
// }

// &__input--disabled {
//   input {
//     cursor: not-allowed;
//     background: var(--color--gray1);
//   }
// }
//}
</style>
