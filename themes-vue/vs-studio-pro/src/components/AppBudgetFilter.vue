<template>
  <div>
    <input
      type="text"
      v-model="filters.search"
      title="search"
      placeholder="Search"
    />
    <input
      type="date"
      v-model="filters.assignedDates.start"
      title="starting date"
      placeholder="Starting date"
    />
    <input
      type="date"
      v-model="filters.assignedDates.end"
      title="ending date"
      placeholder="Ending date"
    />
    <input
      type="number"
      v-model="filters.amount.start"
      title="amount from"
      placeholder="Amount from"
    />
    <input
      type="number"
      v-model="filters.amount.end"
      title="amount to"
      placeholder="Amount to"
    />
    <input type="checkbox" v-model="filters.tag" /> No Tag
    <button type="button" @click="$emit('filters', filters)">Apply</button>
  </div>
</template>

<script lang="ts">
import { defineComponent } from "vue";

export interface IBudgetFilter {
  search: string;
  tag: boolean;
  assignedDates: {
    end: string;
    start: string;
  };
  amount: {
    end: string;
    start: string;
  };
}

export default defineComponent({
  name: "AppBudgetFilter",
  emits: ["filters"],
  data() {
    return {
      filters: {
        search: "",
        tag: false,
        assignedDates: {
          end: "",
          start: "",
        },
        amount: {
          end: "",
          start: "",
        },
      } as IBudgetFilter,
    };
  },
});
</script>

<style lang="scss"></style>
