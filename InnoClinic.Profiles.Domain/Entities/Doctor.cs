using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InnoClinic.Profiles.Domain.Entities
{
    public class Doctor
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string? MiddleName { get; set; }

        public DateTime DateOfBirth { get; set; }
        public string? PhotoUrl { get; set; }
    
        public int CareerStartYear { get; set; } 
        public string Status { get; set; } = "At work"; 


        public Guid AccountId { get; set; }


        public Guid SpecializationId { get; set; }
        public Specialization Specialization { get; set; } = null!;


        public string OfficeAddress { get; set; } = string.Empty;
       

       
    }
}
