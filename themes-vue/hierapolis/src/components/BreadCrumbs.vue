<template>
  <ul id="breadcrumb" class="breadcrumb">
    <template v-if="route.breadcrumbs.length > 0">
      <li v-for="(bc, idx) in route.breadcrumbs" :key="idx">
        <!-- <router-link :to="{ path: bc.link }" :class="[idx == 0 ? 'title' : '']"
          ><span :class="bc.icon"></span> {{ route.name }}</router-link
        > -->
      </li>
      <li v-if="route.name == 'Home'" class="active">&nbsp;</li>
      <li v-else class="active">{{ route.name }}</li>
    </template>
    <template v-else>
      <li class="title">{{ route.name }}</li>
    </template>
  </ul>
</template>

<script lang="ts">
import { defineComponent } from "vue";

export interface IBreadcrumb {
  name?: string;
  breadcrumb: { link: string; icon: string }[];
}

export default defineComponent({
  name: "BreadCrumbs",
  computed: {
    route() {
      const routeData = this.$route.matched[0];
      return {
        name: routeData?.name,
        breadcrumbs: new Array<IBreadcrumb>(),
      };
    },
  },
});
</script>

<style lang="scss"></style>
