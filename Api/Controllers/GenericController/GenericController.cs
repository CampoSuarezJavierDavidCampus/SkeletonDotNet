using AutoMapper;
using Domain.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;
[ApiVersion("1.0")]
public abstract class GenericController{
    protected readonly IUnitOfWork _UnitOfWork=null!;
    protected readonly IMapper _Mapper = null!;

    [HttpGet]
    [Authorize]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    abstract public Task<ActionResult<IEnumerable<EntityDto>>> Get<EntityDto>();

    [HttpPost]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    abstract public Task<ActionResult<EntityDto>> Post<EntityDto>(EntityDto Entity);    

    [HttpDelete("{id}")]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    abstract public Task<IActionResult> Delete(string id);
}