using Api.Controllers;
using Api.Dtos;
using Api.Helpers.Paginator;
using AutoMapper;
using Domain.Entities;
using Domain.Interface;
using Domain.Interface.Pagination;
using Microsoft.AspNetCore.Mvc;

namespace ApiIncidencias.Controllers;
[ApiVersion("1.0")]
[ApiVersion("1.1")]
public class RolController : BaseApiController{
    private readonly IUnitOfWork _UnitOfWork;
    private readonly IMapper _Mapper;

    public RolController (IUnitOfWork unitOfWork,IMapper mapper){
        _UnitOfWork = unitOfWork;
        _Mapper = mapper;
    }

    [HttpGet]
    //[Authorize]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IEnumerable<RolDto>> Get(){
       var records = await _UnitOfWork.Rols.Find();
       return _Mapper.Map<List<RolDto>>(records);
    }

    [HttpGet]
    [MapToApiVersion("1.1")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Pager<RolXUserDtos>>> Get11([FromQuery] PageParam param){
        IPager<Rol> pager = await _UnitOfWork.Rols.Find(param);
        pager.Records = (IEnumerable<Rol>)_Mapper.Map<List<RolXUserDtos>>(pager.Records);        
        return CreatedAtAction("users",pager);
    }

    [HttpGet("{id}")]
    //[Authorize]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<RolXUserDtos>> Get(int id){
       var record = await _UnitOfWork.Rols.FindByIntId(id);
       if (record == null){
           return NotFound();
       }
       return _Mapper.Map<RolXUserDtos>(record);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<RolDto>> Post(RolDto recordDto){
       var record = _Mapper.Map<Rol>(recordDto);
       _UnitOfWork.Rols.Add(record);
       await _UnitOfWork.SaveChanges();
       if (record == null){
           return BadRequest();
       }
       return CreatedAtAction(nameof(Post),new {id= record.IdPk, recordDto});
    }

    [HttpPut]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<RolDtoWithId>> Put([FromBody]RolDtoWithId? recordDto){
       if(recordDto == null)
           return NotFound();
       var record = _Mapper.Map<Rol>(recordDto);
       _UnitOfWork.Rols.Update(record);
       await _UnitOfWork.SaveChanges();
       return recordDto;
    }

    [HttpDelete("{id}")]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id){
       var record = await _UnitOfWork.Rols.FindByIntId(id);
       if(record == null){
           return NotFound();
       }
       _UnitOfWork.Rols.Remove(record);
       await _UnitOfWork.SaveChanges();
       return NoContent();
    }
}