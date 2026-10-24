// this demo was provided here - Minimal.vue
// https://github.com/cameronhimself/vue-drag-drop-demo/tree/master/src
import Drag from "./v-drag.js";
import Drop from "./v-drop.js";

export default {
  name: "app-drag-drop2",
  template: `
<div>
  <drag :transfer-data="{ example: 'minimal' }">drag</drag>
  <drop @drop="handleDrop">drop</drop>
</div>
  `,
  components: { Drag, Drop },
  methods: {
    handleDrop(data) {
      alert(`You dropped with data: ${JSON.stringify(data)}`);
    },
  },
};
