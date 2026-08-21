using InnoClinic.Profiles.Application.Interfaces;
using MassTransit;
using Shared.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InnoClinic.Profiles.Infrastructure.Consumers
{
    public class DoctorAccountCreatedConsumer : IConsumer<DoctorAccountCreated>
    {
        private readonly IDoctorRepository _doctorRepository;

        public DoctorAccountCreatedConsumer(IDoctorRepository doctorRepository)
        {
            _doctorRepository = doctorRepository;
        }

        public async Task Consume(ConsumeContext<DoctorAccountCreated> context)
        {
            var msg = context.Message;
            var doctor = await _doctorRepository.GetByIdAsync(msg.DoctorId, context.CancellationToken);

            if (doctor != null)
            {
                doctor.AccountId = Guid.Parse(msg.UserId);
                await _doctorRepository.UpdateAsync(doctor, context.CancellationToken);
            }
        }
    }
}
