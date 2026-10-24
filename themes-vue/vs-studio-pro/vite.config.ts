import { defineConfig } from "vite";
import vue from "@vitejs/plugin-vue";

// https://vitejs.dev/config/
export default defineConfig({
  server: {
    port: 8080,
    // https: {
    //   key: "./localhostkey.pem",
    //   cert: "./localhostcert.pem",
    // },
  },
  plugins: [vue(
    {
      template: {
        compilerOptions: {
          isCustomElement: (tag) => ["sodipodi:namedview", "dc:title", "cc:Work", "rdf:RDF"].includes(tag)
        }
      }
    }
  )],
  base: "/",
  css: {
    preprocessorOptions: {
      scss: {
        additionalData: `
          @import "./src/scss/_variables.scss";
          @import "./src/scss/_breakpoint-mixins.scss";
        `,
      },
    },
  },
});
