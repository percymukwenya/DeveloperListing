using ProjectManagement.Common.Helpers;
using ProjectManagement.Common.Models;
using ProjectManagement.Infrastructure.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManegment.Test
{
    public class DeveloperServiceMock : IDeveloperRepository
    {
        private readonly IEnumerable<Developer> _developers;
        public DeveloperServiceMock()
        {
            _developers = new List<Developer>()
            {
                new Developer()
                {
                    Id = 1,
                    FirstName = "Bill",
                    LastName = "Gates",
                    Email = "bill@microsoft.com",
                    PhoneNumber = "0112134569",
                    DeveloperType = "Back-End",
                    YearsOfExperience = 30
                },
                new Developer()
                {
                    Id = 2,
                    FirstName = "Steve",
                    LastName = "Jobs",
                    Email = "steve@apple.com",
                    PhoneNumber = "0112134569",
                    DeveloperType = "Front-End",
                    YearsOfExperience = 20
                },
                new Developer()
                {
                    Id = 3,
                    FirstName = "Percy",
                    LastName = "Mukwenya",
                    Email = "percy@test.com",
                    PhoneNumber = "0112134569",
                    DeveloperType = "Back-End",
                    YearsOfExperience = 7
                },
                new Developer()
                {
                    Id = 4,
                    FirstName = "Fari",
                    LastName = "Gerard",
                    Email = "fari@test.com",
                    PhoneNumber = "0112134569",
                    DeveloperType = "Full-Stack",
                    YearsOfExperience = 2
                },
            };
        }

        public Task<IEnumerable<Developer>> All()
        {
            return (Task<IEnumerable<Developer>>)_developers;
        }

        public Task<Developer> Add(Developer entity)
        {
            throw new NotImplementedException();
        }        

        public Task<bool> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<PagedList<Developer>> Find(UserParams userParams)
        {
            throw new NotImplementedException();
        }

        public Task<Developer> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task RefreshCache()
        {
            throw new NotImplementedException();
        }

        public Task<Developer> Upsert(Developer entity)
        {
            throw new NotImplementedException();
        }

        private async Task<IEnumerable<Developer>> GetDevelopers()
        {
            return _developers;
        }
    }
}
