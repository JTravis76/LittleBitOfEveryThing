import _g from "./globals.js";
import { getCellImage } from "./map.js";

export class Gameboard {
  constructor() {
    this.elementId = "msg-bar";
  }

  setupGameBoard() {
    const div = document.createElement("div");
    div.classList.add("gameboard");

    for (let row = 0; row < _g.gridCnt; row++) {
      for (let col = 0; col < _g.gridCnt; col++) {
        const img = document.createElement("img");
        img.alt = "i";
        img.id = `r${row}c${col}`;
        img.src = `${_g.root}no.gif`;

        div.appendChild(img);
      }
    }

    document.querySelector("main").appendChild(div);
  }

  loadScene(scene = 0) {
    _g.setScene(scene);
    for (let row = 0; row <= _g.gridCnt - 1; row++) {
      for (let col = 0; col <= _g.gridCnt - 1; col++) {
        const imgName = getCellImage(row, col);
        const el = document.getElementById(`r${row}c${col}`);
        if (imgName !== "") el.src = `${_g.root}${imgName}.gif`;
        else el.src = `${_g.root}no.gif`;
      }
    }
  }

  loadSceneFrom(data = "") {
    const grid = data.split("\n");
    for (let row = 0; row <= _g.gridCnt - 1; row++) {
      for (let col = 0; col <= _g.gridCnt - 1; col++) {
        const img = grid[row].split("|")[2 * col];
        const el = document.getElementById(`r${row}c${col}`);
        if (img && img !== "") el.src = `${_g.root}${img}.gif`;
        else el.src = `${_g.root}no.gif`;
      }
    }
  }

  clearScene() {
    for (let row = 0; row <= _g.gridCnt - 1; row++) {
      for (let col = 0; col <= _g.gridCnt - 1; col++) {
        document.getElementById(`r${row}c${col}`).src = `${_g.root}no.gif`;
      }
    }
  }

  setIcon(top = 0, left = 0, icon = "") {
    document.getElementById(`r${top}c${left}`).src = `${_g.root}${icon}.gif`;
  }

  setInventory(slot = 0, item = "") {
    document.getElementById(
      `inventory${slot.toString()}`
    ).src = `${_g.root}${item}.gif`;
  }

  setScoring(id = "", value = 0) {
    document.getElementById(id).value = value.toString();
  }

  setMessage(msg = "") {
    document.getElementById(this.elementId).innerHTML = msg;
  }

  clearMessage() {
    document.getElementById(this.elementId).innerHTML = "";
  }
}

export default new Gameboard();
