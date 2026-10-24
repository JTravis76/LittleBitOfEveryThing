export default {
  name: "app-metronome",
  template: `
<div class="app-metronome">
  <div class="header">Metronome Player</div>

  <div class="main">
    <div class="center-center">

      <template v-if="layoutType === '1'">
        <div class="d-flex">
          <template v-for="beat in beatsPerMeasure" :key="beat">
            <div class="beat">
              {{ counter === beat ? 'X' : 'O' }}
            </div>
          </template>
        </div>
        <div class="chord-letter">
          {{ chordLetter }}
        </div>
      </template>

      <template v-if="layoutType === '2'">
        <div :class="[beatsPerMeasure === 3 ? 'three-four' : 'four-four']"
              class="d-flex justify-center flex-wrap">
          <template v-for="(letter, idx) in chordProgression.split(',')"
                    :key="idx">
            <div class="beat">
              {{ songCounter === idx ? "X" : letter }}
            </div>
          </template>
        </div>
      </template>  
    </div>

    <hr />
    <div class="d-flex justify-center">
      <ul>
        <li v-for="(song, idx) in songList"
            :key="song.title">
          <label :class="selectedSong(idx)"
                  @click="selectSong(idx)">
            {{ song.title }}
          </label>
        </li>
      </ul>
    </div>
  </div>

  <div class="footer">
    <button type="button">
      {{ beatsPerMeasure }} / {{ beatsPerNote }}
    </button>
    <input v-model="bpm"
          @changed="onChangedBpm()"
          type="number"
          style="width:50px" />
    <button type="button" @click="startStop()">
      {{ timerId > 0 ? 'Stop' : 'Start' }}
    </button>
    <button type="button" @click="clearSong()">
      Clear Song
    </button>
    <br />
    <input type="radio" 
          name="layoutType" 
          value="1" 
          id="layoutType1"
          v-model="layoutType" />
    <label for="layoutType1">Layout 1</label>
    <input type="radio" 
          name="layoutType" 
          value="2"
          id="layoutType2"
          v-model="layoutType"/>
    <label for="layoutType2">Layout 2</label>
  </div>
</div>`,
  data() {
    return {
      layoutType: "1",
      bpm: 100,
      timerId: 0,
      beatsPerMeasure: 4, // numerator
      beatsPerNote: 4, // denominator
      counter: 0,
      songList: [],
      song: {
        title: "",
        signature: "",
        bpm: 0,
        progression: "",
      },
      songCounter: -1,
      chordProgression: "",
      progressionLength: 0,
    };
  },
  computed: {
    chordLetter() {
      return this.chordProgression.split(",")[
        this.songCounter === -1 ? 0 : this.songCounter
      ];
    },
  },
  created() {
    this.songList = [
      {
        title: "Amazing Grace",
        signature: "3/4",
        bpm: 70,
        progression: "G(6)C(3)G(9)D(6)G(6)C(6)Em(3)D(3)G(6)",
      },
      {
        title: "Crawdad Song",
        signature: "4/4",
        bpm: 100,
        progression: "G(12)D(4)G(4)C(4)G(2)D(2)G(4)",
      },
      {
        title: "I'll Fly Away",
        signature: "4/4",
        bpm: 104,
        progression: "G(8)C(4)G(15)D(1)G(4)",
      },
    ];
    this.selectSong();
  },
  methods: {
    metronome() {
      const tempo = (1 / this.beatsPerNote) * this.beatsPerMeasure;

      // formula to convert BPMs to Milliseconds
      const millisecondBpm = (60000 / this.bpm) * tempo;

      this.progressionLength = this.chordProgression.replaceAll(",", "").length;

      //console.log(`\n %cPlaying "${song.title}". \n`, "color: yellow;");

      this.counter = 1;
      this.timerId = window.setInterval(() => {
        this.songCounter += 1;
        if (this.songCounter === this.progressionLength - 1) {
          this.songCounter = -1;
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
};
