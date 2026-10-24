import { createApp, createPinia, setMapStoreSuffix } from "./deps.js";
import App from "./app.js";
import router from "./router/index.js";
import { dlgPlugin } from "./plugins/index.js";
//import outsideClick from "./directives/outside-click.js";

setMapStoreSuffix("site");

const app = createApp(App);
app.use(router);
app.use(createPinia());
app.use(dlgPlugin);
//app.directive("outside-click", outsideClick);
app.mount("#app");
