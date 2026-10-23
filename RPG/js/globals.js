export default {
  root: "./img/",
  gridCnt: 12, // number of table rows and columns
  iconSize: 32, // size of each grid cell
  scene: 0, // current scene loaded from map array
  setScene(s = 0) {
    this.scene = s;
  },
  text: {
    axe: "axe",
    torch: "torch",
    flute: "flute",
    bottle: "bottle",
    waterbottle: "water_bottle",
    bronzesword: "bronze_sword",
    bronzeshield: "bronze_shield",
    halfherb: "half_herb",
    opening: "Welcome to Adventure of Jeremiah.",
    t1: "You play the flute and a fountain appear.",
    t2: "You do not have enough gold to purchase this item.",
    t3: "Thank you for your purchase.",
    t4: "You fill the bottle with water from the fountain.",
    t5: "You use the torch to light the cave.",
    choice1: "Click Ok to use the 'FULL Herb'. Cancel to use 'HALF Herb'.",
  },
};
