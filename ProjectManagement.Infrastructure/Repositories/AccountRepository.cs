using Microsoft.EntityFrameworkCore;
using ProjectManagement.Common.Models;
using ProjectManagement.Common.Models.DTOs;
using ProjectManagement.Infrastructure.Data;
using ProjectManagement.Infrastructure.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagement.Infrastructure.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly ApplicationDbContext _db;

        public AccountRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<AppUser> Login(LoginDTO loginDTO)
        {
            var user = await _db.AppUsers.SingleOrDefaultAsync(x => x.UserName == loginDTO.UserName);

            if (user == null) return null;

            using var hmac = new HMACSHA512(user.PasswordSalt);

            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDTO.Password));

            for(int i = 0; i < computedHash.Length; i++)
            {
                if (computedHash[i] != user.PasswordHash[i]) return null;
            }

            return user;
        }

        public async Task<AppUser> Register(RegisterDTO registerDTO)
        {
            if (await UserExists(registerDTO.UserName)) return null;

            using var hmac = new HMACSHA512();

            var user = new AppUser
            {
                UserName = registerDTO.UserName.ToLower(),
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDTO.Password)),
                PasswordSalt = hmac.Key
            };

            _db.AppUsers.Add(user);
            await _db.SaveChangesAsync();

            return user;
        }

        private async Task<bool> UserExists(string username)
        {
            return await _db.AppUsers.AnyAsync(x => x.UserName == username.ToLower());
        }
    }
}
