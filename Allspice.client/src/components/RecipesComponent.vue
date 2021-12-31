<template>
  <div class="Recipe card elevation-4 mb-4 mx-4 mt-2 selectable grow">
    <div class="row">
      <img class="img-container" :src="recipe.imgUrl" alt="Recipe Image" />
      <h2 class="det-text det-font">{{ recipe.category }}</h2>
    </div>
  </div>
</template>


<script>
import { computed } from "@vue/reactivity"
import { AppState } from "../AppState"
export default {
  props: {
    recipe: { type: Object }
  },
  setup() {

    return {
      recipes: computed(() => AppState.recipes)
    }
  }
}
</script>


<style lang="scss" scoped>
.det-text {
  text-align: center;
  position: absolute;
  top: 50%;
  left: 50%;
  transform: translate(-50%, -50%);
  color: white;
}
.img-container {
  height: 300px;
  object-fit: cover;
}

/* Container holding the image and the text */
.container {
  position: relative;
  text-align: center;
  color: white;
}

/* Bottom left text */
.bottom-left {
  position: absolute;
  bottom: 8px;
  left: 16px;
}

/* Top left text */
.top-left {
  position: absolute;
  top: 8px;
  left: 16px;
}

/* Top right text */
.top-right {
  position: absolute;
  top: 8px;
  right: 16px;
}

/* Bottom right text */
.bottom-right {
  position: absolute;
  bottom: 8px;
  right: 16px;
}

/* Centered text */
.centered {
  position: absolute;
  top: 50%;
  left: 50%;
  transform: translate(-50%, -50%);
}
</style>