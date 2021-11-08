using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagement.Common.Models.DTOs
{
    public class DeveloperDTO
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public int YearsOfExperience { get; set; }
        public string DeveloperType { get; set; }
        public List<ProjectDTO> Projects { get; set; } = new List<ProjectDTO>();
    }
}
