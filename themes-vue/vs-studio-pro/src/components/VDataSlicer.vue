<template>
  <div class="data-slicer">
    <div class="slicer_header-panel" :class="{ active: localSlicer.active }">
      <button
        type="button"
        class="slicer_header"
        @click="toggle()"
        title="expand or collapse"
      >
        <span>{{ localSlicer.title }}</span>
      </button>
      <div class="slicer-toolset">
        <button type="button" @click="reset()" title="clear slicer">
          &times;
        </button>
      </div>
    </div>

    <div v-show="!localSlicer.collapse">
      <template
        v-if="localSlicer.type === 'checkbox' && localSlicer.data.length > 0"
      >
        <div class="slicer-body">
          <template
            v-for="(d, idx) in (slicer.data as ISlicerData[])"
            :key="idx"
          >
            <label :data-idx="idx">
              <input type="checkbox" :value="d.value" v-model="d.selected" />
              <span>{{ d.text }}</span>
            </label>
          </template>
        </div>
      </template>
      <template v-if="localSlicer.type === 'input'">
        <div class="slicer-body">
          <input
            v-model="localSlicer.data"
            type="text"
            placeholder="search.."
            @input="onChange"
            @keyup.enter="$emit('selection', localSlicer)"
          />
        </div>
      </template>
    </div>
  </div>
</template>

<script lang="ts">
import { defineComponent, PropType } from "vue";

type SlicerType = "checkbox" | "input";

export interface ISlicerData {
  text: string;
  value: string | null;
  selected: boolean;
}

export interface ISlicer {
  title: string;
  active: boolean;
  collapse: boolean;
  type: SlicerType;
  data: ISlicerData[] | string;
}

export class Slicer implements ISlicer {
  constructor(init?: Partial<ISlicer>) {
    this.title = "";
    this.active = false;
    this.collapse = true;
    this.type = "input";
    this.data = "";

    if (init) Object.assign(this, init);
  }
  title: string;
  active: boolean;
  collapse: boolean;
  type: SlicerType;
  data: string | ISlicerData[];
}

export default defineComponent({
  name: "DataSlicer",
  props: {
    slicer: {
      type: Object as PropType<ISlicer>,
      default: new Slicer(),
    },
  },
  emits: ["selection"],
  data() {
    return {
      localSlicer: this.slicer,
    };
  },
  watch: {
    localSlicer: {
      immediate: false,
      deep: true,
      handler: function onSlicers(slicer: ISlicer) {
        if (typeof slicer.data === "string") {
          slicer.active = slicer.data.length > 0;
        } else {
          slicer.active = slicer.data.filter((x) => x.selected).length > 0;
          this.$emit("selection", slicer);
        }
      },
    },
  },
  methods: {
    toggle() {
      this.localSlicer.collapse = !this.localSlicer.collapse;
    },
    reset() {
      if (typeof this.localSlicer.data === "string") {
        this.localSlicer.data = "";
        this.localSlicer.active = false;
      } else {
        this.localSlicer.data.forEach((x: any) => {
          const item = x;
          item.selected = false;
        });
      }

      this.$emit("selection", this.localSlicer);
    },
    // eslint-disable-next-line
    onChange(e: any | InputEvent) {
      this.localSlicer.data = e.target.value;
      this.localSlicer.active = this.localSlicer.data.length > 0;
    },
  },
});
</script>

<style lang="scss" scoped>
.data-slicer {
  border: none;
  border-bottom: 1px solid gray;
  margin: 2px 2px;

  button {
    display: block;
    border: none;
    cursor: pointer;
  }

  .slicer_header-panel {
    display: flex;
    justify-content: space-between;
    background: lightgray;

    &:hover {
      background-color: var(--color--primary);

      button {
        color: var(--purple3);
      }
    }

    .slicer_header {
      background: transparent;
      width: 100%;
      height: 1.5rem;
      padding: 0px 10px;
      text-align: left;
    }

    .slicer-toolset {
      button {
        border: none;
        background: transparent;
        height: 1.5rem;
        padding: 0px 10px;

        &:hover {
          border-radius: 50%;
          background-color: rgba(0, 0, 0, 0.2);
        }
      }
    }

    &.active {
      background-color: var(--color--primary);

      button {
        color: var(--purple3);
      }
    }
  }

  .slicer-body {
    background-color: white;
    max-height: 200px;
    overflow-x: auto;

    input {
      margin-bottom: 0px;
    }

    label {
      margin-left: 8px;

      span {
        margin-left: 4px;
        cursor: pointer;
      }
    }
  }
}
</style>
