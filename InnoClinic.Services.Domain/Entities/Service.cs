using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InnoClinic.Services.Domain.Entities
{
    public class Service
    {
        public Guid Id { get; set; }
        public string ServiceName { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public bool IsActive { get; set; } = true;



        public Guid CategoryId { get; set; }
        public ServiceCategory Category { get; set; } = null!;



        public Guid? SpecializationId { get; set; }
        public Specialization? Specialization { get; set; }
    }
}
