using InnoClinic.Profiles.Application.DTOs;
using InnoClinic.Profiles.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InnoClinic.Profiles.Application.Features.Doctors.Queries.GetDoctors
{
    public  class GetDoctorsQueryHandler : IRequestHandler<GetDoctorsQuery, IEnumerable<DoctorDto>>
    {
        private readonly IDoctorRepository _doctorRepository;


        public GetDoctorsQueryHandler(IDoctorRepository doctorRepository)
        {
            _doctorRepository = doctorRepository;
        }


        public async Task<IEnumerable<DoctorDto>> Handle(GetDoctorsQuery request, CancellationToken cancellationToken)
        {
            var doctors = await _doctorRepository.GetDoctorsAsync(request.FullName, request.SpecializationId, cancellationToken);


            var currentYear = System.DateTime.UtcNow.Year;


            var result = doctors.Select(d => new DoctorDto(
                    d.PhotoUrl,
                    d.FirstName,
                    d.LastName,
                    d.MiddleName,
                    d.SpecializationId,
                    d.Specialization.SpecializationName,
                    currentYear - d.CareerStartYear + 1,
                    d.OfficeAddress
                )).ToList();

            return result;
        }


    }
}
