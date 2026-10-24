<template>
  <div style="margin: 25px">
    <div class="skeleton-ui">
      <div class="row">
        <div class="four columns">
          <div class="photo">
            <img alt="picture of me" class="tilt" src="/img/me.png" />
          </div>
        </div>
        <div class="eight columns">
          <div class="social-bar">
            <a href="http://www.linkedin.com/in/jtravis76/" target="blank">
              <img alt="linkedin" src="/img/linkedin.png" />
            </a>
            <a href="https://github.com/JTravis76/" target="blank">
              <img alt="github" src="/img/gh-icon.png"
            /></a>
            <a href="https://www.youtube.com/@jtravis76/" target="blank">
              <img alt="youtube" src="/img/yt-icon.png" />
            </a>
          </div>
          <div class="about-me--title">Jeremy R. Travis</div>
          <div class="about-me--bio">
            <p>
              Out of high school, I worked a few plant labor jobs until the year
              of 2004; when I was ask for an office position. After learning the
              ropes of a new job, I began finding interest in database and
              ASP.NET. Been experimenting with Visual Studio C++ prior to this,
              thinking I wanted to be a game developer.
            </p>
            <p>
              Working as an Office Professional, my boss had given me the
              liberty to improve a few processes. I quickly began to develop a
              dashboard to display our daily metrics using web form to save data
              entered by another employee. This replaces most of those boring
              spreadsheet.
            </p>
            <p>
              Later learn, how powerful and useful VBA macros were in Microsoft
              Excel. I became a macro writing wizard. Soon, everyone wanted me
              to write scripts to help their daily tasks.
            </p>
            <p>
              All that ended the year of 2009, as the plant was closing it door
              due to a buyout. I started working at Life Ambulance as a Tier I
              helpdesk. In a few months of quality work, I was promoted to
              Network / System Administrator. Two years later, First Med bought
              out Life Ambulance in July of 2011. Through hard work and willing
              to help the company, I developed a HR portal to integrate with
              in-house dispatch software. Which the portal eased in many Human
              Resource related tasks, mainly exporting employee hours into
              third-party vendor software for payroll.
            </p>
            <p>
              In August 2012, I was promoted to .NET Developer / IT Specialist.
              Join the Development team to help write enhancement to the
              company\'s dispatch software. Later, other projects such as;
              document management system, improvement in other portals for
              employees, billing department, etc.
            </p>
            <p>&nbsp;</p>
            <h3 class="title">I design and build amazing things.</h3>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script lang="ts">
import { defineComponent } from "vue";

export default defineComponent({
  name: "AppAboutMe",
});
</script>

<style lang="scss" scoped>
.social-bar {
  align-items: center;
  display: flex;
  flex-wrap: wrap;
  justify-content: flex-end;

  a {
    margin: 2px;
  }
}

.about-me--title {
  font-size: 3rem;
  font-weight: bold;
  font-family: "Happy Monkey", cursive;

  @include xs {
    font-size: 30px;
  }
}

.about-me--bio {
  font-family: "Happy Monkey", cursive;
  font-size: 1rem;
}

.photo {
  padding: 5px;

  img {
    max-width: 250px;
    height: auto;
    width: 95%;
    vertical-align: middle;

    @include xs {
      max-width: 1000px;
    }
  }
}

.tilt {
  border: 10px solid #ffffcc;
  behavior: url(-ms-transform.htc);
  -moz-transform: rotate(-15deg);
  /* Firefox */
  -webkit-transform: rotate(-15deg);
  /* Chrome & Safari */
  -o-transform: rotate(-15deg);
  /* Opera */
  -ms-transform: rotate(-15deg);
  /* IE 9+ */
  transform: rotate(-15deg);
  /* CSS3 */
  filter: progid:DXImageTransform.Microsoft.Matrix(sizingMethod='auto expand', M11=0.7071067811865476, M12=-0.7071067811865475, M21=0.7071067811865475, M22=0.7071067811865476);
  -ms-filter: "progid:DXImageTransform.Microsoft.Matrix(SizingMethod='auto expand', M11=0.7071067811865476, M12=-0.7071067811865475, M21=0.7071067811865475, M22=0.7071067811865476)";

  @include xs {
    -moz-transform: rotate(0deg);
    /* Firefox */
    -webkit-transform: rotate(0deg);
    /* Chrome & Safari */
    -o-transform: rotate(0deg);
    /* Opera */
    -ms-transform: rotate(0deg);
    /* IE 9+ */
    transform: rotate(0deg);
  }
}
</style>
