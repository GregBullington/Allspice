using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Allspice.Models;
using Dapper;

namespace Allspice.Repositories
{
  public class StepsRepository
  {
    private readonly IDbConnection _db;
    public StepsRepository(IDbConnection db)
    {
      _db = db;
    }
    internal List<Step> Get()
    {
      string sql = "SELECT * FROM steps;";
      return _db.Query<Step>(sql).ToList();
    }
    internal Step GetById(int id)
    {
      string sql = "SELECT * FROM steps WHERE id = @id;";
      return _db.QueryFirstOrDefault<Step>(sql, new { id });
    }
    internal List<Step> GetByRecipeId(int id)
    {
      string sql = "SELECT * FROM steps WHERE recipeId = @id;";
      return _db.Query<Step>(sql, new { id }).ToList();
    }
    internal Step Create(Step newStep)
    {
      string sql = @"INSERT INTO steps 
      (iOrder, body, recipeId, creatorId)
      VALUES (@IOrder, @Body, @RecipeId, @CreatorId)
      ;SELECT LAST_INSERT_ID();";
      int id = _db.ExecuteScalar<int>(sql, newStep);
      newStep.Id = id;
      return newStep;
    }
    internal Step Edit(Step editedStep)
    {
      string sql = @"UPDATE steps
      SET iOrder = @IOrder, body = @Body, recipeId = @RecipeId
      WHERE id = @Id;";
      int rows = _db.Execute(sql, editedStep);
      if (rows <= 0)
      {
        throw new Exception("REPO Step Edit was not successful!");
      }
      return editedStep;
    }
    internal void Remove(int id)
    {
      string sql = "DELETE FROM steps WHERE id = @id LIMIT 1;";
      _db.Execute(sql, new { id });
    }
  }
}