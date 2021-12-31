import { AppState } from "../AppState"
import { logger } from "../utils/Logger"
import { api } from "./AxiosService"


class RecipesService {
  async getAllRecipes(query = '') {
    const res = await api.get(query)
    AppState.recipes = res.data
    logger.log(res.data)
  }
}

export const recipesService = new RecipesService()