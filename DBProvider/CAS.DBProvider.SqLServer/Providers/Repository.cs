using Microsoft.EntityFrameworkCore;
using CAS.DBProvider.SqLServer.Configuration;
using System.Linq.Expressions;

namespace CAS.DBProvider.SqLServer.Providers
{
	public class Repository<T> : IRepository<T> where T : class
    {
        #region Fields
        private readonly IDbContext _context;
        private DbSet<T> _entities;
        #endregion

        #region Ctor
        public Repository(IDbContext context)
        {
            _context = context;
            _entities = context.Set<T>();
        }
        #endregion

        public IQueryable<T> Table => Entities;

        public IQueryable<T> TableNoTracking => Entities.AsNoTracking();

        #region Method
        public void Delete(T entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            Entities.Remove(entity);
            _context.SaveChanges();
        }

        public void Delete(IEnumerable<T> entities)
        {
            if (entities == null) throw new ArgumentNullException(nameof(_entities));
            Entities.RemoveRange(entities);
            _context.SaveChanges();
        }

        public async ValueTask DeleteAsync(T entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            Entities.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(IEnumerable<T> entities)
        {
            if (entities == null) throw new ArgumentNullException(nameof(_entities));
            Entities.RemoveRange(entities);
            await _context.SaveChangesAsync();
        }

        public T GetById(object Id)
        {
            return Entities.ToList().FirstOrDefault(c => c.GetType().GetProperty("Id").GetValue(c).Equals(Id));
        }

        public async ValueTask<T> GetByIdAsync(object Id)
        {
            return await Task.FromResult(Entities.ToList().FirstOrDefault(c => c.GetType().GetProperty("Id").GetValue(c).Equals(Id)));
        }

        public IEnumerable<T> GetSql(string sql)
        {
            return Entities.FromSqlRaw(sql);
        }

        public IQueryable<T> IncludeMany(params Expression<Func<T, object>>[] includes)
        {
            return _entities.IncludeMultiple(includes);
        }

        public void Insert(T entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            _entities.Add(entity);
            _context.SaveChanges();
        }

        public void Insert(IEnumerable<T> entities)
        {
            if (entities == null) throw new ArgumentNullException(nameof(entities));
            _entities.AddRange(entities);
            _context.SaveChanges();
        }

        public async ValueTask InsertAsync(T entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            await _entities.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task InsertAsync(IEnumerable<T> entities)
        {
            if (entities == null) throw new ArgumentNullException(nameof(entities));
            await _entities.AddRangeAsync(entities);
            await _context.SaveChangesAsync();
        }

        public void Update(T entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            _entities.Update(entity);
            _context.SaveChanges();
        }

        public void Update(IEnumerable<T> entities)
        {
            if (entities == null) throw new ArgumentNullException(nameof(entities));
            _entities.UpdateRange(entities);
            _context.SaveChanges();
        }

        public async ValueTask UpdateAsync(T entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            _entities.AsNoTracking();
            _entities.Update(entity);

            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(IEnumerable<T> entities)
        {
            if (entities == null) throw new ArgumentNullException(nameof(entities));
            _entities.UpdateRange(entities);
            await _context.SaveChangesAsync();
        }

        #endregion
        protected virtual DbSet<T> Entities => _entities ?? (_entities = _context.Set<T>());
    }
}
