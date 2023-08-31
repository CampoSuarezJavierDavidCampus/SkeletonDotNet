using Application.Repositories;
using Domain.Interface;
using Persistence;

namespace Application.UnitOfWork;
public class UnitOfWork : IUnitOfWork{
    private readonly ApiContext _Context;
    private  IUserRepository? _User;
    private  IRolRepository? _Rol;

    public UnitOfWork(ApiContext Context) => _Context = Context;

    public IUserRepository Users => _User ??= new UserRepository(_Context);

    public IRolRepository Rols =>  _Rol ??= new RolRepository(_Context);
}
