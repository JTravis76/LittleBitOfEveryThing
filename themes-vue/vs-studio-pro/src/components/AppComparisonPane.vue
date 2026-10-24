<template>
  <div class="container comparison-panel">
    <button
      type="button"
      class="gutter"
      title="resize panel"
      @mousedown="resizer($event)"
    >
      ...
    </button>
    <div class="panel-header">
      <div class="panel-title">Circuit Comparison</div>
      <div>
        <button
          type="button"
          class="btn"
          :class="{ active: tableView == true }"
          title="table view"
          @click="tableView = true"
        >
          <Icon icon="fileTable"></Icon>
        </button>
        <button
          type="button"
          class="btn"
          :class="{ active: tableView == false }"
          title="JSON view"
          @click="tableView = false"
        >
          <Icon icon="fileJson"></Icon>
        </button>
      </div>
    </div>
    <div class="panel-body">
      <div class="row">
        <div class="col-lg-12">
          <div id="tbl-wrap" class="table-responsive">
            <table v-if="tableView">
              <thead>
                <tr>
                  <th>Type</th>
                  <th>Field</th>
                  <th>Old Value</th>
                  <th>New Value</th>
                </tr>
              </thead>
              <tbody v-if="loading">
                <td><div class="skeleton">&nbsp;</div></td>
                <td><div class="skeleton">&nbsp;</div></td>
                <td><div class="skeleton">&nbsp;</div></td>
                <td><div class="skeleton">&nbsp;</div></td>
              </tbody>
              <tbody v-else>
                <tr v-for="item in compareTable" :key="item.field">
                  <td>
                    <Icon
                      v-if="item.type === 'add'"
                      icon="addCircle"
                      class="icon-success"
                    ></Icon>
                    <Icon
                      v-if="item.type === 'modify'"
                      icon="flashTriangle"
                      class="icon-warning"
                    ></Icon>
                    <Icon
                      v-if="item.type === 'delete'"
                      icon="minusCircle"
                      class="icon-danger"
                    ></Icon>
                    {{ item.type }}
                  </td>
                  <td>{{ item.field }}</td>
                  <td>{{ item.oldValue }}</td>
                  <td>{{ item.newValue }}</td>
                </tr>
              </tbody>
            </table>
            <div v-else class="json-compare">
              <div class="compare-window--before">
                Before
                <div v-if="loading">
                  <div class="skeleton skeleton-text" style="width: 60%"></div>
                  <div class="skeleton skeleton-text" style="width: 80%"></div>
                </div>
                <CompareWindow
                  v-else
                  :results="compareResults[0]"
                ></CompareWindow>
              </div>
              <div class="compare-window--after">
                After
                <div v-if="loading">
                  <div class="skeleton skeleton-text" style="width: 60%"></div>
                  <div class="skeleton skeleton-text" style="width: 80%"></div>
                </div>
                <CompareWindow
                  v-else
                  :results="compareResults[1]"
                ></CompareWindow>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script lang="ts">
import { defineComponent } from "vue";
import Icon from "./VIcon.vue";
import CompareWindow from "./AppCompareWindow.vue";
import Differ, { DiffResult } from "../lib/json-diff-kit@1.0.19";

interface ICompareTable {
  type: string;
  field: string;
  oldValue: string;
  newValue: string;
}

interface IDiffer {
  0: DiffResult[];
  1: DiffResult[];
}

export default defineComponent({
  name: "AppComparisonPane",
  components: {
    Icon,
    CompareWindow,
  },
  data() {
    return {
      loading: true,
      tableView: true,
      compareTable: new Array<ICompareTable>(),
      compareResults: {
        0: new Array<DiffResult>(),
        1: new Array<DiffResult>(),
      } as IDiffer,
    };
  },
  created() {
    this.loading = true;
    window.setTimeout(() => {
      const d = {
        before: { id: 1, name: "First Last" },
        after: { id: 1, name: "Last, First" },
      };

      this.compareResults = new Differ.Differ().diff(d.before, d.after);
      this.buildView();
      this.loading = false;
    }, 1100);
  },
  beforeUnmount() {
    (document.querySelector(".pane-top") as HTMLElement).style.height = "";
    (document.querySelector(".pane-bottom") as HTMLElement).style.height = "";
  },
  methods: {
    resizer(e: MouseEvent) {
      const topPane = document.querySelector(".pane-top") as HTMLDivElement;
      const bottomPane = document.querySelector(
        ".pane-bottom"
      ) as HTMLDivElement;
      const tblWrap = document.querySelector("#tbl-wrap") as HTMLDivElement;

      let prevY = e.y;
      const topPanel = topPane.getBoundingClientRect();
      const bottomPanel = bottomPane.getBoundingClientRect();
      const tbl = tblWrap.getBoundingClientRect();

      function mousemove(e: MouseEvent) {
        let newY = prevY - e.y;
        topPane.style.height = topPanel.height - newY + "px";
        bottomPane.style.height = bottomPanel.height + newY + "px";
        tblWrap.style.maxHeight = tbl.height + newY + "px";
      }
      function mouseup() {
        window.removeEventListener("mousemove", mousemove);
        window.removeEventListener("mouseup", mouseup);
      }
      window.addEventListener("mousemove", mousemove);
      window.addEventListener("mouseup", mouseup);
    },
    buildView() {
      let className = "Circuit";
      for (let i = 0; i < this.compareResults[1].length; i += 1) {
        const before = this.compareResults[0][i];
        const after = this.compareResults[1][i];

        if (after.level >= 0) className = "Circuit";
        if (after.level >= 2) className = "CircuitGroup";
        if (after.level >= 4) className = "EquipmentInstance";
        if (after.level >= 6) className = "EquipmentConfiguration";
        if (after.level >= 7) className = "EquipmentDefinition";

        if (after.type !== "equal") {
          // eslint-disable-next-line
          const afterValue = after.text.split(": ");
          // eslint-disable-next-line
          const beforeValue = before.text.split(": ");

          this.compareTable.push({
            type: after.type,
            field: `${className}.${afterValue[0]}`,
            oldValue: beforeValue[1],
            newValue: afterValue[1],
          });
        }
      }
    },
  },
});
</script>

<style lang="scss" scoped>
div.comparison-panel {
  border-top: 1px solid var(--color--gray2);

  .gutter {
    border: none;
    background-color: var(--color--gray3);
    color: var(--color--white);
    height: 10px;
    border-radius: 2px;
    line-height: 0;
    padding-bottom: 10px;
    width: 100%;
    position: absolute;
    top: -8px;
    left: 0;
    cursor: n-resize;
  }

  .panel-header {
    margin: 2px 10px;
    font-weight: 600;
    display: flex;
    justify-content: space-between;
    border-bottom: 1px solid var(--color--gray2);

    .btn {
      &.active {
        background-color: var(--color--gray2);
      }
    }
  }

  .panel-body {
    background-color: transparent;

    .table-responsive {
      max-height: 267px;

      .json-compare {
        display: flex;

        .compare-window {
          &--before,
          &--after {
            width: 50%;
          }
        }
      }

      .icon-success {
        color: var(--color--success1);
      }

      .icon-warning {
        color: var(--color--warning1);
      }

      .icon-danger {
        color: var(--color--error2);
      }
    }
  }
}
</style>
