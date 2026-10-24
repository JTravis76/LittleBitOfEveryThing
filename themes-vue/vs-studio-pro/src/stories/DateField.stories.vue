<template>
  <div class="row">
    <div class="eight columns">
      <div>
        <label>Birthday</label>
        <component
          :is="'input'"
          type="date"
          :value="birthday"
          v-model="birthday"
        ></component>
      </div>
      <p>
        <DateField placeholder="Birthday" v-model="birthday"></DateField>
      </p>
    </div>
    <div class="four columns"></div>
  </div>
</template>

<script lang="ts">
import { defineComponent } from "vue";
import DateField from "../components/VDateField.vue";

export default defineComponent({
  name: "DateFieldStory",
  components: {
    DateField,
  },
  data() {
    return {
      birthday: new Date("11/5/1955").toJSON().substring(0, 10),
    };
  },
});
</script>

<style lang="scss"></style>
