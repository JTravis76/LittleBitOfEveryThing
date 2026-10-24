<template>
  <div class="select-field" :class="modifiers">
    <div class="select-field__eyebrow">
      <label>{{ placeholder }}</label>
    </div>
    <div class="select-field__select-wrap">
      <select :disabled="disabled" :title="placeholder" @change="setSelection">
        <template v-for="opt in options" :key="opt.value">
          <option
            v-if="(opt.disabled && opt.value === selected) || !opt.disabled"
            :value="opt.value"
            :selected="opt.selected || opt.value === selected"
          >
            {{ opt.text }}
          </option>
        </template>
      </select>
    </div>
  </div>
</template>

<script lang="ts">
import { defineComponent, PropType } from "vue";

export interface ISelect {
  value: string;
  text: string;
  selected: boolean;
  disabled: boolean;
}

export class Select implements ISelect {
  constructor(init?: Partial<Select>) {
    this.disabled = false;
    this.selected = false;
    this.text = "";
    this.value = "";

    if (init) Object.assign(this, init);
  }
  value: string;
  text: string;
  selected: boolean;
  disabled: boolean;
}

export default defineComponent({
  name: "SelectField",
  props: {
    options: {
      type: Array as PropType<ISelect[]>,
      default: () => [
        { value: "", text: "", selected: false, disabled: false },
      ],
    },
    selected: {
      type: String as PropType<string | null>,
      required: false,
      default: "",
    },
    placeholder: {
      type: String,
      default: "",
    },
    disabled: {
      type: Boolean,
      default: false,
    },
  },
  emits: ["selection"],
  data() {
    return {
      modifiers: {
        "select-field--disabled": this.disabled,
        "select-field--valid": false,
        "select-field--invalid": false,
        "select-field--dirty": false,
      },
    };
  },
  watch: {
    options: {
      immediate: true,
      deep: true,
      handler: function onOptions(val: ISelect[]) {
        this.modifiers["select-field--dirty"] = false;
        if (val && val.filter((x) => x.selected).length > 0) {
          this.modifiers["select-field--dirty"] = true;
        }
      },
    },
    selected: {
      immediate: true,
      deep: false,
      handler: function onSelected(val: string) {
        this.modifiers["select-field--dirty"] = false;
        if (val && val.trim() !== "") {
          this.modifiers["select-field--dirty"] = true;
        }
      },
    },
  },
  methods: {
    setSelection(e: Event) {
      const el = e.target as HTMLSelectElement;
      const selected = el.selectedOptions[0].value.toString();
      this.options.forEach((opt) => {
        opt.selected = false;
        if (opt.value == selected) {
          opt.selected = true;
          this.$emit("selection", opt);
        }
      });
    },
  },
});
</script>

<style lang="scss" scoped>
.select-field {
  width: 100%;
  height: 54px;
  margin: 4px 0px;

  &__eyebrow {
    height: 20px;

    label {
      padding-left: 4px;
      font-size: 12px;
      position: relative;
      top: -4px;
      color: var(--color--gray3);
      transition: opacity 0.3s ease-in-out;
      opacity: 1;
    }
  }

  &__select-wrap {
    border: 1px solid var(--color--gray2);
    border-radius: 4px;
    background-color: var(--color--white);
    width: 100%;
    height: 34px;

    select {
      margin: 0px;
      height: 32px;
      border: 1px solid transparent;
      border-right: none;
      border-radius: 4px;
      padding: 0 0 0 4px;
      width: 100%;
      font-size: 14px;
      color: var(--color--gray3);
    }
  }

  &--dirty {
    .select-field__eyebrow {
      label {
        top: -4px;
        opacity: 1;
      }
    }
  }

  &--disabled {
    .select-field__select-wrap {
      background-color: var(--color--gray2);
      select {
        cursor: not-allowed;
      }
    }
  }
}
</style>
