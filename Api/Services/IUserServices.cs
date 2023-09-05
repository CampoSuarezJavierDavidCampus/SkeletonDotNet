using Api.Dtos;

namespace Application.Services;
public interface IUserServices{
    Task<string> RegisterAsync(SingUpDto model);
    Task<UserDto> GetTokenAsync(LogginDto model);
    Task<string> AddRolAsync(AddRolDto model);

}
