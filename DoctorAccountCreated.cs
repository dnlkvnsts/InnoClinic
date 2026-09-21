using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InnoClinic.Shared.Contracts
{
    public record DoctorAccountCreated(
       Guid AccountId,
       string FirstName,
       string LastName,
       string? MiddleName,
       DateTime DateOfBirth,
       string? PhotoUrl,
       int CareerStartYear,
       string? Status,
       Guid SpecializationId,
       string OfficeAddress
   );
}