using ProjectManagement.Common.Models.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagement.Common.Models
{
    public class Developer: BaseEntity
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string Email { get; set; }

        public string PhoneNumber { get; set; }
        public int YearsOfExperience { get; set; }

        [Required]
        public string DeveloperType { get; set; }
        public List<Project> Projects { get; set; } = new List<Project>();        
    }
}
