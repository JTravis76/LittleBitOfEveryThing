class Queue {
  constructor() {
    this.messages = new Array();
  }
  enqueue(msg = "") {
    this.messages.push(msg);
  }
  dequeue() {
    return this.messages.shift();
  }
  isEmpty() {
    return this.messages.length === 0;
  }
  peek() {
    return this.isEmpty() ? undefined : this.messages[0];
  }
}

export default new Queue();
