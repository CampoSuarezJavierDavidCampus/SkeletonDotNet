using AutoMapper;
using Domain.Interface;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;
public class UserController : GenericControllerWithIntId
{
    public UserController(IUnitOfWork unitOfWork, IMapper mapper){
        _UnitOfWork = unitOfWork;
        _Mapper = mapper;
    }

    public override Task<IActionResult> Delete(string id)
    {
        throw new NotImplementedException();
    }

    public override Task<ActionResult<EntityDto>> Get<EntityDto>(int id)
    {
        throw new NotImplementedException();
    }

    public override async Task<ActionResult<IEnumerable<UserXRolDto>>> Get<UserXRolDto>(){
        var users = await _UnitOfWork.Users.Find();
        return _Mapper.Map<List<UserXRolDto>>(users);
    }

    public override Task<ActionResult<EntityDto>> Post<EntityDto>(EntityDto Entity)
    {
        throw new NotImplementedException();
    }

    public override Task<ActionResult<EntityDto>> Put<EntityDto>(int id, [FromBody] EntityDto entityDto)
    {
        throw new NotImplementedException();
    }
}
