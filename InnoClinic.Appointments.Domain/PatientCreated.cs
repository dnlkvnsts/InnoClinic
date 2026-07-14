using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InnoClinic.Appointments.Domain
{
    public record PatientCreated(
        Guid Id,
        string FirstName,
        string LastName,
        string? MiddleName
    );
}
