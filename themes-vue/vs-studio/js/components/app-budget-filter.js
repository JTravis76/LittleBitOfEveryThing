export default {
  template: `
<div>
  <input type="text" v-model="filters.search" title="search" />
  <input type="date" v-model="filters.assignedDates.start" title="starting date" />
  <input type="date" v-model="filters.assignedDates.end" title="ending date" />
  <input type="number" v-model="filters.amount.start" title="amount from" />
  <input type="number" v-model="filters.amount.end" title="amount to" />
  <button type="button" @click="$emit('filters', filters)">Apply</button>
</div>`,
  name: "app-budget-filter",
  emits: ["filters"],
  data() {
    return {
      filters: {
        search: "",
        assignedDates: {
          end: "",
          start: "",
        },
        amount: {
          end: 0,
          start: 0,
        },
      },
    };
  },
};
