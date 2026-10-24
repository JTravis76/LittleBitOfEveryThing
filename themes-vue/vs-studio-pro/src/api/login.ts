import { IHttpClient } from "../lib/http-client";

export default class Login {
  private _http: IHttpClient;

  constructor(httpClient: IHttpClient) {
    this._http = httpClient;
  }

  async budgetLogin(username: string, password: string) {
    return await this._http.post("login", { username, password });
  }
}