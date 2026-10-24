import { IHttpClient } from "../lib/http-client";

export interface ISong {
  title: string;
  signature: string;
  bpm: number;
  progression: string;
}

export default class Metronome {
  private _http: IHttpClient;

  constructor(httpClient: IHttpClient) {
    this._http = httpClient;
  }

  async getSongList(): Promise<ISong[]> {
    //return await this._http.get<ISong[]>("signlists");
    return new Promise((resolve) => {
      let list = new Array<ISong>();
      list = [
        {
          title: "Worried Man Blues",
          signature: "4/4",
          bpm: 100,
          progression: "A(8)D(8)A(8)G(4)A(4)",
        },
        {
          title: "Amazing Grace",
          signature: "3/4",
          bpm: 70,
          progression: "G(6)C(3)G(9)D(6)G(6)C(6)Em(3)D(3)G(6)",
        },
        {
          title: "Crawdad Song",
          signature: "4/4",
          bpm: 100,
          progression: "G(12)D(4)G(4)C(4)G(2)D(2)G(4)",
        },
        {
          title: "I'll Fly Away",
          signature: "4/4",
          bpm: 104,
          progression: "G(8)C(4)G(15)D(1)G(4)",
        },
      ];
      resolve(list);
    });
  }

}