using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Allspice.Models;
using Dapper;

namespace Allspice.Repositories
{
  public class RecipesRepository
  {
    private readonly IDbConnection _db;

    public RecipesRepository(IDbConnection db)
    {
      _db = db;
    }

    internal List<Recipe> Get()
    {
      string sql = "SELECT * FROM recipes;";
      return _db.Query<Recipe>(sql).ToList();
    }

    internal Recipe GetById(int id)
    {
      string sql = @"SELECT * FROM recipes WHERE id = @id;";
      return _db.QueryFirstOrDefault<Recipe>(sql, new { id });
    }

    internal Recipe Create(Recipe newRecipe)
    {
      string sql = @"INSERT INTO recipes
      (title, subtitle, category, creatorId)
      VALUE(@title, @subtitle, @category, @creatorId)
      ;SELECT LAST_INSERT_ID();";

      int id = _db.ExecuteScalar<int>(sql, newRecipe);
      newRecipe.Id = id;
      return newRecipe;
    }

    internal Recipe Edit(Recipe editedRecipe)
    {
      string sql = @"UPDATE recipes
      SET title = @Title, subtitle = @Subtitle, category = @Category, creatorId = @CreatorId
      WHERE id = @Id;";
      int rows = _db.Execute(sql, editedRecipe);
      if (rows <= 0)
      {
        throw new Exception("REPO Recipe Edit was not successful!");
      }
      return editedRecipe;
    }
    internal void Remove(int id)
    {
      string sql =
      @"DELETE FROM recipes WHERE id = @Id;";
      int rows = _db.Execute(sql, new { id });
      if (rows <= 0)
      {
        throw new Exception("Invalid Id!");
      }
    }
  }
}