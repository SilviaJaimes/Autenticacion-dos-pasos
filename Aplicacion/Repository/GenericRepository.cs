using System.Linq.Expressions;
using Dominio.Entities;
using Dominio.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistencia;

namespace Aplicacion.Repository;

public class GenericRepository<T> : IGenericRepo<T> where T : BaseEntity
{
    private readonly ApiContext _context;
    protected readonly DbSet<T> _Entity;

    public GenericRepository(ApiContext context)
    {
        _context = context;
    }

    public async virtual Task<T> FindFirst(Expression<Func<T, bool>> expression)
{   
                Console.WriteLine("entroalmetodoe");

    if (expression != null)
    {
                        Console.WriteLine(expression);

        var res = await _Entity.FirstOrDefaultAsync(expression);
        if (res != null)
        {
            Console.WriteLine("entroalmetodoe");
            return res;
            
        }
    }

    // Cambia este valor predeterminado según tu lógica de negocio.
    // Para tipos de referencia (objetos), el valor predeterminado es null.
    // Para tipos de valor (como int), el valor predeterminado es 0.
    return default(T);
}


    public virtual void Add(T entity)
    {
        _context.Set<T>().Add(entity);
    }

    public virtual void AddRange(IEnumerable<T> entities)
    {
        _context.Set<T>().AddRange(entities);
    }

    public virtual IEnumerable<T> Find(Expression<Func<T, bool>> expression)
    {
        return _context.Set<T>().Where(expression);
    }

    public virtual async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _context.Set<T>().ToListAsync();
    }

    public virtual async Task<T> GetByIdAsync(int id)
    {
        return await _context.Set<T>().FindAsync(id);
    }

    public virtual Task<T> GetByIdAsync(string id)
    {
        throw new NotImplementedException();
    }

    public virtual void Remove(T entity)
    {
        _context.Set<T>().Remove(entity);
    }

    public virtual void RemoveRange(IEnumerable<T> entities)
    {
        _context.Set<T>().RemoveRange(entities);
    }

    public virtual void Update(T entity)
    {
        _context.Set<T>()
            .Update(entity);
    }
}