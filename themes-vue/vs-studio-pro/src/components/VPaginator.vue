<template>
  <div class="paginator">
    <div class="paginator--page-size">
      <div class="paginator--page-size-label">Items per page:</div>
      <select ref="pagesize" title="page size" @change="paginate(1)">
        <template v-for="opt in pageSizes" :key="opt">
          <option :value="opt">{{ opt }}</option>
        </template>
      </select>
    </div>
    <div class="paginator--range-actions">
      <div class="paginator--range-label">{{ page }} of {{ total }}</div>
      <button
        type="button"
        title="first page"
        :disabled="total == 0 || page == 1"
        @click="paginate(1)"
      >
        <span class="mat-button-wrapper"
          ><svg viewBox="0 0 24 24" focusable="false" class="paginator--icon">
            <path
              d="M18.41 16.59L13.82 12l4.59-4.59L17 6l-6 6 6 6zM6 6h2v12H6z"
            ></path>
          </svg>
        </span>
      </button>
      <button
        type="button"
        title="previous page"
        :disabled="total == 0 || page == 1"
        @click="paginate(page - 1)"
      >
        <span class="mat-button-wrapper">
          <svg viewBox="0 0 24 24" focusable="false" class="paginator--icon">
            <path d="M15.41 7.41L14 6l-6 6 6 6 1.41-1.41L10.83 12z"></path>
          </svg>
        </span>
      </button>
      <button
        type="button"
        title="next page"
        :disabled="total == 0 || page == total"
        @click="paginate(page + 1)"
      >
        <span class="mat-button-wrapper">
          <svg viewBox="0 0 24 24" focusable="false" class="paginator--icon">
            <path d="M10 6L8.59 7.41 13.17 12l-4.58 4.59L10 18l6-6z"></path>
          </svg>
        </span>
      </button>
      <button
        type="button"
        title="last page"
        :disabled="total == 0 || page == total"
        @click="paginate(total)"
      >
        <span class="mat-button-wrapper">
          <svg viewBox="0 0 24 24" focusable="false" class="paginator--icon">
            <path
              d="M5.59 7.41L10.18 12l-4.59 4.59L7 18l6-6-6-6zM16 6h2v12h-2z"
            ></path>
          </svg>
        </span>
      </button>
    </div>
  </div>
</template>

<script lang="ts">
import { defineComponent, PropType } from "vue";

export default defineComponent({
  name: "VPaginator",
  props: {
    pageSizes: {
      type: Array as PropType<string[]>,
      default: () => ["0"],
    },
    page: {
      type: Number,
      default: 0,
    },
    total: {
      type: Number,
      default: 0,
    },
  },
  emits: ["paginate"],
  methods: {
    paginate(page: number) {
      if (page === 0) page = 1;
      if (page > this.total) page = this.total;

      this.$emit("paginate", {
        page,
        pagesize: parseInt(
          (this.$refs.pagesize as HTMLSelectElement).value,
          10
        ),
      });
    },
  },
});
</script>

<style lang="scss" scoped>
.paginator {
  background-color: var(--color--white);
  display: flex;
  align-items: center;
  justify-content: flex-end;
  padding: 0 8px;
  flex-wrap: wrap-reverse;
  width: 100%;
  height: 72px;

  .paginator--page-size {
    display: flex;
    align-items: baseline;
    margin-right: 8px;

    .paginator--page-size-label {
      margin: 0 4px;
      font-size: 12px;
      color: rgba(0, 0, 0, 0.54);
    }

    select {
      width: 64px;
      height: 40px;
      display: inline-block;
      position: relative;
      text-align: left;
      margin: 6px 4px 0 4px;

      border: 1px solid var(--color--gray2);
      border-radius: 4px;

      font-size: 12px;
      font-weight: 400;
      line-height: 1.125;
      font-family: Roboto, "Helvetica Neue", sans-serif;
      letter-spacing: normal;
      color: rgba(0, 0, 0, 0.87);
    }
  }

  .paginator--range-actions {
    display: flex;
    align-items: center;

    .paginator--range-label {
      margin: 0 32px 0 24px;
      font-size: 12px;
      color: rgba(0, 0, 0, 0.54);
    }

    button {
      font-family: Roboto, "Helvetica Neue", sans-serif;
      font-size: 14px;
      font-weight: 500;

      color: inherit;
      background: transparent;

      box-sizing: border-box;
      position: relative;
      -webkit-user-select: none;
      user-select: none;
      cursor: pointer;
      outline: none;
      border: none;
      -webkit-tap-highlight-color: rgba(0, 0, 0, 0);
      display: inline-block;
      white-space: nowrap;
      text-decoration: none;
      vertical-align: baseline;
      text-align: center;
      margin: 0;
      min-width: 64px;
      line-height: 36px;
      padding: 0 16px;
      border-radius: 4px;
      overflow: visible;

      padding: 0;
      min-width: 0;
      width: 40px;
      height: 40px;
      flex-shrink: 0;
      line-height: 40px;
      border-radius: 50%;

      &:disabled {
        .mat-button-wrapper {
          color: rgba(0, 0, 0, 0.26);
          cursor: default;
        }
      }
    }

    .mat-button-wrapper {
      color: rgba(0, 0, 0, 0.54);
    }

    .paginator--icon {
      width: 28px;
      fill: currentColor;

      vertical-align: middle;
    }
  }
}
</style>
