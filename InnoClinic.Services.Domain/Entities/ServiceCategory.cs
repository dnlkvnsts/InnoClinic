using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InnoClinic.Services.Domain.Entities
{
    public class ServiceCategory
    {
        public Guid Id { get; set; }
        public string CategoryName { get; set; } = string.Empty; 
        public int TimeSlotSize { get; set; }



        public ICollection<Service> Services { get; set; } = new List<Service>();


    }
}
