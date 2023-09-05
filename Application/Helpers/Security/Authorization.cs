namespace Application.Helpers.Security;
public class Authorization{
    public enum Roles{
        Admin,
        Manager,
        Employee
    }
    public const Roles default_role = Roles.Employee;
}
