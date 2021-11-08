using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ProjectManagement.Common.Enums;
using ProjectManagement.Common.Helpers;
using ProjectManagement.Common.Models;
using ProjectManagement.Infrastructure.Data;
using ProjectManagement.Infrastructure.Repositories.Interfaces;
using ProjectManagement.Infrastructure.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagement.Infrastructure.Repositories
{
    public class DeveloperRepository : GenericRepository<Developer>, IDeveloperRepository
    {
        public DeveloperRepository(ApplicationDbContext db, ILogger logger, Func<CacheTech, ICacheService> cacheService) : base(db, logger, cacheService)
        {

        }

        public override async Task<IEnumerable<Developer>> All()
        {
            try
            {
                if (!_cacheService(cacheTech).TryGet(cacheKey, out IEnumerable<Developer> cachedList))
                {
                    cachedList = await _dbSet.ToListAsync();
                    _cacheService(cacheTech).Set(cacheKey, cachedList);
                }
                return cachedList;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} All function error", typeof(ProjectRepository));
                return new List<Developer>();
            }
        }

        public override async Task<PagedList<Developer>> Find(UserParams userParams)
        {
            try
            {
                var query = _dbSet.AsQueryable();
                if(userParams.DeveloperType != null)
                    query = query.Where(d => d.DeveloperType == userParams.DeveloperType);
                query = query.Where(d => d.YearsOfExperience >= userParams.YearsOfExperience);

                return await PagedList<Developer>.CreateAsync(query.AsNoTracking(), userParams.PageNumber, userParams.PageSize);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} All function error", typeof(DeveloperRepository));
                return null;
            }
        }

        public override async Task<Developer> Upsert(Developer entity)
        {
            try
            {
                var existingDeveloper = await _dbSet.Where(x => x.Id == entity.Id)
                                                    .FirstOrDefaultAsync();

                if (existingDeveloper == null)
                    return await Add(entity);


                _db.Entry(entity).State = EntityState.Modified;
                return existingDeveloper;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} Upsert function error", typeof(DeveloperRepository));
                return null;
            }
        }

        public override async Task<bool> Delete(int id)
        {
            try
            {
                var existingDeveloper = await _dbSet.Where(x => x.Id == id)
                                        .FirstOrDefaultAsync();

                if (existingDeveloper == null) return false;

                _dbSet.Remove(existingDeveloper);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} Delete function error", typeof(DeveloperRepository));
                return false;
            }
        }

        public override async Task RefreshCache()
        {
            _cacheService(cacheTech).Remove(cacheKey);
            var cachedList = await _dbSet.ToListAsync();
            _cacheService(cacheTech).Set(cacheKey, cachedList);
        }
    }
}
