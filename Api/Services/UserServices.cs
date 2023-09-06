using Api.Dtos;
using Application.Contratos;
using Application.Helpers.Security;
using Application.Services;
using Domain.Entities;
using Domain.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace Api.Services;
public class UserServices : IUserServices
{
    private readonly IUnitOfWork _UnitOfWork;
    private readonly IPasswordHasher<User> _PasswordHasher;
    private readonly IJwtGenerator _JwtGenerator;
    private readonly JWT _Jwt;    

    public UserServices(
        IUnitOfWork UnitOfWork,
        IPasswordHasher<User> PasswordHasher,
        IJwtGenerator JwtGenerator,
        IOptions<JWT> Jwt
    )
    {
        _Jwt = Jwt.Value;
        _UnitOfWork = UnitOfWork;
        _PasswordHasher = PasswordHasher;
        _JwtGenerator = JwtGenerator;        
    }


    public async Task<string> AddRolAsync(AddRolDto model){
        User? user = await _UnitOfWork.Users.FindUserByUsername(model.Username);
        if (user == null){
            return $"No existe algún usuario registrado con la cuenta {model.Username}.";
        }else if(!ValidatePassword(user,model.Password)){
            return $"Credenciales incorrectas para el usuario {model.Username}.";
        }
        var existingRol = await _UnitOfWork.Rols.FindFirst(x => x.Description == model.Rol);
        if (existingRol == null){
             return $"Rol {model.Rol} agregado a la cuenta {model.Username} de forma exitosa.";
        }
        
        var userHasRol = user.Rols?.Any(x => x.IdPk == existingRol.IdPk);
        if (userHasRol == false)
        {
            user.Rols?.Add(existingRol);
            _UnitOfWork.Users.Update(user);
            await _UnitOfWork.SaveChanges();
        }
        return $"Rol {model.Rol} agregado a la cuenta {model.Username} de forma exitosa.";
        
        
    }

    public async Task<UserDataDto> GetTokenAsync(LogginDto model){
        UserDataDto userData = new(){
            IsAuthenticated = false,            
        };
        var user = await _UnitOfWork.Users.FindUserByUsername(model.Username);
        if (user == null){
            userData.Message = $"No existe ningún usuario con el username {model.Username}.";
        }else if(!ValidatePassword(user, model.Password)){
            userData.Message =  $"Credenciales incorrectas para el usuario {model.Username}.";;
        }else{
            userData.Message = "Ok";
            userData.IsAuthenticated = true;
            userData.Username = user.Usename;
            userData.Email = user.Email;
            userData.Token = _JwtGenerator.CreateToken(user);
        }
        return userData;

    }

    public async Task<string> RegisterAsync(SingUpDto model)
    {
        var user = CreateUser(model);

        var existingUser = _UnitOfWork.Users.FindUserByUsername(model.Username);
        if (existingUser == null){
            return $"El usuario {model.Username} ya se encuentra registrado.";
        }
        
        var defaultRol =  (await _UnitOfWork.Rols.FindByRol( Authorization.Default_role ))!;
        
        try{
            user.Rols?.Add(defaultRol);
            _UnitOfWork.Users.Add(user);
            await _UnitOfWork.SaveChanges();
            return $"El usuario  {model.Username} ha sido registrado exitosamente";
        }catch(Exception ex){
            return $"Error: {ex.Message}";
        }
        
    }

    public async Task<LogginDto?> UserLoggin(LogginDto model){
        User? user = await _UnitOfWork.Users.FindUserByUsername(model.Username);        
        if(user != null && ValidatePassword(user, model.Password)){
            return model;
        }
        return null;
    }

    /*! pendiente realizar esta validacion*/
    /*public async Task<DatosUsuarioDto> GetTokenAsync(LoginDto model)
    {
        DatosUsuarioDto datosUsuarioDto = new DatosUsuarioDto();
        var usuario = await _unitOfWork.Usuarios
                    .GetByUsernameAsync(model.Username);

        if (usuario == null)
        {
            datosUsuarioDto.EstaAutenticado = false;
            datosUsuarioDto.Mensaje = $"No existe ningún usuario con el username {model.Username}.";
            return datosUsuarioDto;
        }

        var resultado = _passwordHasher.VerifyHashedPassword(usuario, usuario.Password, model.Password);

        if (resultado == PasswordVerificationResult.Success)
        {
            datosUsuarioDto.EstaAutenticado = true;
            JwtSecurityToken jwtSecurityToken = CreateJwtToken(usuario);
            datosUsuarioDto.Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
            datosUsuarioDto.Email = usuario.Email;
            datosUsuarioDto.UserName = usuario.Username;
            datosUsuarioDto.Roles = usuario.Roles
                                            .Select(u => u.Nombre)
                                            .ToList();
            return datosUsuarioDto;
        }
        datosUsuarioDto.EstaAutenticado = false;
        datosUsuarioDto.Mensaje = $"Credenciales incorrectas para el usuario {usuario.Username}.";
        return datosUsuarioDto;
    }*/

    private User CreateUser(SingUpDto model){
        User user = new(){
            Email = model.Email,
            Usename = model.Username
        };
        user.Password = _PasswordHasher.HashPassword(user,model.Password);
        return user;
    }    

    private bool ValidatePassword(User user, string password){
        return _PasswordHasher.VerifyHashedPassword(user, user.Password, password) == PasswordVerificationResult.Success;
    }
}