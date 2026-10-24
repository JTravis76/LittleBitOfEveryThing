<template>
  <dialog ref="dlg" :class="`modal-${size}`">
    <div class="modal__header">
      <div>
        <h6>{{ title }}</h6>
      </div>
      <div>
        <button type="button" title="close" @click="close()">
          <Icon icon="close"></Icon>
        </button>
      </div>
    </div>
    <slot></slot>
  </dialog>
</template>

<script lang="ts">
import { defineComponent, PropType } from "vue";
import Icon from "./VIcon.vue";

export default defineComponent({
  name: "ModalFrame",
  components: {
    Icon,
  },
  props: {
    title: {
      type: String as PropType<string | null>,
      default: "",
    },
    size: {
      type: String,
      default: "md",
    },
  },
  emits: ["close"],
  mounted() {
    this.$nextTick(() => {
      const dlg = this.$refs.dlg as HTMLDialogElement;
      dlg.addEventListener("click", (e) => {
        const dlgDimensions = dlg.getBoundingClientRect();
        if (
          e.clientX < dlgDimensions.left ||
          e.clientX > dlgDimensions.right ||
          e.clientY < dlgDimensions.top ||
          e.clientY > dlgDimensions.bottom
        ) {
          dlg.close();
        }
      });
    });
  },
  methods: {
    open() {
      (this.$refs.dlg as HTMLDialogElement).showModal();
    },
    close() {
      (this.$refs.dlg as HTMLDialogElement).close();
    },
  },
});
</script>

<style lang="scss" scoped>
dialog {
  margin: 80px auto; // move up but keep centered
  border: 1px solid transparent;
  border-radius: 4px;
  background-color: var(--color--white);
  max-height: calc(100% - 155px); // stop just before the footer
  padding-top: 4px;

  &.modal-md {
    width: 414px;
  }

  &.modal-lg {
    width: 600px;
  }

  &::backdrop {
    background-color: rgba(0, 0, 0, 0.2);
  }

  .modal__header {
    display: flex;
    justify-content: space-between;
    height: 34px;
    border-bottom: 1px solid var(--color--gray2);

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

    h6 {
      margin: 0;
    }
  }
}
</style>
