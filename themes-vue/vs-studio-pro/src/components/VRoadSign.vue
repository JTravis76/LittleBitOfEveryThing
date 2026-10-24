<template>
  <div
    class="road-sign"
    :class="{
      active,
      left: orientation === 'left',
      right: orientation === 'right',
    }"
    :style="`top:${top}px;left:${left}px;`"
    @click="clicked"
  >
    <div class="sign">
      <div class="circle">
        <span :class="icon"></span>
      </div>
      <span class="text" v-html="text"></span>
    </div>
    <button type="button" class="end-cap">
      <span class="icon-plus"></span>
    </button>
    <!-- <span class="app-icon-stack"
      v-show="driverReview || driverAlert">
      <i class="app-icon-circle"></i>
      <i class="app-icon-search-circle-square" v-show="driverReview"></i>
      <i class="app-icon-exclamation-circle-square" v-show="driverAlert"></i>
    </span> -->
    <span v-show="driverReview" class="dealer-review">
      <span class="icon-search"></span>
    </span>
    <span v-show="driverAlert" class="dealer-alert grow-shrink">
      <!-- <font-awesome-icon :icon="faExclamation" /> -->
    </span>
  </div>
</template>

<script lang="ts">
import { defineComponent } from "vue";
//import { faExclamation } from "@fortawesome/free-solid-svg-icons";

export default defineComponent({
  name: "RoadSign",
  props: {
    id: {
      type: String,
      required: true,
    },
    orientation: {
      type: String,
      default: "right", // 'right' || 'left'
    },
    text: {
      type: String,
      default: "",
    },
    icon: {
      type: String,
      default: "icon-dealer",
    },
    active: {
      type: Boolean,
      default: false,
    },
    driverAlert: {
      type: Boolean,
      default: false,
    },
    driverReview: {
      type: Boolean,
      default: false,
    },
    x: {
      type: Number,
      default: 0,
    },
    y: {
      type: Number,
      default: 0,
    },
  },
  emits: ["method"],
  data() {
    return {
      //faExclamation,
      left: this.x,
      top: this.y,
    };
  },
  created() {
    if (window.outerWidth < 415) {
      if (this.orientation === "right") this.left = -75;
      if (this.orientation === "left") this.left = -60;
    }
  },
  methods: {
    clicked() {
      this.$emit("method", this.id);
    },
  },
});
</script>

<style lang="scss" scoped>
.road-sign {
  .end-cap {
    position: relative;
    border-radius: 50%;
    border: 0px;
    background: var(--color--primary);
    color: var(--color--white);
    width: 40px;
    height: 40px;
  }

  .app-icon-stack {
    position: relative;
    display: inline;
    font-size: 30px;

    .app-icon-circle {
      color: white;
    }

    .app-icon-exclamation-circle-square,
    .app-icon-search-circle-square {
      color: lightgreen;
      position: relative;
      left: -32px;
    }
  }

  .dealer-review {
    position: relative;
    background: white;
    border: 4px solid lightgreen;
    border-radius: 50%;
    color: lightgreen;
    padding-top: 0px;
    padding-left: 4px;
    display: inline-block;
    width: 32px;
    height: 32px;
    transform: scaleX(-1); // Flip It

    .app-icon-search-circle-square,
    .icon-search {
      font-size: 18px;
    }
  }

  .dealer-alert {
    position: relative;
    background: white;
    // border: 4px solid lightgreen;
    border-radius: 50%;
    // color: lightgreen;
    padding-top: 0px;
    padding-left: 9px;
    display: inline-block;
    width: 32px;
    height: 32px;

    .fa-exclamation {
      font-size: 18px;
    }
  }

  .dealer-review,
  .dealer-alert {
    border: 4px solid lightgreen;
    color: lightgreen;
  }

  &:hover {
    .circle {
      border: 6px solid blue;
      span {
        color: blue;
      }
    }
    .text {
      color: blue;
    }
  }
}

.circle {
  border: 6px solid var(--color--primary);
  span {
    color: var(--color--primary);
  }
}
span.text {
  color: var(--color--primary);
  display: flex;
  flex-direction: column;
  justify-content: center;
  line-height: 1.4em;
  height: 100px;
}

// == Right-side of road
.right {
  .sign {
    border-top-left-radius: 70px;
    border-bottom-left-radius: 70px;
    border-left: transparent !important;
  }
  .end-cap {
    padding: 1px 0 0 0;
    left: 225px;
    top: -68px;
  }
  .circle {
    float: left;
    margin: -2px 0 0px 0;
  }
  .app-icon-stack,
  .dealer-review,
  .dealer-alert {
    top: -18px;
    left: -10px;
  }
}
// == Left-side of road
.left {
  .sign {
    border-top-right-radius: 70px;
    border-bottom-right-radius: 70px;
    border-right: transparent !important;
  }
  .end-cap {
    padding-top: 4px;
    left: -20px;
    top: -68px;
  }
  .circle {
    float: right;
  }
  .app-icon-stack,
  .dealer-review,
  .dealer-alert {
    top: -18px;
    left: 250px;
  }
}

.sign {
  border: 3px lightgray solid;
  width: 350px;
  height: 100px;
  background-color: white;
  box-shadow: 0px 4px 10px 0px;
  cursor: pointer;
  .circle {
    border-radius: 50%;
    height: 95px;
    width: 95px;
    display: flex;
    justify-content: center;
    align-items: center;
    span {
      font-size: 40px;
    }
  }
  span.text {
    position: relative;
    /*top: 15px;*/
    left: 25px;
    letter-spacing: 1px;
    font-weight: bold;
  }
}

.active {
  .circle {
    border: 10px red solid !important;
    span {
      color: red !important;
    }
  }
  span.text {
    color: red !important;
  }
  .end-cap {
    background-color: red !important;
  }
}
</style>
