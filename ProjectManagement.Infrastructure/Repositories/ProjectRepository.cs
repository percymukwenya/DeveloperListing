using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ProjectManagement.Common.Enums;
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
    public class ProjectRepository : GenericRepository<Project>, IProjectRepository
    {
        public ProjectRepository(ApplicationDbContext db, ILogger logger, Func<CacheTech, ICacheService> cacheService) : base(db, logger, cacheService)
        {

        }

        public override async Task<IEnumerable<Project>> All()
        {
            try
            {
                return await _dbSet.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} All function error", typeof(ProjectRepository));
                return new List<Project>();
            }
        }

        public override async Task<Project> Upsert(Project entity)
        {
            try
            {
                var existingProject = await _dbSet.Where(x => x.Id == entity.Id)
                                                    .FirstOrDefaultAsync();

                if (existingProject == null)
                    return await Add(entity);

                _db.Entry(entity).State = EntityState.Modified;
                return existingProject;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} Upsert function error", typeof(ProjectRepository));
                return null;
            }
        }

        public override async Task<bool> Delete(int id)
        {
            try
            {
                var existingProject = await _dbSet.Where(x => x.Id == id)
                                        .FirstOrDefaultAsync();

                if (existingProject == null) return false;

                _dbSet.Remove(existingProject);

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} Delete function error", typeof(ProjectRepository));
                return false;
            }
        }
    }
}
