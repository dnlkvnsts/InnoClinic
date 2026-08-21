using InnoClinic.Auth.Application.Interfaces;
using MassTransit;
using Microsoft.Extensions.Logging;
using Shared.Contracts;


namespace InnoClinic.Auth.Infrastructure.Consumers
{
    public class DoctorCreatedConsumer : IConsumer<DoctorCreated>
    {
        private readonly IIdentityService _identityService;
        private readonly IEmailService _emailService;
        private readonly ILogger<DoctorCreatedConsumer> _logger;

        public DoctorCreatedConsumer(
            IIdentityService identityService,
            IEmailService emailService,
            ILogger<DoctorCreatedConsumer> logger)
        {
            _identityService = identityService;
            _emailService = emailService;
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<DoctorCreated> context)
        {
            var msg = context.Message;

            var (isSuccess, userId, password, errors) =
                await _identityService.CreateDoctorAsync(msg.Email);

            if (!isSuccess)
            {
                _logger.LogError("Failed to create doctor account for {Email}: {Errors}",
                    msg.Email, string.Join(", ", errors ?? Array.Empty<string>()));
                return;
            }


            await context.Publish(new DoctorAccountCreated(msg.DoctorId, userId));

            var (tokenSuccess, token, tokenErrors) =
                    await _identityService.GenerateEmailConfirmationTokenAsync(userId);

            if (!tokenSuccess || string.IsNullOrEmpty(token))
            {
                _logger.LogError("Failed to generate token for {Email}: {Errors}",
                    msg.Email, string.Join(", ", tokenErrors ?? Array.Empty<string>()));
                return;
            }

            
            string confirmationLink = $"https://your-frontend-domain.com/confirm-email?userId={userId}&token={Uri.EscapeDataString(token)}";

            await _emailService.SendDoctorWelcomeEmailAsync(msg.Email, password, confirmationLink);
        }
    }

}
