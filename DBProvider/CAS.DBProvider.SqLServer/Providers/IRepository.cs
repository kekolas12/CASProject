using System.Linq.Expressions;

namespace CAS.DBProvider.SqLServer.Providers
{
	public interface IRepository<T> where T : class
    {
        IQueryable<T> Table { get; }
        IQueryable<T> TableNoTracking { get; }
        T GetById(object Id);
        ValueTask<T> GetByIdAsync(object Id);
        void Insert(T entity);
        void Insert(IEnumerable<T> entities);
        ValueTask InsertAsync(T entity);
        Task InsertAsync(IEnumerable<T> entities);
        ValueTask UpdateAsync(T entity);
        void Update(T entity);
        void Update(IEnumerable<T> entities);
        Task UpdateAsync(IEnumerable<T> entities);
        void Delete(T entity);
        void Delete(IEnumerable<T> entities);
        ValueTask DeleteAsync(T entity);
        Task DeleteAsync(IEnumerable<T> entities);
        IQueryable<T> IncludeMany(params Expression<Func<T, object>>[] includes);
        IEnumerable<T> GetSql(string sql);
    }
}
