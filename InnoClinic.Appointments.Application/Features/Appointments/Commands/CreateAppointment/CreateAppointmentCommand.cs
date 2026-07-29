using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InnoClinic.Appointments.Application.Features.Appointments.Commands.CreateAppointment
{
    public record CreateAppointmentCommand(
        Guid PatientId,
        Guid DoctorId,
        Guid ServiceId,
        DateTime Date,
        TimeSpan Time
    ) : IRequest<Guid>;
}
