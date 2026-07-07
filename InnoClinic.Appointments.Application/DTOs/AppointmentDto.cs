using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InnoClinic.Appointments.Application.DTOs
{
    public record AppointmentDto(
        Guid PatientId,
        Guid DoctorId,
        Guid ServiceId,
        DateTime Date,
        TimeSpan Time
    );
}
