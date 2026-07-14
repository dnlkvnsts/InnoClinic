using InnoClinic.Profiles.Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InnoClinic.Profiles.Application.Features.Patients.Queries.GetPatients
{
    public record GetPatientsQuery() : IRequest<IEnumerable<PatientDto>>;
}
