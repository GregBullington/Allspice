<template>
  <div
    class="modal fade"
    id="recipeModal"
    aria-hidden="true"
    aria-labelledby="recipeModal"
    tabindex="-1"
  >
    <div class="modal-dialog modal-dialog-centered">
      <div class="modal-content">
        <div class="modal-header bg-primary">
          <h5 class="modal-title" id="exampleModalToggleLabel">
            Create A Recipe
          </h5>
          <button
            type="button"
            class="btn-close"
            data-bs-dismiss="modal"
            aria-label="Close"
          ></button>
        </div>
        <form @submit.prevent="createRecipe()">
          <div class="modal-body">
            <div class="row">
              <div class="col m-2">
                <input
                  type="text"
                  aria-label="name"
                  placeholder="Name your recipe.."
                  class="form-control"
                  v-model="state.editable.title"
                  required
                />
              </div>
            </div>
            <div class="row">
              <div class="col m-2">
                <input
                  type="text"
                  aria-label="Category.."
                  placeholder="Category.."
                  class="form-control"
                  v-model="state.editable.category"
                  required
                />
              </div>
            </div>
            <div class="row">
              <div class="col m-2">
                <input
                  type="text"
                  aria-label="Subtitle"
                  placeholder="Brief desc.."
                  class="form-control"
                  v-model="state.editable.subTitle"
                  required
                />
              </div>
            </div>
            <div class="row">
              <div class="col m-2">
                <input
                  type="text"
                  aria-label="ImgUrl"
                  placeholder="Image Url.."
                  class="form-control"
                  v-model="state.editable.imgUrl"
                  required
                />
              </div>
            </div>
          </div>

          <div class="modal-footer">
            <button
              type="submit"
              class="btn btn-outline-primary"
              data-bs-dismiss="modal"
            >
              Create
            </button>
          </div>
        </form>
      </div>
    </div>
  </div>
</template>


<script>
import { reactive } from "@vue/reactivity";
import { logger } from "../utils/Logger";
import Pop from "../utils/Pop";
import { recipesService } from "../services/RecipesService";

export default {
  setup() {
    const state = reactive({
      editable: {},
    });
    return {
      state,

      async createRecipe() {
        try {
          await recipesService.createRecipe(state.editable)
          state.editable = {}
        } catch (error) {
          logger.error(error)
          Pop.toast("Something went wrong creating recipe!", 'error')
        }
      }
    }
  }
}
</script>


<style lang="scss" scoped>
</style>