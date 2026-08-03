using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InnoClinic.Appointments.Domain.Entities
{
    public class Doctor
    {
        public Guid Id { get; set; } 
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string? MiddleName { get; set; }

       
        public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
    }
}
