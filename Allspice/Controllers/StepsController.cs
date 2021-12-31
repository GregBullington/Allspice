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
  public class StepsController : ControllerBase
  {
    private readonly StepsService _ss;
    private readonly AccountService _acctService;
    public StepsController(StepsService ss, AccountService acctService)
    {
      _ss = ss;
      _acctService = acctService;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Step>> Get()
    {
      try
      {
        var steps = _ss.Get();
        return Ok(steps);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpGet("recipe/{recipeId}")]
    public ActionResult<IEnumerable<Step>> GetByRecipeId(int recipeId)
    {
      try
      {
        var steps = _ss.GetByRecipeId(recipeId);
        return Ok(steps);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpGet("{id}")]
    public ActionResult<IEnumerable<Step>> GetById(int id)
    {
      try
      {
        var step = _ss.GetById(id);
        return Ok(step);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpPost]
    [Authorize]
    public async Task<ActionResult<Step>> Create([FromBody] Step newStep)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        newStep.CreatorId = userInfo?.Id;
        Step step = _ss.Create(newStep);
        return Ok(step);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpPut("{id}")]
    [Authorize]
    public async Task<ActionResult<Step>> Edit([FromBody] Step editedStep, int id)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        editedStep.Id = id;
        Step step = _ss.Edit(editedStep, userInfo.Id);
        return Ok(step);
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
        _ss.Remove(id, userInfo.Id);
        return Ok("Step Deleted!");
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }
  }
}