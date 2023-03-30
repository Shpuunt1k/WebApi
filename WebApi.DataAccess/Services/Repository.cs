using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using WebApi.DataAccess.Interfaces;
using WebApi.Entity.AppDbContext;

namespace WebApi.DataAccess.Services;

public class Repository<T> : IRepository<T> where T : class
{
    private readonly ApplicationDbContext _dbContext;
    private readonly DbSet<T> _dbSet;

    public Repository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
        _dbSet = dbContext.Set<T>();
    }

    public T Create(T item)
    {
        var createdItem = _dbSet.Add(item).Entity;
        _dbContext.SaveChanges();
        return createdItem;
    }
    public void CreateRange(List<T> items)
    {
        foreach (var item in items)
        {
            _dbSet.Add(item);
        }
        //_dbSet.AddRange(items);
        _dbContext.SaveChanges();
    }

    public bool Delete(long id)
    {
        var entity = _dbSet.Find(id);
        if (entity == null)
        {
            return false;
        }

        var deletedItem = _dbSet.Remove(entity);
        _dbContext.SaveChanges();

        return deletedItem != null;
    }

    public bool DeleteRange(List<T> items)
    {
        _dbSet.RemoveRange(items);
        var res = _dbContext.SaveChanges();
        return res == items.Count;
    }

    public bool DeleteRange(List<long> ids)
    {

        var items = new List<long>();
        foreach (var item in ids)
        {
            var entity = _dbSet.Find(item);

            if (entity != null)
            {
                items.Add(item);
            }
        }

        _dbSet.RemoveRange((IEnumerable<T>)items);
        var res = _dbContext.SaveChanges();
        return res == items.Count;
    }

    public IQueryable<T> GetAll()
    {
        return _dbSet.AsNoTracking();
    }
    public List<T> Get(int indent, int take)
    {
        return _dbSet.Skip(indent).ToList().Take(take).ToList();
    }

    public T GetById(int id)
    {
        return _dbSet.Find(id);
    }

    public bool Update(T item)
    {
        _dbContext.Entry(item).State = EntityState.Modified;
        return _dbContext.SaveChanges() != 0;
    }

    public List<T> UpdateRange(List<T> items)
    {
        items.ForEach(e =>
        {
            _dbContext.Entry(e).State = EntityState.Modified;
        });
        _dbContext.SaveChanges();
        return items;
    }
    public IQueryable<T> Include(params Expression<Func<T, object>>[] children)
    {
        children.ToList().ForEach(x => _dbSet.Include(x).Load());
        return _dbSet;
    }
}