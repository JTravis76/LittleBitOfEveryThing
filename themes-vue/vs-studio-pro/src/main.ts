import { createApp } from 'vue'
import { createPinia } from "pinia";
import router from "./router";
import { filterPlugin, httpPlugin, dialogPlugin } from "./plugins";
import './scss/index.scss';
import App from './App.vue'

const app = createApp(App);
app.use(router);
app.use(createPinia());
app.use(filterPlugin);
app.use(httpPlugin);
app.use(dialogPlugin);
app.mount('#app');
