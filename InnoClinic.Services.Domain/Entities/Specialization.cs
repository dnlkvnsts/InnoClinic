using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InnoClinic.Services.Domain.Entities
{
    public  class Specialization
    {
        public Guid Id { get; set; }
        public string SpecializationName { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;


        public ICollection<Service> Services { get; set; } = new List<Service>();

    }
}
