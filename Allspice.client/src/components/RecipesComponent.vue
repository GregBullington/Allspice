<template>
  <div class="row">
    <div
      class="col hero-container card elevation-4 selectable grow p-0 mx-3 mb-4"
    >
      <img class="img-container" :src="recipe.imgUrl" alt="Recipe Image" />
      <h2 class="pill-bg det-font top-left">{{ recipe.category }}</h2>
      <div class="card desc-bg bottom-left det-font">
        <h2 class="m-0">{{ recipe.title }}</h2>
        <p class="fs-5 fst-italic fw-light m-0">
          Lorem ipsum dolor sit amet consectetur adipisicing elit.
        </p>
      </div>
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
.pill-bg {
  color: white;
  background: rgba(0, 0, 0, 0.658);
  padding-inline: 20px;
  border-radius: 15px;
}
.desc-bg {
  color: white;
  background: rgba(0, 0, 0, 0.658);
  padding-inline: 20px;
  width: 100%;
  border-radius: 0;
}
.img-container {
  height: 300px;
  object-fit: cover;
}
</style>