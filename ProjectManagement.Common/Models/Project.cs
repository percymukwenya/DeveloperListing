using ProjectManagement.Common.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagement.Common.Models
{
    public class Project: BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int DeveloperId { get; set; }
        public Developer Developer { get; set; }
    }
}
