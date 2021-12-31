using System;
using System.Collections.Generic;
using Allspice.Models;
using Allspice.Repositories;

namespace Allspice.Services
{
  public class RecipesService
  {
    private readonly RecipesRepository _repo;

    public RecipesService(RecipesRepository repo)
    {
      _repo = repo;
    }

    internal List<Recipe> Get()
    {
      return _repo.Get();
    }

    internal Recipe GetById(int id)
    {
      Recipe found = _repo.GetById(id);
      if (found == null)
      {
        throw new Exception("Invalid Id!");
      }
      return found;
    }
    internal Recipe Create(Recipe newRecipe)
    {
      return _repo.Create(newRecipe);
    }
    internal Recipe Edit(Recipe editedRecipe, string userId)
    {
      Recipe oldRecipe = GetById(editedRecipe.Id);
      if (oldRecipe.CreatorId != userId)
      {
        throw new Exception("You cannot edit this Recipe!");
      }
      editedRecipe.Title = editedRecipe.Title != null ? editedRecipe.Title : oldRecipe.Title;
      editedRecipe.ImgUrl = editedRecipe.ImgUrl != null ? editedRecipe.ImgUrl : oldRecipe.ImgUrl;
      editedRecipe.Subtitle = editedRecipe.Subtitle != null ? editedRecipe.Subtitle : oldRecipe.Subtitle;
      editedRecipe.Category = editedRecipe.Category != null ? editedRecipe.Category : oldRecipe.Category;
      editedRecipe.CreatorId = editedRecipe.CreatorId != null ? editedRecipe.CreatorId : oldRecipe.CreatorId;
      return _repo.Edit(editedRecipe);
    }
    internal void Remove(int id, string userId)
    {
      Recipe recipe = GetById(id);
      if (recipe.CreatorId != userId)
      {
        throw new Exception("You cannot Delete this Recipe!");
      }
      _repo.Remove(id);
    }
  }
}