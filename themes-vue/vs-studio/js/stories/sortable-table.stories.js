import SortableTable from "../components/v-sortable-table.js";

export default {
  template: `
<div class="row">
  <div class="col-md-8">
    <SortableTable :cols="cols" :rows="items" @complete="sort($event)">
      <template #thead>
        <th>Name</th>
        <th>Type</th>
      </template>
    </SortableTable>
  </div>
  <div class="col-md-4">
    <fieldset>
      <legend>Info</legend>
      This story displays how to use the sortable table component.
    </fieldset>
  </div>
</div>
`,
  name: "sortable-table-story",
  components: {
    SortableTable,
  },
  data() {
    return {
      cols: ["name", "type"],
      items: [
        { id: 1, name: "Johnny Blaze", type: "Science Fiction", order: 0 },
        { id: 2, name: "John Wick", type: "Action", order: 1 },
        { id: 3, name: "John Wayne", type: "Western", order: 2 },
      ],
    };
  },
  methods: {
    sort(data) {
      this.items = data;
    },
  },
};
