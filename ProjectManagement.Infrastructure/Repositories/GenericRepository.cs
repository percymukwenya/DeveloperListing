using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ProjectManagement.Common.Enums;
using ProjectManagement.Common.Helpers;
using ProjectManagement.Infrastructure.Data;
using ProjectManagement.Infrastructure.Repositories.Interfaces;
using ProjectManagement.Infrastructure.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagement.Infrastructure.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly static CacheTech cacheTech = CacheTech.Memory;
        protected readonly string cacheKey = $"{typeof(T)}";
        protected readonly Func<CacheTech, ICacheService> _cacheService;
        protected ApplicationDbContext _db;
        internal DbSet<T> _dbSet;
        protected readonly ILogger _logger;

        public GenericRepository(ApplicationDbContext db, ILogger logger, Func<CacheTech, ICacheService> cacheService)
        {
            _db = db;
            _dbSet = db.Set<T>();
            _logger = logger;
            _cacheService = cacheService;
        }

        public virtual Task<IEnumerable<T>> All()
        {
            throw new NotImplementedException();
        }
        public virtual Task<PagedList<T>> Find(UserParams userParams)
        {
            throw new NotImplementedException();
        }

        public virtual async Task<T> GetById(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public virtual async Task<T> Add(T entity)
        {
            await _dbSet.AddAsync(entity);
            return entity;
        }        

        public virtual Task<bool> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public virtual Task<T> Upsert(T entity)
        {
            throw new NotImplementedException();
        }

        public virtual async Task RefreshCache()
        {
            _cacheService(cacheTech).Remove(cacheKey);
            var cachedList = await _dbSet.ToListAsync();
            _cacheService(cacheTech).Set(cacheKey, cachedList);
        }        
    }
}
