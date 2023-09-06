namespace Api.Dtos;
public class UserXRolDto: UserDto{
    public HashSet<string>? Rols { get; set; } = new HashSet<string>();
    
}