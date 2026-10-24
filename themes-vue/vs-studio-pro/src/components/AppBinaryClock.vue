<template>
  <div class="clock-container">
    <div class="binary-clock--container">
      <template v-for="(led, idx) in leds.split('')" :key="idx">
        <div
          :class="{ active: led === '1', hidden: led === '-' }"
          :data-idx="idx"
          class="led"
        ></div>
      </template>
    </div>
    <div class="digital-clock--container">
      <template v-for="part in timeParts">
        <img :alt="part" :src="'/img/clock/' + part + '.gif'" />
      </template>
    </div>
  </div>
</template>

<script lang="ts">
import { defineComponent } from "vue";

export default defineComponent({
  name: "AppBinaryClock",
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
    toBinary(v: string) {
      const num = parseInt(v.toString(), 10);
      return num.toString(2).padStart(4, "0");
    },
  },
});
</script>

<style lang="scss" scoped>
/*== Standard/Binary Clock ==*/
.clock-container {
  margin: 0;
  width: 100%;
  height: 100%;
  background-color: var(--black);
}

.binary-clock--container {
  width: 200px;
  height: 124px;
  background-color: var(--black);
  color: var(--white);
  padding: 4px;
  display: flex;
  justify-content: space-around;
  flex-wrap: wrap;
  flex-direction: column;
}

.binary-clock--container {
  .led {
    width: 24px;
    height: 24px;
    margin: 2px;
    border: 1px solid red;
    border-radius: 50%;
    background-color: rgba(255, 0, 0, 0.3);
  }

  .active {
    background-color: rgba(255, 0, 0, 0.8);
    box-shadow: inset 0 0 10px 2px black;
  }

  .hidden {
    border: 0px solid transparent;
    background-color: transparent;
  }
}

.digital-clock--container {
  width: 200px;
  display: flex;
  justify-content: center;
  align-items: center;
}
</style>
