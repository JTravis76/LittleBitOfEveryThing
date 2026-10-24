export default {
  name: "app-parse-csv",
  template: `
<div style="margin: 20px;">
  <h3>Simple Client-Side CSV Parser</h3>
  <p>
    To use this demo, first create a csv file with the following content:
    <div style="background-color: black; color: green; width:50%">
      <code>
        type,timeOfDay,temp,unit,rating<br />
        "Normal","Day",5,"F","XR1"<br />
        "Load Dump","Night",10,"F","XR2"
      </code>   
    </div>
  </p>
  <input type="file" accept=".csv" id="csv" />
  <br />
  <input type="checkbox" v-model="hasHeader" /> has header
  <br />
  <button type="button" @click=process> Process </button>
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
        <tr>
          <td>{{ item.type }}</td>
          <td>{{ item.timeOfDay }}</td>
          <td>{{ item.temp }}</td>
          <td>{{ item.unit }}</td>
          <td>{{ item.rating }}</td>
        </tr>
      </template>     
    <tbody>
  </table>
</div>`,
  data() {
    return {
      arr: [],
      hasHeader: true,
    };
  },
  methods: {
    process() {
      const el = document.getElementById("csv");
      const file = el.files[0];

      const reader = new FileReader();
      reader.addEventListener("load", (e) => {
        this.arr = [];

        const result = e.target.result;
        let cnt = 0;
        let headers = [];
        result.split("\r\n").forEach((x) => {
          if (cnt === 0 && this.hasHeader) {
            headers = x.split(",");
          } else {
            const data = x.replaceAll('"', "").split(",");

            const obj = {};
            let i = 0;
            for (const key of headers) {
              obj[key] = data[i];
              i = i + 1;
            }
            this.arr.push(obj);
          }
          cnt = cnt + 1;
        });
      });

      if (file) {
        reader.readAsText(file);
      }
    },
  },
};
