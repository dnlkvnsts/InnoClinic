using InnoClinic.Profiles.Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InnoClinic.Profiles.Application.Features.Doctors.Queries.GetDoctors
{
    public  record  GetDoctorsQuery(string? FullName = null, Guid? SpecializationId = null) : IRequest<IEnumerable<DoctorDto>>;
    
}
