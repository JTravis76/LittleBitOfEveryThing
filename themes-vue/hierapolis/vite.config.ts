import { defineConfig } from "vite";
import vue from "@vitejs/plugin-vue";

// https://vitejs.dev/config/
export default defineConfig({
  server: {
    port: 8080,
    https: {
      key: "./localhostkey.pem",
      cert: "./localhostcert.pem",
    },
  },
  plugins: [vue()],
  base: "/",
});
