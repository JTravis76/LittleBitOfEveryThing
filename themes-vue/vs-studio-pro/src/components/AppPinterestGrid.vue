<template>
  <div class="masonry-container">
    <template v-for="item in cartoons" :key="item">
      <div class="masonry-item">
        <a href="#">
          <img :src="path + item" />
        </a>
        <span>{{ item }}</span>
      </div>
    </template>
  </div>
</template>

<script lang="ts">
import { defineComponent } from "vue";

export default defineComponent({
  name: "AppPinterestGrid",
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
});
</script>

<style lang="scss" scoped>
/*== Pinterest Grid ==*/
.masonry-container {
  -webkit-column-count: 3;
  -moz-column-count: 3;
  column-count: 3;

  -webkit-column-gap: 15px;
  -moz-column-gap: 15px;
  column-gap: 15px;
  margin: 1px;

  .masonry-item {
    display: inline-block;
    width: 100%;
    margin-bottom: 15px;
    border: 1px solid #ccc;
    border-radius: 5px;

    img {
      display: block;
      width: 100%;
    }

    /* Just for decoration: */
    span {
      font-family: sans-serif;
      display: block;
      padding: 10px;
    }
  }
}
</style>
