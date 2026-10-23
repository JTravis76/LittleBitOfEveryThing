import _g from "./globals.js";
import tw from "./typewriter.js";
import gb from "./gameboard.js";
import { getCellType, getCellImage, updateScene } from "./map.js";

export class Player {
  constructor() {
    this.top = 0;
    this.left = 0;

    this.direction = {
      north: `${_g.root}guy_back.gif`,
      east: `${_g.root}guy_right.gif`,
      south: `${_g.root}guy_front.gif`,
      west: `${_g.root}guy_left.gif`,
    };

    this.facing = this.direction.east;

    this.lastPositon = {
      top: 0,
      left: 0,
      facing: this.direction.east,
      scene: 0,
    };

    this.hp = 100; // hit points
    this.ep = 0; // experience points
    this.level = 0; // experience level
    this.gp = 10; // gold points
    this.ehp = 0; // enemy hit points

    this.sword = false;
    this.shield = false;
    this.axe = false;
    this.torch = false;
    this.flute = false;
    this.half_herb = false;
    this.full_herb = false;
    this.bottle = false;
    this.water = false;

    this.p1 = document.createElement("div");
    this.p1.id = "player1";
    this.p1.setAttribute("name", "player1");

    this.p1icon = document.createElement("img");
    this.p1icon.alt = "p1";
    this.p1icon.setAttribute("name", "player1icon");
    this.p1icon.src = this.facing;

    this.p1.append(this.p1icon);
  }

  setupPlayer() {
    this.setPosition(5, 2);
    document.querySelector("main").append(this.p1);
    gb.setScoring("hp", this.hp);
    gb.setScoring("ep", this.ep);
    gb.setScoring("level", this.level);
    gb.setScoring("gp", this.gp);
    gb.setScoring("ehp", this.ehp);
    tw.write(_g.text.opening);

    document.onkeyup = (e) => {
      switch (e.key) {
        case "8":
          this.moveTop(-1);
          break;
        case "2":
          this.moveTop(1);
          break;
        case "4":
          this.moveLeft(-1);
          break;
        case "6":
          this.moveLeft(1);
          break;
        case "5":
          this.takeAction();
          break;
        case "0":
          this.useHerb();
          break;
        case ".":
          // == set game cheats
          this.gp = 999;
          gb.setScoring("gp", this.gp);
          // this.axe = true;
          // gb.setInventory(3, _g.text.axe);
          this.torch = true;
          gb.setInventory(4, _g.text.torch);
          this.flute = true;
          gb.setInventory(5, _g.text.flute);
          this.bottle = true;
          gb.setInventory(10, _g.text.bottle);
          // this.water = true;
          // gb.setInventory(10, _g.text.waterbottle);
          break;
      }
    };
  }

  setPosition(top = 0, left = 0) {
    this.top = top;
    this.left = left;
    this.p1.style.top = `${this.top * _g.iconSize}px`;
    this.p1.style.left = `${this.left * _g.iconSize}px`;
    this.p1icon.src = this.facing;
  }

  /** move the player either left or right across the scene */
  moveLeft(step = 0) {
    step = this.left + step;
    tw.clear();
    // don't move beyond the gameboard
    if (step > -1 && step < _g.gridCnt) {
      let cell = "";
      if (step > this.left) {
        cell = getCellType(this.top, this.left + 1);
        this.facing = this.direction.east;
      } else {
        cell = getCellType(this.top, this.left - 1);
        this.facing = this.direction.west;
      }
      this.doMove(cell, this.top, step);
    }
  }

  /** move the player either up or down across the scene */
  moveTop(step = 0) {
    step = this.top + step;
    tw.clear();
    // don't move beyond the gameboard
    if (step > -1 && step < _g.gridCnt) {
      let cell = "";
      if (step > this.top) {
        cell = getCellType(this.top + 1, this.left);
        this.facing = this.direction.south;
      } else {
        cell = getCellType(this.top - 1, this.left);
        this.facing = this.direction.north;
      }
      this.doMove(cell, step, this.left);
    }
  }

  /** process the cell data and perform the move */
  doMove(cell = "", top = 0, left = 0) {
    //console.log(cell);
    const parts = cell.split("-");
    if (cell === "0" || cell.startsWith("4-")) {
      this.setPosition(top, left);
    } else if (cell.startsWith("5-")) {
      // fight scene
      this.goFight(parts, top, left);
    } else if (cell.startsWith("2-")) {
      // jump scene
      gb.loadScene(parts[1]);
      this.setPosition(parseInt(parts[3], 10), parseInt(parts[2], 10));
    } else {
      this.p1icon.src = this.facing;
    }
  }

  getFacingCoordinates() {
    let top = 0;
    let left = 0;
    if (this.facing === this.direction.north) {
      top = this.top - 1;
      left = this.left;
    }
    if (this.facing === this.direction.east) {
      top = this.top;
      left = this.left + 1;
    }
    if (this.facing === this.direction.south) {
      top = this.top + 1;
      left = this.left;
    }
    if (this.facing === this.direction.west) {
      top = this.top;
      left = this.left - 1;
    }
    return { top, left };
  }

  takeAction() {
    let img = "";
    let cell = "";
    let coordinates = {
      top: 0,
      left: 0,
    };

    coordinates = this.getFacingCoordinates();
    img = getCellImage(coordinates.top, coordinates.left);
    cell = getCellType(coordinates.top, coordinates.left);
    const parts = cell.split("-");
    //console.log(img, cell, parts);

    // == (3-n-n-n) is a solid object with an action
    if (parts[0] === "3") {
      switch (parts[1]) {
        case "0": // display message
          tw.write(parts[2]);
          break;
        case "1": // 3-1-n-n (use axe to cut tree)
          if (img === "tree" && this.axe) {
            gb.setIcon(coordinates.top, coordinates.left, parts[2]);
            if (parts[2] === "treasure_close2") {
              parts[1] = "2";
              parts[2] = "treasure_open2";
              const d = parts.join("-");
              updateScene(coordinates.top, coordinates.left, parts[2], d);
            } else {
              updateScene(coordinates.top, coordinates.left, parts[2], "0");
            }
          }
          break;
        case "2": // 3-2-n-n (open treasure chest)
          getElementById.setIcon(coordinates.top, coordinates.left, parts[2]);
          gb.setScoring("gp", parseInt(parts[4], 10) + this.gp);
          tw.write(parts[3]);
          break;
        case "3": // 3-3-n-n (play flute)
          if (img === "water_up" && this.flute) {
            gb.setIcon(coordinates.top - 1, coordinates.left, parts[2]);
            updateScene(coordinates.top, coordinates.left, parts[2], "3-7-0-0");
            tw.write(_g.text.t1);
          }
          break;
        case "4": // 3-4-n-n (purchase item from store)
          if (
            confirm(
              `Do you want to buy the '${parts[2]
                .toUpperCase()
                .replace("_", " ")}' for ${parts[3]} gold coins?`
            )
          ) {
            if (this.gp < parseInt(parts[3], 10)) {
              tw.write(_g.text.t2);
            } else {
              switch (parts[2]) {
                case "axe":
                  this.axe = true;
                  gb.setInventory(3, _g.text.axe);
                  break;
                case "bronze_sword":
                  this.sword = true;
                  gb.setInventory(1, _g.text.bronzesword);
                  break;
                case "bronze_shield":
                  this.shield = true;
                  gb.setInventory(2, _g.text.bronzeshield);
                  break;
                case "half_herb":
                  this.half_herb = true;
                  gb.setInventory(8, _g.text.halfherb);
                  break;
                case "rest":
                  this.hp = 100;
                  gb.setScoring("hp", this.hp);
                  break;
              }
              this.gp = this.gp - parseInt(parts[3], 10);
              gb.setScoring("gp", this.gp);
              tw.write(_g.text.t3);
            }
          }
          break;
        case "5":
          if (this.ehp === 0) return;
          const earned = { gp: 0, ep: 0 };
          let ap = 0; // attack points
          let defeated = false;
          let max = this.level;
          if (this.level < 3) max = 3;

          ap = Math.floor(Math.random() * max);
          if (this.sword) ap = +10;

          if (this.ehp > 0) {
            tw.write(`You attacked the enemy with ${ap} attack points.`);
            this.ehp = this.ehp - ap;
            if (this.ehp < 0) this.ehp = 0;
            gb.setScoring("ehp", this.ehp);
          }
          if (this.ehp === 0) {
            defeated = true;
            tw.write("You defeated the enemy.");
            gb.setIcon(6, 7, "block2");
            //compute gold & exp points based on exp level
            earned.gp = Math.floor(Math.random() * this.level + 2);
            earned.ep = Math.floor(Math.random() * this.level + 1);
            tw.write(
              `You earned ${earned.gp} gold points and ${earned.ep} experience points.`
            );
            this.gp += earned.gp;
            this.ep += earned.ep;
            gb.setScoring("gp", this.gp);
            gb.setScoring("ep", this.ep);
            this.checkExperience();
            //this.goFight(["5", "1"]); //<- return to last scene
          }
          //== Enemy turns
          if (!defeated) {
            ap = Math.floor(Math.random() * max);
            if (this.shield) ap -= 5;
            if (ap <= 0) ap = 1;

            tw.write(`Enemy attack you with ${ap} attack point.`);
            this.hp -= ap;
            gb.setScoring("hp", this.hp);
            if (this.hp <= 0) {
              tw.write("GAME OVER!!");
              gb.setIcon(6, 5, "block2");
              if (
                confirm("The enemy defeated you! Would you like to play again?")
              ) {
                window.location.reload(true);
              }
            }
          }
          break;
        case "7": // 3-7-n-n (fill water bottle)
          if (img === "water_foundtain" && this.bottle) {
            this.water = true;
            gb.setInventory(10, _g.text.waterbottle);
            tw.write(_g.text.t4);
          }
          break;
      }
    }
    // == (4-n-n-n) is a non-solid object with an action
    if (parts[0] === "4") {
      switch (parts[1]) {
        case "0":
          if (this.torch) {
            tw.write(_g.text.t5);
            gb.loadScene(parseInt(parts[2], 10));
          }
          break;
      }
    }
  }

  goFight(parts = [], top = 0, left = 0) {
    if (parts[1] === "0") {
      this.lastPositon = {
        top: top,
        left: left,
        facing: this.facing,
        scene: _g.scene,
      };
      // compute enemy hit points
      this.ehp = Math.floor(Math.random() * this.level + 3);
      if (this.ehp > 0) this.ehp = 3;
      gb.setScoring("ehp", this.ehp);
      // jump to fight scene
      this.facing = this.direction.east;
      this.setPosition(6, 5);
      gb.loadScene(parseInt(parts[2], 10));
    }
    if (parts[1] === "1") {
      // return to last position
      const { facing, top, left, scene } = this.lastPositon;
      this.ehp = 0;
      this.facing = facing;
      this.setPosition(top, left);
      gb.setScoring("ehp", this.ehp);
      gb.loadScene(scene);
    }
  }

  useHerb() {
    if (this.half_herb && this.full_herb) {
      // allow player to select
      if (confirm(_g.text.choice1)) this.useFullHerb();
      else this.useHalfHerb();
    } else if (this.half_herb) this.useHalfHerb();
    else if (this.full_herb) this.useFullHerb();

    gb.setScoring("hp", this.hp);
  }

  useFullHerb() {
    this.hp = 100;
    this.full_herb = false;
    gb.setInventory(9, "no");
  }

  useHalfHerb() {
    this.hp = this.hp + 50;
    if (this.hp > 100) this.hp = 100;
    this.half_herb = false;
    gb.setInventory(8, "no");
  }

  checkExperience() {
    if (this.level <= 3 && this.ep >= 30) this.setExperience();
    else if (this.level > 3 && this.level <= 6 && this.ep >= 60)
      this.setExperience();
    else if (this.level > 6 && this.level <= 9 && this.ep >= 90)
      this.setExperience();
    else if (this.level >= 10 && this.ep >= 100) this.setExperience();
  }

  setExperience() {
    this.level += 1;
    this.ep = 0;
    gb.setScoring("level", this.level);
    gb.setScoring("ep", this.ep);
    tw.write("You advance to the next level.");
  }
}

export default new Player();
