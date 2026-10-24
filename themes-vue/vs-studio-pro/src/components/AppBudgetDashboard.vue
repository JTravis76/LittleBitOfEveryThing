<template>
  <div>
    <template v-if="!authenticated">
      <div class="skeleton-ui container">
        <form>
          <h6>Login</h6>
          <div class="row">
            <div class="twelve columns">
              <label>Username</label>
              <input v-model="login.user" type="text" class="u-full-width" />
            </div>
          </div>
          <div class="row">
            <div class="twelve columns">
              <label>Password</label>
              <input v-model="login.pwd" type="password" class="u-full-width" />
            </div>
          </div>
          <div class="row">
            <div class="twelve columns">
              <button
                type="button"
                class="button button-primary"
                @click="submitLogin()"
              >
                Submit
              </button>
            </div>
          </div>
        </form>
      </div>
    </template>
    <template v-else>
      <div>
        <BudgetFilter @filters="getTransactions($event)"></BudgetFilter>
      </div>
      <h5>Transactions</h5>
      <div class="row">
        <div class="twelve columns">
          <table class="stripes">
            <thead>
              <tr>
                <th>Date</th>
                <th>Type</th>
                <th>Name</th>
                <th>Memo</th>
                <th>Amount</th>
                <th>Tag</th>
              </tr>
            </thead>
            <tbody v-if="transactions.length > 0">
              <template v-for="trans in transactions" :key="trans.id">
                <tr>
                  <td>
                    {{ $filters.toLocaleDateString(trans.date) }}
                  </td>
                  <td>
                    {{ trans.transaction }}
                  </td>
                  <td>
                    <div class="text-ellipsis" :title="trans.name">
                      {{ trans.name }}
                    </div>
                  </td>
                  <td>
                    <input
                      v-if="trans.id === memoId"
                      type="text"
                      :value="trans.memo"
                      @input="setMemo"
                      @keyup.enter="memoId = 0"
                      @blur="saveMemo(trans.id)"
                    />
                    <button
                      v-else
                      type="button"
                      class="text-ellipsis"
                      @click="editMemo(trans.id)"
                    >
                      {{ trans.memo }}
                    </button>
                  </td>
                  <td>{{ $filters.currency(trans.amount, false) }}</td>
                  <td>
                    <select
                      ref="select"
                      v-if="trans.id === tagId"
                      @change="onTagChange($event)"
                    >
                      <option value="item1" selected></option>
                      <template v-for="tag in tags" :key="tag">
                        <option :value="tag">{{ tag }}</option>
                      </template>
                    </select>
                    <button
                      v-else
                      type="button"
                      class="text-ellipsis"
                      @click="editTag(trans.id)"
                    >
                      {{ trans.tag }}
                    </button>
                  </td>
                </tr>
              </template>
            </tbody>
            <tbody v-else>
              <tr>
                <td colspan="6">No records found...</td>
              </tr>
            </tbody>
          </table>
        </div>
      </div>

      <button type="button" @click="autoTags()">Set Auto Tags</button>
      <div>
        <FileUploader @files="uploadFiles($event)"> </FileUploader>
      </div>
    </template>
  </div>
</template>

<script lang="ts">
import { defineComponent } from "vue";
import BudgetFilter, { IBudgetFilter } from "./AppBudgetFilter.vue";
import FileUploader from "./VFileUploader.vue";
import { ITransaction } from "../api/transaction";

export default defineComponent({
  name: "AppBudgetDashboard",
  components: {
    BudgetFilter,
    FileUploader,
  },
  data() {
    return {
      authenticated: false,
      login: {
        user: "",
        pwd: "",
      },
      transactions: new Array<ITransaction>(),
      tags: new Array<string>(),
      memoId: 0,
      tagId: 0,
    };
  },
  beforeCreate() {
    localStorage.removeItem("Authorization");
  },
  methods: {
    submitLogin() {
      const { user, pwd } = this.login;
      this.$http.login
        .budgetLogin(user, pwd)
        .then((res) => {
          localStorage.setItem("Authorization", res);
          this.authenticated = true;

          this.$http.tag.get().then((d) => {
            this.tags = d.map((x) => x.name);
          });
          this.getTransactions();
        })
        .catch((err) => console.error(err));
    },
    getTransactions(filters?: string | IBudgetFilter) {
      this.$http.transaction.get(1, 50, filters).then((d) => {
        this.transactions = d;
      });
    },
    editMemo(id?: number) {
      this.tagId = 0;
      this.memoId = id ? id : 0;
      // todo: find input and set focus
      window.setTimeout(() => {
        (this.$refs.select as HTMLSelectElement).focus();
      }, 500);
    },
    editTag(id?: number) {
      this.memoId = 0;
      this.tagId = id ? id : 0;
    },
    onTagChange(e: Event) {
      const el = e.target as HTMLSelectElement;
      const trans = this.transactions.find((x) => x.id === this.tagId);
      if (trans && el) {
        trans.tag = el.value;
        this.$http.transaction.put(trans.id, trans.memo, trans.tag);
      }
      this.tagId = 0;
    },
    setMemo(e: Event) {
      const el = e.target as HTMLInputElement;
      const trans = this.transactions.find((x) => x.id === this.memoId);
      if (trans && el) {
        trans.memo = el.value;
      }
    },
    saveMemo(id?: number) {
      const trans = this.transactions.find((x) => x.id === id);
      if (trans) {
        //this.$http.transaction.put(trans.id, trans.memo, trans.tag);
      }
      this.memoId = 0;
    },
    uploadFiles(fileList: FileList) {
      this.$http.transaction.post(fileList).then((d) => {
        console.log(d);
        this.getTransactions();
      });
    },
    autoTags() {
      this.$http.tag.setAutoTags();
    },
  },
});
</script>

<style lang="scss" scoped>
.login-wrapper {
  width: 100%;
  display: flex;
  justify-content: center;

  .login-panel {
    display: flex;
    flex-wrap: wrap;
    max-width: 170px;
  }
}

table {
  //width: 100%;
  // background-color: var(--color--white);
  // border-collapse: collapse;

  // thead {
  //   font-size: 14px;

  //   tr th {
  //     text-align: left;
  //   }
  // }

  // tbody {
  //   font-size: 0.8rem;

  //   tr td button {
  //     border: 1px dotted transparent;
  //     background: transparent;
  //     color: inherit;
  //     font-size: inherit;
  //     min-width: 75px;
  //     min-height: 20px;
  //     cursor: pointer;

  //     &:hover {
  //       border: 1px dotted black;
  //     }
  //   }
  // }

  &.stripes {
    tbody tr:nth-child(even) {
      background-color: var(--color--white);
    }

    tbody tr:nth-child(odd) {
      background-color: var(--color--gray1);
    }
  }
}
</style>
