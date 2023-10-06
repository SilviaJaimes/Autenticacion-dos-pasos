using Aplicacion.Repository;
using Dominio.Interfaces;
using Persistencia;

namespace Aplicacion.UnitOfWork;

public class UnitOfWork : IUnitOfWork, IDisposable
{
    private readonly ApiContext context;
    private IUser _users;

    public UnitOfWork(ApiContext _context)
    {
        context = _context;
    }
    public IUser Users
    {
        get
        {
            if(_users == null){
                _users = new UserRepository(context);
            }
            return _users;
        }
    }

    public void Dispose()
    {
        context.Dispose();
    }
    public async Task<int> SaveAsync()
    {
        return await context.SaveChangesAsync();
    }
}