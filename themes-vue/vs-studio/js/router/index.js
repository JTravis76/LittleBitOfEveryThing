import {
  createRouter,
  createWebHashHistory,
} from "../vue-router.esm-browser.js";
import routes from "./routes.js";

const router = createRouter({
  history: createWebHashHistory(),
  routes,
});

export default router;
