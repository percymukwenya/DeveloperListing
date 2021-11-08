using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagement.Infrastructure.Repositories.Interfaces
{
    public interface IUnitOfWork
    {
        IDeveloperRepository DeveloperRepository { get; }
        IProjectRepository ProjectRepository { get; }
        Task<bool> Completed();
        bool HasChanges();
    }
}
