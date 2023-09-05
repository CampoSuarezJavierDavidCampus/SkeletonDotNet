using Domain.Entities.Generics;

namespace Domain.Entities;
public class User:BaseEntityWithIntId{
    public string Usename { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string Email { get; set; } = null!;

    public IEnumerable<Rol> Rols { get; set; } = new HashSet<Rol>();
    public IEnumerable<RolUser> RolUsers { get; set; } = new HashSet<RolUser>();
}
