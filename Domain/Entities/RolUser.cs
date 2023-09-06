namespace Domain.Entities;
public class RolUser{
    public int UserIdPk { get; set; }
    public User? User { get; set; }

    public int RolIdPk { get; set; }
    public Rol? Rol { get; set; }

}
