using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ProjectManagement.Common.Models;
using ProjectManagement.Infrastructure.Data;
using ProjectManagement.Infrastructure.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagement.Infrastructure.Repositories
{
    public class DeveloperRepository : GenericRepository<Developer>, IDeveloperRepository
    {
        public DeveloperRepository(ApplicationDbContext db, ILogger logger) : base(db, logger)
        {

        }

        public override async Task<IEnumerable<Developer>> All()
        {
            try
            {
                return await _dbSet.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} All function error", typeof(DeveloperRepository));
                return new List<Developer>();
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
    }
}
