<template>
<div style="margin: 20px;">
  <h3>Simple Client-Side CSV Parser</h3>
  <p>
    To use this demo, first create a csv file with the following content:
    <div style="background-color: black; color: green; width:50%">
      <div>
        <button 
        type="button" 
        style="float: right;"
        :class="{ copied }" 
        @click="setClipboard()">{{ copied ? "Copied" : "Copy" }}</button>
      </div>
      <pre>
type,timeOfDay,temp,unit,rating
"Normal","Day",5,"F","1250"
"Load Dump","Night",10,"F","1425"
      </pre>   
    </div>
  </p>
  <input type="checkbox" v-model="hasHeader" aria-label="has header data" /> My data has header
  <br />
  <input type="file" accept=".csv" id="csv" />
  <div style="margin-top:20px;">
    <button type="button" @click=process> Process </button>
  </div>
  <hr />
  <table>
    <thead>
      <tr>
        <th>Type</th>
        <th>Time</th>
        <th>Temp</th>
        <th>Unit</th>
        <th>Rating</th>
      </tr>
    </thead>
    <tbody>
      <template v-for="(item, idx) in arr" :key="idx">
        <tr :data-idx="idx">
          <td>{{ item.type }}</td>
          <td>{{ item.timeOfDay }}</td>
          <td>{{ item.temp }}</td>
          <td>{{ item.unit }}</td>
          <td>{{ item.rating }}</td>
        </tr>
      </template>     
    </tbody>
  </table>
</div>
</template>

<script lang="ts">
import { defineComponent } from "vue";

export interface IRating {
  type: string;
  timeOfDay: string;
  temp: number;
  unit: string;
  rating: number;
}

export default defineComponent({
  name: "AppParseCsv",
  data() {
    return {
      arr: new Array<IRating>(),
      hasHeader: true,
      copied: false,
    };
  },
  methods: {
    process() {
      const el = document.getElementById("csv") as HTMLInputElement;
      if (el.files) {
        const file = el.files[0]; 

        const reader = new FileReader();
        reader.addEventListener("load", (e) => {
          this.arr = new Array<IRating>();

          const result = e.target?.result;
          if (result && typeof result === "string") {
            let cnt = 0;
          let headers = new Array<string>;
          result.split("\r\n").forEach((x) => {
            if (cnt === 0 && this.hasHeader) {
              headers = x.split(",");
            } else {
              const data = x.replaceAll('"', "").split(",");

              const obj = {} as { [key: string]: string };
              let i = 0;
              for (const key of headers) {
                obj[key] = data[i];
                i = i + 1;
              }
              this.arr.push(obj as unknown as IRating);
            }
            cnt = cnt + 1;
          });
          }
          
        });

      if (file) {
        reader.readAsText(file);
      }
      }
    },
    setClipboard() {
      const div = document.querySelector("pre") as HTMLPreElement;
      navigator.clipboard.writeText(div.textContent?.trim() || "");

      this.copied = true;
      window.setTimeout(() => {
        this.copied = false;
      }, 1500);
    }
  },
});
</script>

<style lang="scss" scoped>
.copied {
  color: red;
}

table {
  width: 100%;

  thead tr {
    border-bottom: 1px solid;
  }
}
</style>
