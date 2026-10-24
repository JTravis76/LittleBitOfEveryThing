import { IHttpClient } from "../lib/http-client";

export interface ITag {
  id: number;
  name: string;
  active: boolean;
}

export default class Tag {
  private _http: IHttpClient;

  constructor(httpClient: IHttpClient) {
    this._http = httpClient;
  }

  async get() {
    return await this._http.get<ITag[]>("tags");
  }

  async setAutoTags() {
    return await this._http.get("autotags");
  }
}