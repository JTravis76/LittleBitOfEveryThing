<template>
  <div class="row">
    <div class="eight columns">
      <SortableTable :cols="cols" :rows="items" @complete="sort($event)">
        <template #thead>
          <th>Name</th>
          <th>Type</th>
        </template>
      </SortableTable>
    </div>
    <div class="four columns">
      <fieldset>
        <legend>Info</legend>
        <p>
          This story demos how to setup a sortable table. The component must
          include a list of column names that matches the properties of the item
          array. This list is used to display the values from the object.
        </p>
      </fieldset>
    </div>
  </div>
</template>

<script lang="ts">
import { defineComponent } from "vue";
import SortableTable from "../components/VSortableTable.vue";

interface IsortableData {
  id: number;
  name: string;
  type: string;
  order: number;
}

export default defineComponent({
  name: "SortableTableStory",
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
      ] as IsortableData[],
    };
  },
  methods: {
    sort(data: IsortableData[]) {
      this.items = data;
    },
  },
});
</script>

<style lang="scss"></style>
