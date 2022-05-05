using System.Linq.Expressions;

namespace PsychoShop.Framework.Domain
{
    public interface IRepositoryBase<TKey, T> where T : class
    {
        List<T> GetAll();
        T Get(TKey id);
        void Create(T entity);
        void SaveChanges();
        bool Exists(Expression<Func<T, bool>> expression);
    }
}
