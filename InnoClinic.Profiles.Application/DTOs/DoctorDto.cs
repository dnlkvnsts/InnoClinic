using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InnoClinic.Profiles.Application.DTOs
{
    public record DoctorDto(
        string? PhotoUrl,
        string FirstName,
        string LastName,
        string? MiddleName,
        string Specialization,
        int ExperienceYears, 
        string OfficeAddress
    );
}
