using Application.Repositories;
using Domain.Interface;
using Persistence;

namespace Application.UnitOfWork;
public class UnitOfWork : IUnitOfWork,IDisposable{
    private readonly ApiContext _Context;
    private  IUserRepository? _User;
    private  IRolRepository? _Rol;

    public UnitOfWork(ApiContext Context) => _Context = Context;

    public IUserRepository Users => _User ??= new UserRepository(_Context);

    public IRolRepository Rols =>  _Rol ??= new RolRepository(_Context);
    
    public virtual async Task<int> SaveChanges()=>await _Context.SaveChangesAsync();    

    public virtual void Dispose(){
        _Context.Dispose();
        GC.SuppressFinalize(this); 
    }
}
