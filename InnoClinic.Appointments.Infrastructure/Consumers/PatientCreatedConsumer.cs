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
    public class PatientCreatedConsumer : IConsumer<PatientCreated>
    {


        private readonly AppointmentsDbContext _context;


        public PatientCreatedConsumer(AppointmentsDbContext context)
        {
            _context = context;
        }




        public async Task Consume(ConsumeContext<PatientCreated> context)
        {
            var message = context.Message;

            var existingPatient = await _context.Patients.FindAsync(message.Id);
            if (existingPatient != null) return;


            var patient = new Patient
            {
                Id = message.Id,
                FirstName = message.FirstName,
                LastName = message.LastName,
                MiddleName = message.MiddleName
            };



            _context.Patients.Add(patient);
            await _context.SaveChangesAsync();
        }

    }
}
