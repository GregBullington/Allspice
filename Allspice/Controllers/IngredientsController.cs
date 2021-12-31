using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Allspice.Models;
using Allspice.Services;
using CodeWorks.Auth0Provider;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Allspice.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class IngredientsController : ControllerBase
  {
    private readonly IngredientsService _igs;
    private readonly AccountService _acctService;
    public IngredientsController(IngredientsService igs, AccountService acctService)
    {
      _igs = igs;
      _acctService = acctService;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Ingredient>> Get()
    {
      try
      {
        var ingredients = _igs.Get();
        return Ok(ingredients);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpGet("recipe/{recipeId}")]
    public ActionResult<IEnumerable<Ingredient>> GetByRecipeId(int recipeId)
    {
      try
      {
        var ingredients = _igs.GetByRecipeId(recipeId);
        return Ok(ingredients);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpGet("{id}")]
    public ActionResult<IEnumerable<Ingredient>> GetById(int id)
    {
      try
      {
        var ingredient = _igs.GetById(id);
        return Ok(ingredient);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpPost]
    [Authorize]
    public async Task<ActionResult<Ingredient>> Create([FromBody] Ingredient newIngredient)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        newIngredient.CreatorId = userInfo?.Id;
        Ingredient ingredient = _igs.Create(newIngredient);
        return Ok(ingredient);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpPut("{id}")]
    [Authorize]
    public async Task<ActionResult<Ingredient>> Edit([FromBody] Ingredient editedIngredient, int id)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        editedIngredient.Id = id;
        Ingredient ingredient = _igs.Edit(editedIngredient, userInfo.Id);
        return Ok(ingredient);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpDelete("{id}")]
    [Authorize]
    public async Task<ActionResult<string>> Remove(int id)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        _igs.Remove(id, userInfo.Id);
        return Ok("Ingredient Deleted!");
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }
  }
}