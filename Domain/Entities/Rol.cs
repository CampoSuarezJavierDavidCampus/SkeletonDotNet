using Domain.Entities.Generics;

namespace Domain.Entities;
public class Rol: BaseEntityWithIntId{
    public string Description { get; set; } = null!;

    public IEnumerable<User> Users { get; set; } = new HashSet<User>();
    public IEnumerable<RolUser> RolUsers { get; set; } = new HashSet<RolUser>();
}
