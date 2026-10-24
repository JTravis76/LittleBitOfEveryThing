<template>
  <div>
    <h5>Browser Notification API</h5>
    <p>This demostrates the use of the browser notification API.</p>
  </div>
</template>

<script lang="ts">
import { defineComponent } from "vue";

export default defineComponent({
  name: "BrowserNotificationStory",
  mounted() {
    if (!("Notification" in window)) return;
    if (Notification.permission === "denied") return;

    Notification.requestPermission()
      .then((result) => {
        // (3) states: granted | denied | default
        if (result === "granted") {
          const n = new Notification("Weather Alert", {
            body: "The weather is about to change!",
            data: { url: "https://weather.com", regionCode: 45648 },
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
  },
});
</script>

<style lang="scss"></style>
