using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InnoClinic.Appointments.Domain.Entities
{
    public  class Service
    {

        public Guid Id { get; set; }
        public string ServiceName { get; set; } = string.Empty;
        public decimal Price { get; set; }


        public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();

    }
}
