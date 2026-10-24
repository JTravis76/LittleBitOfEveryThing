// Credit goes here: https://medium.com/nerd-for-tech/how-to-create-a-fish-tank-with-html-css-c98aca39ae85
export default {
  name: "app-fish-tank",
  template: `
<div id="fish-tank">
  <div id="bubble-field"></div>
  <div class="plant">
    <div class="leaf-1"></div>
  </div>
  <div class="fish">
    <div class="top-fin"></div>
    <div class="fish-body"></div>
    <div class="tail-fin"></div>
    <div class="side-fin"></div>
    <div class="scale scale-1"></div>
    <div class="scale scale-2"></div>
    <div class="scale scale-3"></div>
  </div>  
  <div class="fish blue-fish">
    <div class="top-fin"></div>
    <div class="fish-body"></div>
    <div class="tail-fin"></div>
    <div class="side-fin"></div>
    <div class="scale scale-1"></div>
    <div class="scale scale-2"></div>
    <div class="scale scale-3"></div>
  </div>
  <div class="fish white-fish">
    <div class="top-fin"></div>
    <div class="fish-body"></div>
    <div class="tail-fin"></div>
    <div class="side-fin"></div>
    <div class="scale scale-1"></div>
    <div class="scale scale-2"></div>
    <div class="scale scale-3"></div>
  </div>
    <div class="fish red-fish">
    <div class="top-fin"></div>
    <div class="fish-body"></div>
    <div class="tail-fin"></div>
    <div class="side-fin"></div>
    <div class="scale scale-1"></div>
    <div class="scale scale-2"></div>
    <div class="scale scale-3"></div>
  </div>
</div>
`,
  data() {
    return {
      bubbles: 30,
    };
  },
  // created() {
  //   this.$nextTick(() => {
  //     const bubbleField = document.getElementById("bubble-field");
  //     //generate bubbles with randomly timed animation durations
  //     for (let i = 0; i < this.bubbles; i++) {
  //       const randNum = Math.floor(Math.random() * 20) + 1;
  //       const animDur = 2 + 0.5 * randNum;
  //       let moveEl = document.createElement("div");
  //       moveEl.setAttribute("class", "bubble-rise");
  //       moveEl.setAttribute(
  //         "style",
  //         "animation-duration: " + animDur.toString() + "s;"
  //       );

  //       let bubbleEl = document.createElement("div");
  //       bubbleEl.setAttribute("class", "bubble");
  //       let bubbleElContent = document.createTextNode("");
  //       bubbleEl.appendChild(bubbleElContent);

  //       moveEl.appendChild(bubbleEl);
  //       bubbleField.appendChild(moveEl);
  //     }
  //   });
  // },
};
