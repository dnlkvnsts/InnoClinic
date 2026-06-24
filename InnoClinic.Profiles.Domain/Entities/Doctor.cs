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
        public string? PhotoUrl { get; set; }
        public string Specialization { get; set; } = string.Empty;
        public int CareerStartYear { get; set; } 
        public string Status { get; set; } = "At work"; 
        public string OfficeAddress { get; set; } = string.Empty;
        public string UserId { get; set; } = string.Empty; 
    }
}
