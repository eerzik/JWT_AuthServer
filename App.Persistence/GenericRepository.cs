using App.Application.Contracts.Persistence;
using App.Domain.Entities.Common;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace App.Persistence;

public class GenericRepository<T, TId>(AppDbContext context) : IGenericRepository<T, TId> where T : BaseEntity<TId> where TId : struct
{
    //Sadece miras alınan sınıflarda kullanılsın diye prodected
    protected AppDbContext Context = context;


    private readonly DbSet<T> _dbset = context.Set<T>();

    public Task<bool> AnyAsync(TId id) => _dbset.AnyAsync(x => x.Id.Equals(id));
    public async ValueTask AddAsync(T entity) => await _dbset.AddAsync(entity);

    public void Delete(T entity) => _dbset.Remove(entity);

    public ValueTask<T?> GetByIdAsync(int id) => _dbset.FindAsync(id);

    public void Update(T entity) => _dbset.Update(entity);


    public IQueryable<T> Where(Expression<Func<T, bool>> predicate) => _dbset.Where(predicate).AsQueryable().AsNoTracking();

    public Task<bool> AnyAsync(Expression<Func<T, bool>> predicate) => _dbset.AnyAsync(predicate);


    public Task<List<T>> GetAllAsync() => _dbset.ToListAsync();

    public Task<List<T>> GetAllPagedAsync(int pageNumber, int pageSize) => _dbset.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
}
