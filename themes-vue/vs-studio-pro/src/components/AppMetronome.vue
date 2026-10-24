<template>
  <div class="app-metronome">
    <div class="header">Metronome Player</div>

    <div class="main">
      <div class="center-center">
        <template v-if="layoutType === '1'">
          <div class="d-flex">
            <template v-for="beat in beatsPerMeasure" :key="beat">
              <div class="beat" :class="{ active: counter === beat }">
                {{ counter === beat ? "X" : "O" }}
              </div>
            </template>
          </div>
          <div class="chord-letter">
            {{ chordLetter }}
          </div>
        </template>

        <template v-if="layoutType === '2'">
          <div
            :class="[beatsPerMeasure === 3 ? 'three-four' : 'four-four']"
            class="d-flex justify-center flex-wrap"
          >
            <template
              v-for="(letter, idx) in chordProgression.split(',')"
              :key="idx"
            >
              <div class="beat" :class="{ active: songCounter === idx }">
                {{ letter }}
                <!-- {{ songCounter === idx ? "X" : letter }} -->
              </div>
            </template>
          </div>
        </template>
      </div>

      <hr />
      <div class="d-flex justify-center">
        <DialWheel
          :list="songList.map((x) => x.title)"
          @selection="selectSong($event)"
        ></DialWheel>
      </div>
    </div>

    <div class="footer">
      <button type="button">{{ beatsPerMeasure }} / {{ beatsPerNote }}</button>
      <input
        v-model="bpm"
        @changed="onChangedBpm()"
        type="number"
        style="width: 50px"
      />
      <button type="button" @click="startStop()">
        {{ timerId > 0 ? "Stop" : "Start" }}
      </button>
      <button type="button" @click="clearSong()">Clear Song</button>
      <br />
      <input
        type="radio"
        name="layoutType"
        value="1"
        id="layoutType1"
        v-model="layoutType"
      />
      <label for="layoutType1">Layout 1</label>
      <input
        type="radio"
        name="layoutType"
        value="2"
        id="layoutType2"
        v-model="layoutType"
      />
      <label for="layoutType2">Layout 2</label>
    </div>
  </div>
</template>

<script lang="ts">
import { defineComponent } from "vue";
import DialWheel from "./VDialWheel.vue";
import { ISong } from "../api/metronome";

export default defineComponent({
  name: "AppMetronome",
  components: {
    DialWheel,
  },
  data() {
    return {
      layoutType: "1",
      bpm: 100,
      timerId: 0,
      beatsPerMeasure: 4, // numerator
      beatsPerNote: 4, // denominator
      counter: 0, // layout 1 - counts beats in time signature
      songList: new Array<ISong>(),
      song: {
        title: "",
        signature: "",
        bpm: 0,
        progression: "",
      },
      songCounter: -1, // layout 2 - counts beats in progression
      chordProgression: "",
      progressionLength: 0,
    };
  },
  computed: {
    chordLetter() {
      const idx = this.songCounter === -1 ? 0 : this.songCounter;
      return this.chordProgression.split(",")[idx];
    },
  },
  async created() {
    this.songList = await this.$http.metronome.getSongList();
    this.selectSong();
  },
  methods: {
    metronome() {
      const tempo = (1 / this.beatsPerNote) * this.beatsPerMeasure;

      // formula to convert BPMs to Milliseconds
      const millisecondBpm = (60000 / this.bpm) * tempo;

      this.progressionLength = this.chordProgression.split(",").length;

      //console.log(`\n %cPlaying "${song.title}". \n`, "color: yellow;");

      this.counter = 1;
      this.songCounter = 0;
      this.timerId = window.setInterval(() => {
        this.songCounter += 1;
        if (this.songCounter === this.progressionLength) {
          this.songCounter = 0;
        }

        this.counter += 1;
        if (this.counter > this.beatsPerMeasure) this.counter = 1;
      }, millisecondBpm);
    },
    startStop() {
      if (this.timerId > 0) {
        window.clearInterval(this.timerId);
        this.timerId = 0;
        this.counter = 0;
        this.songCounter = -1;
      } else {
        this.metronome();
      }
    },
    onChangedBpm() {
      if (this.timerId > 0) {
        this.startStop();
        this.metronome();
      }
    },
    selectSong(idx = 0) {
      this.song = this.songList[idx];

      const signatureParts = this.song.signature.split("/");
      this.beatsPerMeasure = parseInt(signatureParts[0], 10); // numerator
      this.beatsPerNote = parseInt(signatureParts[1], 10); // denominator
      this.bpm = this.song.bpm;

      // Parse compress progession
      this.chordProgression = this.decompressChordProgression(
        this.song.progression
      );
    },
    selectedSong(idx = 0) {
      return this.songList[idx].title === this.song.title ? "text-bold" : "";
    },
    clearSong() {
      this.song = {
        title: "",
        signature: "",
        bpm: 0,
        progression: "",
      };

      this.chordProgression = "";
    },
    decompressChordProgression(progression = "") {
      let converted = "";
      progression.split(")").forEach((x) => {
        if (x !== "") {
          let chordLetter = "";
          let cnt = 0;
          x.split("(").forEach((y) => {
            if (cnt % 2 === 0) {
              chordLetter = y + ",";
            } else {
              const numOfBeats = parseInt(y, 10);
              for (let i = 1; i <= numOfBeats; i += 1) {
                converted += chordLetter;
              }
            }
            cnt += 1;
          });
        }
      });
      // Remove last comma
      return converted.substring(0, converted.lastIndexOf(","));
    },
    compressChordProgression(progression = "") {
      let converted = "";
      let chordLetter = "";
      let cnt = 0;
      progression.split(",").forEach((x) => {
        if (chordLetter !== x) {
          if (cnt > 0) converted += `${chordLetter}(${cnt.toString()})`;
          chordLetter = x;
          cnt = 1;
        } else {
          cnt += 1;
        }
      });
      // include the last one since we jump loop early
      converted += `${chordLetter}(${cnt.toString()})`;
      return converted;
    },
  },
});
</script>

<style lang="scss" scoped>
$primary-color: lightblue;
$active-color: red;

.app-metronome {
  background-color: var(--black);
  height: 100%;

  .header {
    background-color: $primary-color;
    padding: 10px;
    font-weight: bold;
  }

  .main {
    color: var(--white);
    height: calc(100vh - 276px);

    .beat {
      height: 48px;
      min-width: 48px;
      font-size: 28px;
      line-height: 1.1;
      text-align: center;
      border: 1px solid $primary-color;

      &.active {
        color: $active-color;
        border: 1px solid $active-color;
      }
    }
  }

  .footer {
    background-color: $primary-color;
    height: 100px;
    padding: 10px;

    button {
      background-color: transparent;
      cursor: pointer;
    }
  }

  .chord-letter {
    font-size: 48px;
  }

  .four-four {
    width: 244px;
  }

  .three-four {
    width: 194px;
  }
}

.text-bold {
  font-weight: bold;
}

.center-center {
  height: 80%;
  display: flex;
  justify-content: flex-start;
  align-items: center;

  flex-direction: column;
  overflow-x: auto;
}
.d-flex {
  display: flex;
}
.flex-wrap {
  flex-wrap: wrap;
}
.justify-center {
  justify-content: center;
}
</style>
