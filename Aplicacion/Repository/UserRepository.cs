using Dominio.Entities;
using Dominio.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistencia;

namespace Aplicacion.Repository;

public class UserRepository : GenericRepository<User>, IUser
{
    protected readonly ApiContext _context;

    public UserRepository(ApiContext context) : base (context)
    {
        _context = context;
    }

    public override async Task<IEnumerable<User>> GetAllAsync()
    {
        return await _context.Users
            .ToListAsync();
    }

    public override async Task<User> GetByIdAsync(int id)
    {
        return await _context.Users
        .FirstOrDefaultAsync(p =>  p.Id == id);
    }
    public async Task<User> GetByUsernameAsync(string username)
    {
        return await _context.Users
            .FirstOrDefaultAsync(u => u.Usuario.ToLower() == username.ToLower());
    }
}