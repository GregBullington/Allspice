using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Allspice.Models;
using Dapper;

namespace Allspice.Repositories
{
  public class IngredientsRepository
  {
    private readonly IDbConnection _db;
    public IngredientsRepository(IDbConnection db)
    {
      _db = db;
    }
    internal List<Ingredient> Get()
    {
      string sql = "SELECT * FROM ingredients;";
      return _db.Query<Ingredient>(sql).ToList();
    }
    internal Ingredient GetById(int id)
    {
      string sql = "SELECT * FROM ingredients WHERE id = @id;";
      return _db.QueryFirstOrDefault<Ingredient>(sql, new { id });
    }
    internal List<Ingredient> GetByRecipeId(int id)
    {
      string sql = "SELECT * FROM ingredients WHERE recipeId = @id;";
      return _db.Query<Ingredient>(sql, new { id }).ToList();
    }
    internal Ingredient Create(Ingredient newIngredient)
    {
      string sql = @"INSERT INTO ingredients 
      (name, quantity, recipeId, creatorId)
      VALUES (@Name, @Quantity, @RecipeId, @CreatorId)
      ;SELECT LAST_INSERT_ID();";
      int id = _db.ExecuteScalar<int>(sql, newIngredient);
      newIngredient.Id = id;
      return newIngredient;
    }
    internal Ingredient Edit(Ingredient editedIngredient)
    {
      string sql = @"UPDATE ingredients
      SET name = @Name, quantity = @Quantity, recipeId = @RecipeId
      WHERE id = @Id;";
      int rows = _db.Execute(sql, editedIngredient);
      if (rows <= 0)
      {
        throw new Exception("REPO Ingredient Edit was not successful!");
      }
      return editedIngredient;
    }
    internal void Remove(int id)
    {
      string sql = "DELETE FROM ingredients WHERE id = @id LIMIT 1;";
      _db.Execute(sql, new { id });
    }
  }
}