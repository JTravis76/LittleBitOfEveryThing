import Gameboard from "./gameboard.js";
import Player from "./player.js";

Gameboard.setupGameBoard();
Gameboard.setInventory(1, "sword");
Gameboard.setInventory(2, "shield");
Gameboard.setInventory(3, "no");
Gameboard.setInventory(4, "no");
Gameboard.setInventory(5, "no");
Gameboard.setInventory(6, "no");
Gameboard.setInventory(7, "no");
Gameboard.setInventory(8, "no");
Gameboard.setInventory(9, "no");
Gameboard.setInventory(10, "no");
Gameboard.loadScene(0);
Player.setupPlayer();
