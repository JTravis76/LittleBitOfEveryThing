<template>
  <div class="row">
    <div class="eight columns">
      <Paginator
        :page-sizes="['5', '10', '20']"
        :page="page"
        :total="total"
        @paginate="paginate($event)"
      ></Paginator>
      <table class="u-full-width">
        <thead>
          <tr>
            <th>Column</th>
          </tr>
        </thead>
        <tbody>
          <tr v-for="item in items" :key="item">
            <td>Row {{ item }}</td>
          </tr>
        </tbody>
      </table>
    </div>
    <div class="four columns">
      <fieldset>
        <legend>Info</legend>
        This story shows how to use the paginator controls to select a set of
        data within a table.
      </fieldset>
    </div>
  </div>
</template>

<script lang="ts">
import { defineComponent } from "vue";
import Paginator from "../components/VPaginator.vue";

export default defineComponent({
  name: "PaginatorStory",
  components: {
    Paginator,
  },
  data() {
    return {
      page: 1,
      pageSize: 5,
      total: 0,
      items: new Array<number>(),
    };
  },
  created() {
    // set the initial paginator
    this.page = 1;
    this.pageSize = 5;

    this.getData();
  },
  methods: {
    paginate(pager: { page: number; pagesize: number }) {
      this.page = pager.page;
      this.pageSize = pager.pagesize;
      this.getData();
    },
    getData() {
      this.items = new Array<number>();

      // simulate fetching data
      window.setTimeout(() => {
        const recordCnt = 50;
        const skipTo = this.page * this.pageSize - this.pageSize;
        for (let i = skipTo; i < recordCnt; i += 1) {
          if (this.items.length < this.pageSize) this.items.push(i);
        }

        this.total = Math.ceil(recordCnt / this.pageSize);
      }, 500);
    },
  },
});
</script>

<style lang="scss"></style>
