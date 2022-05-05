using Microsoft.EntityFrameworkCore;
using PsychoShop.Framework.Domain;
using System.Linq.Expressions;

namespace PsychoShop.Framework.Infrastructure
{
    public class RepositoryBase<TKey, T> : IRepositoryBase<TKey, T> where T : class
    {
        private readonly DbContext _context;

        public RepositoryBase(DbContext context)
        {
            _context = context;
        }

        public List<T> GetAll()
        {
            return _context.Set<T>().ToList();
        }

        public T Get(TKey id)
        {
            return _context.Set<T>().Find(id);
        }

        public void Create(T entity)
        {
            _context.Set<T>().Add(entity);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public bool Exists(Expression<Func<T, bool>> expression)
        {
            return _context.Set<T>().Any(expression);
        }
    }
}
