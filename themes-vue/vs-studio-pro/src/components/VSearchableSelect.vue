<template>
  <div v-outside-click="close" class="searchable-dropdown">
    <div class="searchable-dropdown__input">
      <input
        v-model="searchText"
        type="text"
        placeholder="Search ..."
        x-blur="toggle(false)"
        @input="handleInput"
        @keyup="setSelection"
        @focus="open"
      />
      <button type="button" @click="emitSelection" title="submit">
        <Icon icon="search"></Icon>
      </button>
    </div>
    <select
      ref="sel"
      title="search result list"
      @change="onChange"
      @keyup.enter="close"
      @mouseup="selectItem"
      @blur="close"
    >
      <option value=""></option>
      <option v-for="(opt, idx) in localOptions" :key="idx" :value="opt.value">
        {{ opt.text }}
      </option>
    </select>
  </div>
</template>

<script lang="ts">
import { defineComponent, PropType } from "vue";
import Icon from "./VIcon.vue";
import { outsideClick } from "../directives";

export interface ISearchableSelectOption {
  text: string;
  value: string;
}

export default defineComponent({
  name: "VSearchableSelect",
  directives: {
    "outside-click": outsideClick,
  },
  components: {
    Icon,
  },
  props: {
    options: {
      type: Array as PropType<ISearchableSelectOption[]>,
      default: () => new Array<ISearchableSelectOption>(),
    },
  },
  emits: ["selection"],
  data() {
    return {
      searchText: "",
      localOptions: this.options,
      selectedOpt: {
        text: "",
        value: "",
      },
    };
  },
  watch: {
    options: {
      deep: false,
      immediate: true,
      handler: function onOptions(opt: ISearchableSelectOption[]) {
        this.localOptions = opt;
      },
    },
  },
  methods: {
    setSelection(e: KeyboardEvent) {
      const cnt = this.localOptions.length;
      const cur = (this.$refs.sel as HTMLSelectElement).selectedIndex;
      let idx = 0;
      switch (e.key) {
        case "ArrowDown":
          if (cur === cnt) idx = 0;
          else idx = cur + 1;
          (this.$refs.sel as HTMLSelectElement).selectedIndex = idx;
          this.selectedOpt = {
            value: (this.$refs.sel as HTMLSelectElement).selectedOptions[0]
              .value,
            text: (this.$refs.sel as HTMLSelectElement).selectedOptions[0].text,
          };
          this.searchText = this.selectedOpt.text;
          break;
        case "ArrowUp":
          if (cur === 0) idx = cnt;
          else idx = cur - 1;
          (this.$refs.sel as HTMLSelectElement).selectedIndex = idx;
          this.selectedOpt = {
            value: (this.$refs.sel as HTMLSelectElement).selectedOptions[0]
              .value,
            text: (this.$refs.sel as HTMLSelectElement).selectedOptions[0].text,
          };
          this.searchText = this.selectedOpt.text;
          break;
        case "Enter":
          this.close();
          this.emitSelection();
          break;
        case "Backspace":
          this.open();
          break;
      }
    },
    handleInput(e: Event) {
      const txt = (e.target as HTMLInputElement).value;
      if (txt.length === 0) {
        this.localOptions = this.options;
        const cnt = this.localOptions.length;
        (this.$refs.sel as HTMLSelectElement).size = cnt < 10 ? cnt + 1 : 10;
        this.searchText = "";
        this.selectedOpt.value = "";
        this.selectedOpt.text = "";
      } else {
        this.localOptions = this.options.filter((x) => x.text.includes(txt));
        const cnt = this.localOptions.length;
        if (cnt > 0 && cnt < 11) {
          (this.$refs.sel as HTMLSelectElement).size = cnt + 1;
        }
      }
      (this.$refs.sel as HTMLSelectElement).selectedIndex = 0;
    },
    selectItem(e: MouseEvent) {
      // selecting an item from the list
      const el = e.target as HTMLSelectElement | HTMLOptionElement;
      if ("selectedOptions" in el) {
        // as HTMLSelectElement
        this.selectedOpt = {
          value: el.selectedOptions[0].value,
          text: el.selectedOptions[0].text,
        };
        this.searchText = this.selectedOpt.text;
      } else {
        // as HTMLOptionElement
        this.selectedOpt.value = el.value;
        this.selectedOpt.text = el.text;
        this.searchText = el.text;

        this.localOptions = this.options.filter((x) =>
          x.text.includes(el.text)
        );
        const cnt = this.localOptions.length;
        if (cnt > 0 && cnt < 11) {
          (this.$refs.sel as HTMLSelectElement).size = cnt + 1;
        }

        this.close();
        this.emitSelection();
      }
    },
    close() {
      (this.$refs.sel as HTMLSelectElement).size = 1;
      (this.$refs.sel as HTMLSelectElement).style.opacity = "0";
    },
    open() {
      const cnt = this.localOptions.length;
      (this.$refs.sel as HTMLSelectElement).size = cnt < 10 ? cnt + 1 : 10;
      (this.$refs.sel as HTMLSelectElement).style.opacity = "1";
    },
    onChange(e: Event) {
      const el = e.target as HTMLSelectElement;
      this.selectedOpt = {
        value: el.selectedOptions[0].value,
        text: el.selectedOptions[0].text,
      };
      this.searchText = this.selectedOpt.text;
    },
    emitSelection() {
      if (this.selectedOpt.value === "" && this.searchText !== "") {
        this.selectedOpt.text = this.searchText;
        this.selectedOpt.value = this.searchText;
      }
      this.$emit("selection", this.selectedOpt);
    },
  },
});
</script>

<style lang="scss" scoped>
.searchable-dropdown {
  input {
    margin-bottom: 0;
  }

  select {
    width: 100%;
    height: auto;
    opacity: 0;
    font-size: 14px;
  }

  & .searchable-dropdown__input {
    display: flex;

    [type="text"] {
      border-right: none;
      border-top-right-radius: 0px;
      border-bottom-right-radius: 0px;
    }

    button {
      font-size: 24px;
      background: var(--color--white);
      border: 1px solid var(--color--gray2);
      border-top-right-radius: 4px;
      border-bottom-right-radius: 4px;
      width: 2.4rem;
      height: 2.4rem;
      padding: 2px 0 0 2px;

      &:hover {
        background: var(--color--black);
        color: var(--color--white);
      }
    }
  }
}
</style>
