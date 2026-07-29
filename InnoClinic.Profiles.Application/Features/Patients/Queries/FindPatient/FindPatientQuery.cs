using InnoClinic.Profiles.Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InnoClinic.Profiles.Application.Features.Patients.Queries.FindPatient
{
    public record FindPatientQuery(
        string FirstName,
        string LastName,
        string? MiddleName,
        DateTime DateOfBirth
    ) : IRequest<PatientMatchResultDto>;
}
