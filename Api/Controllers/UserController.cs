using Api.Dtos;
using AutoMapper;
using Domain.Entities;
using Domain.Interface;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;
[ApiVersion("1.0")]
public class UserController : BaseApiController{
    private readonly IUnitOfWork _UnitOfWork;
    private readonly IMapper _Mapper;

    public UserController(IUnitOfWork unitOfWork,IMapper mapper)
    {
        _UnitOfWork = unitOfWork;
        _Mapper = mapper;
    }

    [HttpGet]
    //[Authorize]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IEnumerable<UserDto>> Get()
    {                
        var users = await _UnitOfWork.Users.Find();
        return _Mapper.Map<List<UserDto>>(users);    
    }
    
    [HttpGet("{id}")]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<UserXRolDto>> Get(int id)
    {
        var user = await _UnitOfWork.Users.Find(x => x.IdPk == id);
        if (user == null){
            return NotFound();
        }
        return _Mapper.Map<UserXRolDto>(user);
    }

    [HttpPut]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<UserDtoWithId>> Put([FromBody]UserDtoWithId? userDto){
        if(userDto == null)
            return NotFound();
        var users = _Mapper.Map<User>(userDto);
        _UnitOfWork.Users.Update(users);
        await _UnitOfWork.SaveChanges();
        return userDto;
    }

    [HttpDelete("{id}")]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id){
        var user = await _UnitOfWork.Users.FindByIntId(id);
        if(user == null){
            return NotFound();
        }
        _UnitOfWork.Users.Remove(user);
        await _UnitOfWork.SaveChanges();
        return NoContent();
    }
}
