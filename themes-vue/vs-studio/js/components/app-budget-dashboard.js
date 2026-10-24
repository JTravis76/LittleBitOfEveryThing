import BudgetFilter from "./app-budget-filter.js";
import FileUploader from "./v-file-uploader.js";

export default {
  template: `
<div>
  <div>
    <BudgetFilter @filters="getTransactions($event)"></BudgetFilter>
  </div>
  <h5>Transactions</h5>
  <div>
    <table>
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
                  class="btn text-ellipsis"
                  @click="editMemo(trans.id)"
                >
                  {{ trans.memo }}
                </button>
              </td>
              <td>{{ $filters.currency(trans.amount, false) }}</td>
              <td>
                <select v-if="trans.id === tagId" @change="onTagChange($event)">
                  <option value="item1" selected></option>
                  <template v-for="tag in tags" :key="tag">
                    <option :value="tag">{{ tag }}</option>
                  </template>
                </select>
                <button
                  v-else
                  type="button"
                  class="btn text-ellipsis"
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
            <td colspan="6">
              No records found...
            </td>
          </tr>
        </tbody>
    </table>
  </div>
  <div>
    <FileUploader @files="uploadFiles($event)"> </FileUploader>
  </div>
</div>`,
  name: "app-budget-dashboard",
  components: {
    FileUploader,
    BudgetFilter,
  },
  data() {
    return {
      transactions: [],
      tags: [],
      memoId: 0,
      tagId: 0,
    };
  },
  created() {
    // this.$http.tag.get().then((d) => {
    //   this.tags = d.map((x) => x.name);
    // });
    this.getTransactions();
  },
  methods: {
    getTransactions(filters) {
      // this.$http.transaction.get(1, 25, filters).then((d) => {
      //   this.transactions = d;
      // });
    },
    editMemo(id) {
      this.tagId = 0;
      this.memoId = id ? id : 0;
      // todo: find input and set focus
    },
    editTag(id) {
      this.memoId = 0;
      this.tagId = id ? id : 0;
    },
    onTagChange(e) {
      const el = e.target;
      const trans = this.transactions.find((x) => x.id === this.tagId);
      if (trans && el) {
        trans.tag = el.value;
        // this.$http.transaction.put(trans.id, trans.memo, trans.tag);
      }
      this.tagId = 0;
    },
    setMemo(e) {
      const el = e.target;
      const trans = this.transactions.find((x) => x.id === this.memoId);
      if (trans && el) {
        trans.memo = el.value;
      }
    },
    saveMemo(id) {
      const trans = this.transactions.find((x) => x.id === id);
      if (trans) {
        //this.$http.transaction.put(trans.id, trans.memo, trans.tag);
      }
      this.memoId = 0;
    },
    uploadFiles(fileList) {
      // this.$http.transaction.post(fileList).then((d) => {
      //   console.log(d);
      //   this.getTransactions();
      // });
    },
  },
};
