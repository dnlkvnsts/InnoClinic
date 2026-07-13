using InnoClinic.Appointments.Domain;
using InnoClinic.Profiles.Application.DTOs;
using InnoClinic.Profiles.Application.Interfaces;
using MassTransit;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MassTransit;
using InnoClinic.Appointments.Domain;

namespace InnoClinic.Profiles.Application.Features.Doctors.Queries.GetDoctors
{
    public  class GetDoctorsQueryHandler : IRequestHandler<GetDoctorsQuery, IEnumerable<DoctorDto>>
    {
        private readonly IDoctorRepository _doctorRepository;
        private readonly IPublishEndpoint _publishEndpoint;


        public GetDoctorsQueryHandler(IDoctorRepository doctorRepository, IPublishEndpoint publishEndpoint)
        {
            _doctorRepository = doctorRepository;
            _publishEndpoint = publishEndpoint;
        }


        public async Task<IEnumerable<DoctorDto>> Handle(GetDoctorsQuery request, CancellationToken cancellationToken)
        {
          
            var doctors = await _doctorRepository.GetDoctorsAsync(request.FullName, request.SpecializationId, cancellationToken);
          
            var doctors = _doctorRepository.GetDoctorsQuery().Where(d => d.Status == "At work").ToList();

            var currentYear = System.DateTime.UtcNow.Year;

            foreach (var doctor in doctors)
            {
                await _publishEndpoint.Publish(new DoctorCreated(
                    doctor.Id,
                    doctor.FirstName,
                    doctor.LastName,
                    doctor.MiddleName
                ), cancellationToken);
            }
            var currentYear = DateTime.UtcNow.Year;
          

            foreach (var doctor in doctors)
            {
                await _publishEndpoint.Publish(new DoctorCreated(
                    doctor.Id,
                    doctor.FirstName,
                    doctor.LastName,
                    doctor.MiddleName
                ), cancellationToken);
            }

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
