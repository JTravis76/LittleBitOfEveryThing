<template>
  <div class="text-tag-field text-tag-field--dirty">
    <div class="text-tag-field__eyebrow">
      <label>{{ placeholder }}</label>
    </div>
    <div class="text-tag-field__input-wrap">
      <input
        v-model="inputValue"
        type="text"
        title="enter tag label"
        @keyup.enter="addTag"
      />
      <button type="button" title="add tag" @click="addTag">
        <Icon icon="add"></Icon>
      </button>
    </div>
    <div class="tags">
      <template v-for="tag in tags" :key="tag">
        <button type="button" class="tag" @click="$emit('remove', tag)">
          {{ tag }} <span>&times;</span>
        </button>
      </template>
    </div>
  </div>
</template>

<script lang="ts">
import { defineComponent, PropType } from "vue";
import Icon from "./VIcon.vue";

export default defineComponent({
  name: "TextTagField",
  components: {
    Icon,
  },
  props: {
    placeholder: {
      type: String,
      default: "",
    },
    tags: {
      type: Array as PropType<string[]>,
      default: () => new Array<string>(),
    },
  },
  emits: ["tag", "remove"],
  data() {
    return {
      inputValue: "",
    };
  },
  methods: {
    addTag() {
      if (this.inputValue.trim() !== "") {
        this.$emit("tag", this.inputValue);
        this.inputValue = "";
      }
    },
  },
});
</script>

<style lang="scss" scoped>
.text-tag-field {
  min-height: 54px;
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

    button {
      border: 1px solid transparent;
      border-left: none;
      border-radius: 0 4px 4px 0;
      background-color: transparent;
      float: right;
      height: 33px;
      width: 40px;
      font-size: 20px;
      cursor: pointer;

      &:hover {
        background-color: var(--color--black);
        color: var(--color--white);
      }
    }
  }

  input {
    margin: 0px;
    height: 32px;
    border: 1px solid transparent;
    border-right: none;
    border-radius: 4px;
    padding: 0 0 0 4px;
    width: calc(100% - 42px);
    font-size: 14px;
  }

  &--dirty {
    .text-tag-field__eyebrow {
      label {
        top: -5px;
        opacity: 1;
      }
    }
  }

  .tags {
    height: 23px;
    display: flex;
    flex-wrap: wrap;

    .tag {
      background-color: var(--color--gray4);
      border: 1px solid var(--color--gray4);
      border-radius: 4px;
      color: var(--color--white);
      font-size: 0.8rem;
      margin: 2px 3px;
      padding: 4px;

      &:hover {
        background-color: var(--color--primary);
      }

      &:focus-visible {
        outline: 3px solid var(--color--primary);
        outline-offset: 1px;
      }
    }
  }
}
</style>
