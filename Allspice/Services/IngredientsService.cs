using System;
using System.Collections.Generic;
using Allspice.Models;
using Allspice.Repositories;

namespace Allspice.Services
{
  public class IngredientsService
  {
    private readonly IngredientsRepository _repo;
    public IngredientsService(IngredientsRepository repo)
    {
      _repo = repo;
    }
    internal List<Ingredient> Get()
    {
      return _repo.Get();
    }
    internal List<Ingredient> GetByRecipeId(int id)
    {
      return _repo.GetByRecipeId(id);
    }
    internal Ingredient GetById(int id)
    {
      Ingredient found = _repo.GetById(id);
      if (found == null)
      {
        throw new Exception("Invalid Id!");
      }
      return found;
    }
    internal Ingredient Create(Ingredient newIngredient)
    {
      return _repo.Create(newIngredient);
    }
    internal Ingredient Edit(Ingredient editedIngredient, string userId)
    {
      Ingredient oldIngredient = GetById(editedIngredient.Id);
      if (oldIngredient.CreatorId != userId)
      {
        throw new Exception("You cannot edit this Ingredient!");
      }
      editedIngredient.Name = editedIngredient.Name != null ? editedIngredient.Name : oldIngredient.Name;
      editedIngredient.Quantity = editedIngredient.Quantity != null ? editedIngredient.Quantity : oldIngredient.Quantity;
      editedIngredient.RecipeId = editedIngredient.RecipeId != 0 ? editedIngredient.RecipeId : oldIngredient.RecipeId;
      editedIngredient.CreatorId = editedIngredient.CreatorId != null ? editedIngredient.CreatorId : oldIngredient.CreatorId;
      return _repo.Edit(editedIngredient);
    }
    internal void Remove(int id, string userId)
    {
      Ingredient ingredient = GetById(id);
      if (ingredient.CreatorId != userId)
      {
        throw new Exception("You cannot Delete this Ingredient!");
      }
      _repo.Remove(id);
    }
  }
}