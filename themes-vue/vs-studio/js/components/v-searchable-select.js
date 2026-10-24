import outsideClick from "../directives/outside-click.js";

export default {
  template: `
<div class="searchable-dropdown" v-outside-click="close">
  <div class="searchable-dropdown__input">
    <input type="text"
      v-model="searchText"
      placeholder="Search ..."
      @input="handleInput"
      @keyup="setSelection"
      @focus="open"
      x-blur="toggle(false)"
    />
    <button type="button" @click="emitSelection">Go</button>  
  </div>
  <select
    ref="sel"
    @change="onChange"
    @keyup.enter="close"
    @mouseup="selectItem"
    @blur="close"
  >
    <option value=""></option>
    <option
      v-for="(opt, idx) in localOptions"
      :key="idx"
      :value="opt.value">
      {{ opt.text }}
    </option>
  </select>
</div>
`,
  name: "v-searchable-select",
  directives: {
    "outside-click": outsideClick,
  },
  props: {
    options: {
      type: Array,
      default: () => [],
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
  created() {},
  methods: {
    setSelection(e) {
      const cnt = this.localOptions.length;
      const cur = this.$refs.sel.selectedIndex;
      let idx = 0;
      switch (e.key) {
        case "ArrowDown":
          if (cur === cnt) idx = 0;
          else idx = cur + 1;
          this.$refs.sel.selectedIndex = idx;
          this.selectedOpt = {
            value: this.$refs.sel.selectedOptions[0].value,
            text: this.$refs.sel.selectedOptions[0].text,
          };
          this.searchText = this.selectedOpt.text;
          break;
        case "ArrowUp":
          if (cur === 0) idx = cnt;
          else idx = cur - 1;
          this.$refs.sel.selectedIndex = idx;
          this.selectedOpt = {
            value: this.$refs.sel.selectedOptions[0].value,
            text: this.$refs.sel.selectedOptions[0].text,
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
    handleInput(e) {
      const txt = e.target.value;
      if (txt.length === 0) {
        this.localOptions = this.options;
        this.$refs.sel.size = 10;
        this.searchText = "";
        this.selectedOpt.value = "";
        this.selectedOpt.text = "";
      } else {
        this.localOptions = this.options.filter((x) => x.text.includes(txt));
        const cnt = this.localOptions.length;
        if (cnt > 0 && cnt < 11) {
          this.$refs.sel.size = cnt + 1;
        }
      }
      this.$refs.sel.selectedIndex = 0;
    },
    selectItem(e) {
      // selecting an item from the list
      const el = e.target;
      if (el["selectedOptions"]) {
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
          this.$refs.sel.size = cnt + 1;
        }

        this.close();
        this.emitSelection();
      }
    },
    close() {
      this.$refs.sel.size = 1;
      this.$refs.sel.style.opacity = 0;
    },
    open() {
      const cnt = this.localOptions.length;
      this.$refs.sel.size = cnt < 10 ? cnt + 1 : 10;
      this.$refs.sel.style.opacity = 1;
    },
    onChange(e) {
      const el = e.target;
      this.selectedOpt = {
        value: el.selectedOptions[0].value,
        text: el.selectedOptions[0].text,
      };
      this.searchText = this.selectedOpt.text;
    },
    emitSelection() {
      this.$emit("selection", this.selectedOpt);
    },
  },
};
