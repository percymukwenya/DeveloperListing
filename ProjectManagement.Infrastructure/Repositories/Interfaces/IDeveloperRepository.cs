using ProjectManagement.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagement.Infrastructure.Repositories.Interfaces
{
    public interface IDeveloperRepository: IGenericRepository<Developer>
    {
        Task RefreshCache();
    }
}
