import q from "./queue.js";
import gb from "./gameboard.js";

class TypeWriter {
  constructor() {
    this.running = false;
    this.speed = 80;
  }

  /** Writes a message */
  async write(msg = "") {
    q.enqueue(msg);
    if (!this.running) {
      this.running = true;
      do {
        await this.processMessage(q.peek() || "");
        q.dequeue();
      } while (!q.isEmpty());
      this.running = false;
    }
  }

  /** Outputs the phrase to either console or screen */
  outputAsync(phrase) {
    return new Promise((resolve) => {
      setTimeout(() => {
        // console.log(phrase);
        gb.setMessage(phrase);
        resolve(0);
      }, this.speed);
    });
  }

  /** Process the message */
  async processMessage(msg) {
    const chars = msg.split("");
    let sb = "";
    // for loop is key, forEach would not work
    for (let i in chars) {
      sb += chars[i];
      await this.outputAsync(sb);
    }
  }

  clear() {
    gb.clearMessage();
  }
}

export default new TypeWriter();
