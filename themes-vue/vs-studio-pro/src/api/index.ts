import HttpClient, {
  IHttpClient,
  IHttpRequestConfig,
} from "../lib/http-client";
import Transaction from "./transaction";
import Tag from "./tag";
import Login from "./login";
import Metronome from "./metronome";

const success = (response: Response) => {
  return response;
};

const failure = (response: Response) => {
  if (!response) {
    throw response;
  }

  switch (response.status) {
    case 400: //Bad Request
      break;
    case 401: //Unauthorized
      break;
    case 404: //Not Found
      break;
    case 500: //Internal Server Error
      console.error(response);
      break;
    default:
  }
};

class ApiManager {
  public httpClient: IHttpClient;
  public transaction: Transaction;
  public tag: Tag;
  public login: Login;
  public metronome: Metronome;

  constructor() {
    this.httpClient = HttpClient;
    this.httpClient.defaults.baseUrl = `${import.meta.env.VITE_API_URL}`;
    //this.httpClient.defaults.headers.common.authorization = { bearer: "" };

    this.httpClient.interceptors.request.use(
      async (config: IHttpRequestConfig) => {

        config.headers = {
          Accept: "application/json, */*",
          "Content-Type": "application/json",
          //Authorization: `bearer ${token}`,
        };

        const authToken = window.localStorage.getItem("Authorization");
        if (authToken) config.headers["Authorization"] = authToken;

        config.mode = "cors";
        config.credentials = "same-origin";
        return config;
      },
    );
    this.httpClient.interceptors.response.use(success, failure);

    this.transaction = new Transaction(this.httpClient);
    this.tag = new Tag(this.httpClient);
    this.login = new Login(this.httpClient);
    this.metronome = new Metronome(this.httpClient);
  }
}

export default new ApiManager();
