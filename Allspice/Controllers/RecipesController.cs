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
  public class RecipesController : ControllerBase
  {
    private readonly RecipesService _rs;

    public RecipesController(RecipesService rs)
    {
      _rs = rs;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Recipe>> Get()
    {
      try
      {
        var recipes = _rs.Get();
        return Ok(recipes);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }
    [HttpGet("{id}")]

    public ActionResult<Recipe> Get(int id)
    {
      try
      {
        var recipe = _rs.Get(id);
        return Ok(recipe);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }
    [HttpPost]
    [Authorize]

    public async Task<ActionResult<Recipe>> Create([FromBody] Recipe newRecipe)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        newRecipe.CreatorId = userInfo?.Id;
        Recipe recipe = _rs.Create(newRecipe);
        return Ok(recipe);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }
    [HttpPut("{id}")]
    public ActionResult<Recipe> Edit([FromBody] Recipe editedRecipe, int id)
    {
      try
      {
        editedRecipe.Id = id;
        Recipe recipe = _rs.Edit(editedRecipe);
        return Ok(editedRecipe);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }
    [HttpDelete]
    public ActionResult<string> Remove(int id)
    {
      try
      {
        _rs.Remove(id);
        return Ok("Recipe has been Deleted!");
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

  }
}