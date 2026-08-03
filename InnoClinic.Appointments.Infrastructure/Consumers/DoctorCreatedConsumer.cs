using InnoClinic.Appointments.Domain;
using InnoClinic.Appointments.Domain.Entities;
using InnoClinic.Appointments.Infrastructure.Persistence;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InnoClinic.Appointments.Infrastructure.Consumers
{
    public  class DoctorCreatedConsumer : IConsumer<DoctorCreated>
    {

        private readonly AppointmentsDbContext _context;


        public DoctorCreatedConsumer(AppointmentsDbContext context)
        {
            _context = context;
        }


        public async Task Consume(ConsumeContext<DoctorCreated> context)
        {
            var message = context.Message;

            var existingDoctor = await _context.Doctors.FindAsync(message.Id);
            if (existingDoctor != null) return;


            var doctor = new Doctor
            {
                Id = message.Id,
                FirstName = message.FirstName,
                LastName = message.LastName,
                MiddleName = message.MiddleName
            };

            
            _context.Doctors.Add(doctor);
            await _context.SaveChangesAsync();
        }

    }
}
