export class Transaction {
  constructor(init) {
    this.id = 0;
    this.date = null;
    this.transaction = null;
    this.name = null;
    this.memo = null;
    this.amount = 0;
    this.tag = null;
    if (init) Object.assign(this, init);
  }
}
