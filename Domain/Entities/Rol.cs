using Domain.Entities.Generics;

namespace Domain.Entities;
public class Rol: BaseEntityWithIntId{
    public string Description { get; set; } = null!;

    public ICollection<User>? Users { get; set; } = new HashSet<User>();
    public ICollection<RolUser>? RolUsers { get; set; } = new HashSet<RolUser>();
}
