import { AppState } from "../AppState"
import { logger } from "../utils/Logger"
import { api } from "./AxiosService"


class RecipesService {
  async getAllRecipes(query = '') {
    const res = await api.get(query)
    AppState.recipes = res.data
    logger.log(res.data)
  }

  async createRecipe(recipe) {
    const res = await api.post('api/recipes', recipe)
    logger.log(res.data)
    AppState.recipes.push(res.data)
  }

  async getActiveSteps(id) {
    const res = await api.get('api/steps/recipe/' + id)
    AppState.activeSteps = res.data
    logger.log(res.data)
  }

  async getActiveIngredients(id) {
    const res = await api.get('api/ingredients/recipe/' + id)
    AppState.activeIngredients = res.data
    logger.log(res.data)
  }
  async createStep(step) {
    const res = await api.post('api/steps', step)
    logger.log(res.data)
    AppState.recipes.push(res.data)
  }

  async createIngredient(ingredient) {
    const res = await api.post('api/ingredients', ingredient)
    logger.log(res.data)
    AppState.ingredients.push(res.data)
  }
}

export const recipesService = new RecipesService()