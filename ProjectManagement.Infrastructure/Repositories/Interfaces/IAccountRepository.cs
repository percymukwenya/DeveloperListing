using ProjectManagement.Common.Models;
using ProjectManagement.Common.Models.DTOs;
using ProjectManagement.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagement.Infrastructure.Repositories.Interfaces
{
    public interface IAccountRepository
    {
        Task<AppUser> Register(RegisterDTO registerDTO);
        Task<AppUser> Login(LoginDTO loginDTO);
    }
}
