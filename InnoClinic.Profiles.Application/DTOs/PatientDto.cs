using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InnoClinic.Profiles.Application.DTOs
{
    public record PatientDto(
        string FirstName,
        string LastName,
        string? MiddleName,
        DateTime DateOfBirth,
        bool IsLinkedToAccount
  );
}
