export default {
  name: "app-binary-clock",
  template: `
<div class="clock-container">
  <div class="binary-clock--container">
    <template v-for="(led, idx) in leds.split('')" :key="idx">
      <div
        :class="{ active: led === '1', hidden: led === '-'}"
        class="led">
      </div>
    </template>
  </div>
  <div class="digital-clock--container">
    <template v-for="part in timeParts">
      <img :alt="part" :src="'./img/clock/' + part + '.gif'" />
    </template>
  </div>
</div>`,
  data() {
    return {
      timeParts: ["8", "8", "dgc", "8", "8", "dgc", "8", "8", "am"],
      leds: "--0000000000000000000000",
      timerIdx: 0,
    };
  },
  created() {
    this.timerIdx = window.setInterval(() => {
      const d = new Date();
      let hr = d.getHours() + 100;
      const mn = d.getMinutes() + 100;
      const se = d.getSeconds() + 100;
      let am_pm = "am";
      if (hr === 100) {
        hr = 112;
        am_pm = "am";
      } else if (hr < 112) am_pm = "am";
      else if (hr === 112) am_pm = "pm";
      else if (hr > 112) {
        am_pm = "pm";
        hr = hr - 12;
      }
      const timeStr = "" + hr.toString() + mn.toString() + se.toString();
      this.timeParts[0] = timeStr.substring(1, 2);
      this.timeParts[1] = timeStr.substring(2, 3);
      this.timeParts[3] = timeStr.substring(4, 5);
      this.timeParts[4] = timeStr.substring(5, 6);
      this.timeParts[6] = timeStr.substring(7, 8);
      this.timeParts[7] = timeStr.substring(8, 9);
      this.timeParts[8] = am_pm;

      let newTime = "--";
      const hh = d.getHours().toString().padStart(2, "0");
      const mm = d.getMinutes().toString().padStart(2, "0");
      const ss = d.getSeconds().toString().padStart(2, "0");
      newTime += this.toBinary(hh[0]).substring(2, 4);
      newTime += this.toBinary(hh[1]);
      newTime += this.toBinary(mm[0]);
      newTime += this.toBinary(mm[1]);
      newTime += this.toBinary(ss[0]);
      newTime += this.toBinary(ss[1]);
      this.leds = newTime;
    }, 1000);
  },
  beforeUnmount() {
    window.clearInterval(this.timerIdx);
  },
  methods: {
    toBinary(v) {
      const num = parseInt(v.toString(), 10);
      return num.toString(2).padStart(4, "0");
    },
  },
};
