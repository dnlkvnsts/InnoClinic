using InnoClinic.Profiles.Application.DTOs;
using InnoClinic.Profiles.Application.Interfaces;
using MediatR;

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
            var query = _doctorRepository.GetDoctorsQuery();

            query = query.Where(d => d.Status == "At work");

            var currentYear = System.DateTime.UtcNow.Year;


            var result = query.Select(d => new DoctorDto(
                    d.PhotoUrl,
                    d.FirstName,
                    d.LastName,
                    d.MiddleName,
                    d.Specialization,
                    currentYear - d.CareerStartYear + 1,
                    d.OfficeAddress
                )).ToList();

            return await Task.FromResult(result);
        }



    }
}
