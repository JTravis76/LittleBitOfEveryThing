export default {
  name: "app-pinterest-grid",
  template: `
<div class="masonry-container">
  <template v-for="item in cartoons" :key="item">
    <div class="masonry-item">
      <a href="#">
        <img :src="path + item" />
      </a>
        <span>{{ item }}</span>
    </div>  
  </template>
</div>`,
  data() {
    return {
      path: "img/cartoons/",
      cartoons: [
        "JoyOfTech-vicious-cycle.gif",
        "CommitStrip-Delete-People.jpg",
        "CommitStrip-Developement-progress.jpg",
        "DibertTed-struggling.gif",
        "Flying-to-Mars.jpg",
        "DibertTed-More-Information.gif",
        "MonkeyUser-staying_focus.png",
        "DibertTed-New-Feature.gif",
        "BizarroComics-standing_desk.png",
        "MonkeyUser-Project_Managment.png",
        "Roselbob-SDLC-Patterns.jpg",
        "MonkeyUser-The_Struggle.png",
      ],
    };
  },
};
