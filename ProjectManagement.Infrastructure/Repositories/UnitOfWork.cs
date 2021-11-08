using Microsoft.Extensions.Logging;
using ProjectManagement.Common.Enums;
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
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly ApplicationDbContext _db;
        private readonly ILogger _logger;
        private readonly Func<CacheTech, ICacheService> _cacheService;

        public UnitOfWork(ApplicationDbContext db, ILoggerFactory loggerFactory, Func<CacheTech, ICacheService> cacheService)
        {
            _db = db;
            _logger = loggerFactory.CreateLogger("logs");
            _cacheService = cacheService;
        }
        public IDeveloperRepository DeveloperRepository => new DeveloperRepository(_db, _logger, _cacheService);

        public IProjectRepository ProjectRepository => new ProjectRepository(_db, _logger, _cacheService);

        public async Task<bool> Completed()
        {
            return await _db.SaveChangesAsync() > 0;
        }        

        public bool HasChanges()
        {
            return _db.ChangeTracker.HasChanges();
        }

        public void Dispose()
        {
            _db.Dispose();
        }
    }
}
