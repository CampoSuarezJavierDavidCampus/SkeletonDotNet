namespace Api.Dtos;
public class RolXUsers{
    public string Description { get; set; } = null!;
    public HashSet<UserDto> Users { get; set; } = new HashSet<UserDto>();
}
