<template>
  <div class="div container-fluid">
    <div class="row">
      <div class="col">
        <!-- Hero Image -->
        <div class="card elevation-4 mt-2 hero-image">
          <div class="row justify-content-end align-items-center">
            <div class="col-md-5">
              <div class="search">
                <div class="m-3">
                  <form @submit.prevent="searchRecipes">
                    <div class="input-group">
                      <input
                        v-model="search"
                        type="text"
                        class="form-control"
                        placeholder="Search Recipes.."
                        aria-label="Search Recipes"
                        aria-describedby="button-addon2"
                      />
                      <button
                        class="btn btn-outline-light"
                        type="submit"
                        id="button-addon2"
                      >
                        Search
                      </button>
                    </div>
                  </form>
                </div>
              </div>
            </div>
          </div>
          <div class="row">
            <div class="col hero-text">
              <h1>All-Spice</h1>
              <h4>Cherish Your Family</h4>
              <h4>And Their Cooking</h4>
            </div>
          </div>
        </div>
        <button
          @click="login"
          class="site-font floating-btn-left fs-6"
          v-if="!user.isAuthenticated"
        >
          Login
        </button>
        <button
          class="mdi mdi-account floating-btn-left"
          data-bs-toggle="offcanvas"
          data-bs-target="#offcanvasRight"
          aria-controls="offcanvasRight"
          v-else
        ></button>
        <button class="mdi mdi-plus floating-btn-right"></button>

        <!-- End Hero -->
        <div class="row justify-content-center px-5">
          <div class="card bg-light elevation-3 col-lg-6 mb-2 nav-card">
            <div class="d-inline-flex p-2 justify-content-between site-font">
              <li style="list-style-type: none">
                <a class="btn selectable">Home</a>
              </li>
              <li style="list-style-type: none">
                <a class="btn selectable">My Recipes</a>
              </li>
              <li style="list-style-type: none">
                <a class="btn selectable">Favorites</a>
              </li>
            </div>
          </div>
        </div>
      </div>
    </div>
    <div class="row justify-content-around">
      <div v-for="r in recipes" :key="r.id" class="col-md-4">
        <RecipesComponent :recipe="r" />
      </div>
    </div>
  </div>
  <AccountOffCanvas />
</template>

<script>
import { computed, ref } from "@vue/reactivity"
import { AppState } from "../AppState"
import { onMounted } from "@vue/runtime-core"
import Pop from "../utils/Pop"
import { recipesService } from "../services/RecipesService"
import { logger } from "../utils/Logger"
import { AuthService } from "../services/AuthService"
export default {
  name: 'Home',
  setup() {
    const search = ref('')
    onMounted(async () => {
      try {
        await recipesService.getAllRecipes('api/recipes')
      } catch (error) {
        Pop.toast("Something went wrong!", 'error')
      }
    })
    return {
      account: computed(() => AppState.account),
      user: computed(() => AppState.user),
      recipes: computed(() => AppState.recipes),
      search,

      async login() {
        AuthService.loginWithPopup()
      },

      async searchRecipes() {
        try {
          logger.log("searching")
          await recipesService.getAllRecipes('api/recipes/search?q=' + search.value)
          search.value = ''
        } catch (error) {
          logger.error(error)
        }
      },

    }
  }
}
</script>

<style scoped lang="scss">
.hero-image {
  background-image: linear-gradient(rgba(0, 0, 0, 0.5), rgba(0, 0, 0, 0.5)),
    url("https://images.squarespace-cdn.com/content/v1/54be40d1e4b07f864edf6a24/1540323346079-GS0YA24GUZ6RMY0TRQAC/Teal+Spice+Background.jpg?format=2500w");
  height: 250px;
  background-position: center;
  background-repeat: no-repeat;
  background-size: cover;
  position: relative;
}
.nav-card {
  transform: translateY(-50%);
}

.home {
  display: grid;
  height: 80vh;
  place-content: center;
  text-align: center;
  user-select: none;
  .home-card {
    width: 50vw;
    > img {
      height: 200px;
      max-width: 200px;
      width: 100%;
      object-fit: contain;
      object-position: center;
    }
  }
}
</style>
