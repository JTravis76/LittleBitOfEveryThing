export default {
  template: `
<div>
  <h3>Browser Notification API</h3>
  <p>This demostrates the use of the browser notification API.</p>
</div>
`,
  name: "browser-notification-story",
  mounted() {
    if (!("Notification" in window)) {
      if (Notification.permission === "denied") return;

      Notification.requestPermission()
        .then((result) => {
          // (3) states: granted | denied | default
          if (result === "granted") {
            const n = new Notification("Weather Alert", {
              body: "The weather is about to change!",
              data: { url: "https://weather.com", regionCode: 45653 },
              lang: "en-US",
              icon: undefined,
              timestamp: Math.floor(Date.now()),
              vibrate: [100, 200, 100],
              requireInteraction: true,
              renotify: false,
            });

            // display the data
            console.log(n.data);
          }
        })
        .catch((err) => console.error(err));
    }
  },
};
