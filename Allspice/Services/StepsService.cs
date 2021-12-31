using System;
using System.Collections.Generic;
using Allspice.Models;
using Allspice.Repositories;

namespace Allspice.Services
{
  public class StepsService
  {
    private readonly StepsRepository _repo;
    public StepsService(StepsRepository repo)
    {
      _repo = repo;
    }
    internal List<Step> Get()
    {
      return _repo.Get();
    }
    internal List<Step> GetByRecipeId(int id)
    {
      return _repo.GetByRecipeId(id);
    }
    internal Step GetById(int id)
    {
      Step found = _repo.GetById(id);
      if (found == null)
      {
        throw new Exception("Invalid Id!");
      }
      return found;
    }
    internal Step Create(Step newStep)
    {
      return _repo.Create(newStep);
    }
    internal Step Edit(Step editedStep, string userId)
    {
      Step oldStep = GetById(editedStep.Id);
      if (oldStep.CreatorId != userId)
      {
        throw new Exception("You cannot edit this Step!");
      }
      editedStep.IOrder = editedStep.IOrder != 0 ? editedStep.IOrder : oldStep.IOrder;
      editedStep.Body = editedStep.Body != null ? editedStep.Body : oldStep.Body;
      editedStep.RecipeId = editedStep.RecipeId != 0 ? editedStep.RecipeId : oldStep.RecipeId;
      editedStep.CreatorId = editedStep.CreatorId != null ? editedStep.CreatorId : oldStep.CreatorId;
      return _repo.Edit(editedStep);
    }
    internal void Remove(int id, string userId)
    {
      Step step = GetById(id);
      if (step.CreatorId != userId)
      {
        throw new Exception("You cannot Delete this Step!");
      }
      _repo.Remove(id);
    }
  }
}