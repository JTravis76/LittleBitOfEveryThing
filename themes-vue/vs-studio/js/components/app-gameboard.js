export default {
  name: "app-gameboard",
  template: `
<div style="width: 50%; margin: 25px">
  <div class="gameboard">
    <template v-for="cell in totalCells"> 
      <div class="cell"> </div> 
    </template>
  </div>
</div>`,
  data() {
    return {
      totalCells: 144,
    };
  },
};
