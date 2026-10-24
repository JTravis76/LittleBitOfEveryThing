import Avatar from "../components/v-avatar.js";

export default {
  template: `
  <div class="row">
  <div class="col-lg-8">
    <Avatar
      :name="name"
      :size="size"
      :round="round">
    </Avatar>
  </div>
  <div class="col-lg-4">
    <fieldset>
      <legend>Info</legend>
      This story shows the use of the Avatar component.
      <p>Name prop can be either 'First Last' or 'Last, First'</p>
    </fieldset>
  </div>
</div>`,
  name: "avatar-story",
  components: {
    Avatar,
  },
  data() {
    return {
      name: "Jeremy Travis",
      size: 92,
      round: true,
    };
  },
};
