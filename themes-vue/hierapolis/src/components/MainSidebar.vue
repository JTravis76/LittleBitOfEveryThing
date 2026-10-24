<template>
  <section id="sidebar">
    <button type="button" class="btn btn-xs btn-link" @click="toggleSidebar">
      <i id="toggle" class="fa fa-align-justify icon-large"></i>
    </button>
    <ul id="dock">
      <li class="launcher">
        <router-link to="/dashboard">
          <i class="fa fa-dashboard"></i> <a><span>Dashboard</span></a>
        </router-link>
      </li>

      <li v-if="$UserInRole('Admin')" class="launcher dropdown">
        <i class="fa fa-circle-o"></i>
        <a href="#" data-toggle="dropdown" class="dropdown-toggle">Admin</a>
        <ul class="dropdown-menu">
          <li class="dropdown-header">Administrative</li>
          <li><router-link to="/grouptypes">Group Types</router-link></li>
          <li><router-link to="/emailgroups">Email Groups</router-link></li>
          <li class="divider"></li>
        </ul>
      </li>

      <li v-if="$UserInRole('NCManager')" class="launcher">
        <router-link to="/ncmanager">
          <i class="fa fa-users"></i> <a><span>Administration</span></a>
        </router-link>
      </li>

      <li class="launcher">
        <router-link to="/about">
          <i class="fa fa-question"></i> <a><span>About</span></a>
        </router-link>
      </li>
    </ul>
    <div id="beaker" data-toggle="tooltip" title="Made by lab2023"></div>
  </section>
</template>

<script lang="ts">
import { defineComponent } from "vue";

export default defineComponent({
  name: "MainSidebar",
  methods: {
    toggleSidebar() {
      const lis = document.querySelectorAll("li.launcher");
      for (let l = 0; l < lis.length; l++) {
        let li = lis[l] as HTMLLIElement;

        let i = li.firstElementChild as HTMLElement;
        if (i != null)
          i.style.display = i.style.display == "none" ? "" : "none";
      }
    },
  },
});
</script>

<style lang="scss"></style>
