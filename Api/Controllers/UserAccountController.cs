using Api.Dtos;
using Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

public class UserAccountController: BaseApiController{
    private readonly IUserServices _UserServices;
    public UserAccountController(IUserServices userServices) => _UserServices = userServices;

    [HttpPost("register")]
    public async Task<ActionResult> RegisterAsync(SingUpDto model) => Ok(await _UserServices.RegisterAsync(model));

    [HttpPost("Token")]    
    public async Task<ActionResult> GetTokenAsync(LogginDto model) => Ok(await _UserServices.GetTokenAsync(model));

    [HttpPost("addrol")]
    public async Task<ActionResult> AddRolAsync(AddRolDto model) => Ok(await _UserServices.AddRolAsync(model));
    
}
