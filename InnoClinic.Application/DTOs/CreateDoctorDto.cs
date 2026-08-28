using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InnoClinic.Auth.Application.DTOs
{
    public record CreateDoctorDto(
     string Email,
     string Password,
     string FirstName,
     string LastName,
     string? MiddleName,
     DateTime DateOfBirth,
     string? PhotoUrl,
     int CareerStartYear,
     Guid SpecializationId,
     string OfficeAddress,
     string? Status
 );
}
