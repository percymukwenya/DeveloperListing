using Microsoft.Extensions.Logging;
using ProjectManagement.Infrastructure.Data;
using ProjectManagement.Infrastructure.Repositories.Interfaces;
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

        public UnitOfWork(ApplicationDbContext db, ILoggerFactory loggerFactory)
        {
            _db = db;
            _logger = loggerFactory.CreateLogger("logs");
        }
        public IDeveloperRepository DeveloperRepository => new DeveloperRepository(_db, _logger);

        public IProjectRepository ProjectRepository => new ProjectRepository(_db, _logger);

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
