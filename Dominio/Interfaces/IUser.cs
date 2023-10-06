using Dominio.Entities;

namespace Dominio.Interfaces;

public interface IUser : IGenericRepo<User>
{
    Task<User> GetByUsernameAsync(string username);
    
}