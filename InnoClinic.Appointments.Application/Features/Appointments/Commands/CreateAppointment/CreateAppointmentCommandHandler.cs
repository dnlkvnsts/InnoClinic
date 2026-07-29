using InnoClinic.Appointments.Application.Interfaces;
using InnoClinic.Appointments.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InnoClinic.Appointments.Application.Features.Appointments.Commands.CreateAppointment
{
    public  class CreateAppointmentCommandHandler : IRequestHandler<CreateAppointmentCommand, Guid>
    {

        private readonly IAppointmentRepository _repository;

        public CreateAppointmentCommandHandler(IAppointmentRepository repository)
        {
            _repository = repository;
        }

        public async Task<Guid> Handle(CreateAppointmentCommand request, CancellationToken cancellationToken)
        {
            var appointment = new Appointment
            {
                Id = Guid.NewGuid(),
                PatientId = request.PatientId,
                DoctorId = request.DoctorId,
                ServiceId = request.ServiceId,
                Date = request.Date.Date,
                Time = request.Time,
                IsApproved = false
            };

            await _repository.AddAsync(appointment, cancellationToken);
            await _repository.SaveChangesAsync(cancellationToken);

            return appointment.Id;
        }

    }
}
