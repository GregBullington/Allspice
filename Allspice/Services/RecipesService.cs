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

    internal Recipe Get(int id)
    {
      Recipe found = _repo.Get(id);
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
    internal Recipe Edit(Recipe editedRecipe)
    {
      Recipe oldRecipe = Get(editedRecipe.Id);
      editedRecipe.Title = editedRecipe.Title != null ? editedRecipe.Title : oldRecipe.Title;
      editedRecipe.Subtitle = editedRecipe.Subtitle != null ? editedRecipe.Subtitle : oldRecipe.Subtitle;
      editedRecipe.Category = editedRecipe.Category != null ? editedRecipe.Category : oldRecipe.Category;
      editedRecipe.CreatorId = editedRecipe.CreatorId != null ? editedRecipe.CreatorId : oldRecipe.CreatorId;
      return _repo.Edit(editedRecipe);
    }
    internal void Remove(int id)
    {
      Recipe recipe = Get(id);
      _repo.Remove(id);
    }
  }
}