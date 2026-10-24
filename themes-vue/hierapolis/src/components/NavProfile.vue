<template>
  <li class="dropdown user">
    <a class="dropdown-toggle" data-toggle="dropdown" href="#">
      <i class="fa fa-user"></i> <strong>{{ Fullname }}</strong>
      <!-- <Avatar round :size="20" :name="Fullname" class="img-rounded"></Avatar> -->
      <b class="caret"></b>
    </a>
    <ul class="dropdown-menu">
      <li><router-link to="/profile">Profile</router-link></li>
      <!--<li class='divider'></li> <li> <a href="/">Sign out</a> </li>-->
    </ul>
  </li>
</template>

<script lang="ts">
import { defineComponent } from "vue";

export default defineComponent({
  name: "NavProfile",
  computed: {
    Fullname(): string {
      // TODO: load profile from store
      return "Travis, Jeremy";
    },
  },
});
</script>

<style lang="scss"></style>
