<template>
  <div>
    <div class="app-banner-notification__section">
      <div
        aria-live="assertive"
        aria-atomic="true"
        class="app-banner-notification__section__type--persistent"
      >
        <template v-for="(notice, idx) in notifications">
          <TopBanner
            v-if="notice.type === 'persistent'"
            :key="idx"
            :banner-style="notice.style"
            :banner-type="notice.type"
            :message="notice.message"
            :sub-message="notice.subMessage"
            @dismiss="close(idx)"
          />
        </template>
      </div>
    </div>
    <div class="app-banner-notification__section">
      <div
        aria-live="assertive"
        aria-atomic="true"
        class="app-banner-notification__section__type--dismissible"
      >
        <template v-for="(notice, idx) in notifications">
          <TopBanner
            v-if="notice.type === 'dismissible'"
            :key="idx"
            :banner-style="notice.style"
            :banner-type="notice.type"
            :message="notice.message"
            :sub-message="notice.subMessage"
            :timer="notice.timer"
            @dismiss="close(idx)"
          />
        </template>
      </div>
    </div>
  </div>
</template>

<script lang="ts">
import { defineComponent } from "vue";
import TopBanner from "./TopBanner.vue";
import { IBanner } from "../store/modules/banner";

export default defineComponent({
  name: "TopBannerNotification",
  components: {
    TopBanner,
  },
  computed: {
    notifications() {
      return this.$store.state.bannerStore.banners as IBanner[];
    },
  },
  methods: {
    close(idx: number) {
      this.$store.commit("bannerStore/remove", idx);
    },
  },
});
</script>

<style lang="scss" scoped>
.app-banner-notification__section,
.app-banner-notification__section__type--persistent {
  position: relative;
  width: 100%;
}

.app-banner-notification__section__type--dismissible {
  position: absolute;
  top: 0;
  left: 0;
  width: 100%;
}
</style>
