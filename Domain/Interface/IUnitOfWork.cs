namespace Domain.Interface;
public interface IUnitOfWork{
    IUserRepository Users { get; }
    IRolRepository Rols { get; }

    Task<int> SaveChanges();

    void Dispose();
}
