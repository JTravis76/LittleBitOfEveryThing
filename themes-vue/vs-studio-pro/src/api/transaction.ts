import { IHttpClient } from "../lib/http-client";
import { IBudgetFilter } from "../components/AppBudgetFilter.vue";

export interface ITransaction {
  id: number;
  date: string;
  transaction: string;
  name: string;
  memo: string;
  amount: number;
  tag: string;
}

export default class Transaction {
  private _http: IHttpClient;

  constructor(httpClient: IHttpClient) {
    this._http = httpClient;
  }

  async get(page: number = 1, pagesize: number = 10, searchFilter?: string | IBudgetFilter) {
    const params = {
      page,
      pagesize,
    } as { page: number, pagesize: number, search?: string, filters?: string };

    if (typeof searchFilter === "string") {
      params.search = searchFilter;
    } else if (typeof searchFilter === "object") {
      params.search = searchFilter.search;
      const arr = new Array<string>();
      if (searchFilter.assignedDates.start) arr.push(`d1=${searchFilter.assignedDates.start}`);
      if (searchFilter.assignedDates.end) arr.push(`d2=${searchFilter.assignedDates.end}`);
      if (searchFilter.amount.start) arr.push(`a1=${searchFilter.amount.start}`);
      if (searchFilter.amount.end) arr.push(`a2=${searchFilter.amount.end}`);
      if (searchFilter.tag) arr.push("t=true");
      params.filters = arr.join(",");
    }
    const data = await this._http.get<ITransaction[]>("transactions", {
      params,
    });

    return data;
  }

  async put(id: number, memo?: string, tag?: string) {
    const data = await this._http.put("transaction", {
      id,
      memo,
      tag,
    });

    return data;
  }

  async post(fileList: FileList) {
    let formData = new FormData();
    for (let i = 0; i < fileList.length; i += 1) {
      const file = fileList[i];
      formData.append(file.name, file);
    }

    return await this._http.post("transactions", formData);

    // const data = await this._http.post("transactions", null, {
    //   headers: {
    //     "content-type": "multipart/form-data"
    //   },
    //   body: formData
    // });

    //return data;

    // return fetch("https://localhost:7067/transactions", {
    //   method: "POST",
    //   headers: {
    //     "content-type": "multipart/form-data"
    //   },
    //   body: formData
    // }).then(async (resp) => {
    //   const txt = await resp.text();
    //   return txt;
    // });
  }
}