<template>
  <div
    class="modal fade modal-dialog-bg"
    id="detailsModal"
    aria-hidden="true"
    aria-labelledby="detailsModal"
    tabindex="-1"
  >
    <div class="modal-dialog modal-dialog-centered modal-xl">
      <div class="modal-content">
        <div class="modal-body p-0">
          <div class="row">
            <div class="col-md-4">
              <img
                class="img-container"
                :src="activeRecipe.imgUrl"
                alt="RecipeImage"
              />
            </div>
            <div class="col-md-8 container-fluid">
              <div class="row">
                <div class="col-2 ms-auto text-end m-2">
                  <button
                    type="button"
                    class="btn-close"
                    data-bs-dismiss="modal"
                    aria-label="Close"
                  ></button>
                </div>
              </div>
              <div class="row">
                <div class="col d-inline-flex">
                  <h1 class="text-primary det-font fs-1 px-4">
                    {{ activeRecipe.title }}
                  </h1>
                  <h2 class="pill-bg det-font fs-5">
                    {{ activeRecipe.category }}
                  </h2>
                </div>
                <p class="fs-5 det-font text-secondary darken-30 fw-light px-5">
                  Lorem ipsum dolor sit amet.
                </p>
              </div>
              <div class="row">
                <div class="col-md-5 card elevation-3 ms-4">
                  <div class="row">
                    <div class="col p-1 text-center bg-primary">
                      <h3 class="mb-0 det-font">Recipe Steps</h3>
                    </div>
                  </div>
                  <div class="row">
                    <div class="col">
                      <ol class="p-3 overflow-auto">
                        <li v-for="s in steps" :key="s.id">
                          <p>{{ s.body }}</p>
                        </li>
                      </ol>
                    </div>
                  </div>
                  <div class="modal-footer p-0 d-flex flex-column mt-auto">
                    <div class="row">
                      <form class="p-0" @submit.prevent="createStep()">
                        <div class="input-group">
                          <input
                            class="form-control"
                            type="text"
                            name=""
                            id="addStep"
                            placeholder="Add step.."
                          />
                          <button
                            class="mdi mdi-plus input-group-addon text-primary"
                            type="submit"
                            id="addStep"
                          ></button>
                        </div>
                      </form>
                    </div>
                  </div>
                </div>
                <div class="col-md-5 card elevation-3 ms-4">
                  <div class="row">
                    <div class="col p-1 text-center bg-primary">
                      <div class="row">
                        <h3 class="mb-0 det-font">Ingredients</h3>
                      </div>
                    </div>
                    <div class="row">
                      <div class="col">
                        <ul class="p-3 overflow-auto">
                          <li v-for="i in ingredients" :key="i.id">
                            <p class="">{{ i.quantity }} {{ i.name }}</p>
                          </li>
                        </ul>
                      </div>
                    </div>
                  </div>
                  <div class="modal-footer p-0 d-flex flex-column mt-auto">
                    <div class="row">
                      <form class="p-0" @submit.prevent="createIngredient()">
                        <div class="input-group">
                          <input
                            class="form-control"
                            type="text"
                            name=""
                            id="addStep"
                            placeholder="Add ingredient.."
                          />
                          <button
                            class="mdi mdi-plus input-group-addon text-primary"
                            type="submit"
                            id="addStep"
                          ></button>
                        </div>
                      </form>
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>


<script>
import { computed, reactive } from "@vue/reactivity"
import { AppState } from "../AppState"
import { recipesService } from "../services/RecipesService"
import Pop from "../utils/Pop"
import { onMounted } from "@vue/runtime-core"
import { logger } from "../utils/Logger"
export default {
  setup() {
    const state = reactive({
      editable: {},
    });
    return {
      state,
      activeRecipe: computed(() => AppState.activeRecipe),
      ingredients: computed(() => AppState.activeIngredients),
      steps: computed(() => AppState.activeSteps),

      async createStep() {
        try {
          await recipesService.createRecipe(state.editable)
          state.editable = {}
        } catch (error) {
          logger.error(error)
          Pop.toast("Something went wrong creating step!", 'error')
        }
      },

      async createIngredient() {
        try {
          await recipesService.createIngredient(state.editable)
          state.editable = {}
        } catch (error) {
          logger.error(error)
          Pop.toast("Something went wrong creating ingredient!", 'error')
        }
      }
    }
  }
}
</script>


<style lang="scss" scoped>
.img-container {
  height: 500px;
  max-width: 260px;
  object-fit: cover;
}
.pill-bg {
  color: white;
  background: rgba(49, 49, 49, 0.658);
  padding-inline: 20px;
  border-radius: 15px;
}
#addStep {
  background: none;
  outline: none;
  box-shadow: none;
  border: none;
}
.modal-dialog-bg {
  backdrop-filter: blur(5px);
}
</style>