using InnoClinic.Profiles.Domain.Entities;
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
        Guid SpecializationId,
        string SpecializationName,
        int ExperienceYears, 
        string OfficeAddress
    );
}
