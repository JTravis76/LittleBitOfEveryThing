<template>
  <div id="navbar" class="navbar navbar-default">
    <router-link to="/" class="navbar-brand"
      ><i class="fa fa-beer" />
      <span class="text-bold">Hierapolis</span></router-link
    >
    <ul class="nav navbar-nav pull-right">
      <NavMessage></NavMessage>
      <template v-if="$UserInRole('Admin')">
        <li>
          <router-link to="/settings"
            ><i class="fa fa-cog"></i> Settings</router-link
          >
        </li>
      </template>
      <NavProfile></NavProfile>
    </ul>
  </div>
</template>

<script lang="ts">
import { defineComponent } from "vue";
import NavMessage from "./NavMessage.vue";
import NavProfile from "./NavProfile.vue";

export default defineComponent({
  name: "NavBar",
  components: {
    NavMessage,
    NavProfile,
  },
});
</script>

<style lang="scss"></style>
