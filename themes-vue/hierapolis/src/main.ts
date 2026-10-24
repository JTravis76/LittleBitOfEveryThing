import { createApp } from "vue";
import App from "./App.vue";
import { createPinia, setMapStoreSuffix } from "pinia";
import router from "./router";
import { UserInRole } from "./plugins";
import "./scss/index.scss";

setMapStoreSuffix("");

const app = createApp(App);
app.use(createPinia());
app.use(router);

app.config.globalProperties.$UserInRole = UserInRole;
app.mount("#app");
