using InnoClinic.Auth.Application.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InnoClinic.Auth.Application.Features.Users.Commands.ResendEmail
{
    public class ResendEmailCommandHandler : IRequestHandler<ResendEmailCommand, bool>
    {
        private readonly IIdentityService _identityService;
        private readonly IEmailService _emailService;

        public ResendEmailCommandHandler(
            IIdentityService identityService,
            IEmailService emailService)
        {
            _identityService = identityService;
            _emailService = emailService;
        }

        public async Task<bool> Handle(ResendEmailCommand request, CancellationToken cancellationToken)
        {
           
            var exists = await _identityService.UserExistsAsync(request.Email);
            if (!exists)
            {
                return true;
            }

            
            var isConfirmed = await _identityService.IsEmailConfirmedAsync(request.Email);
            if (isConfirmed)
            {
                return true;
            }

          
            var (isSuccess, userId, token, errors) = await _identityService.GenerateEmailConfirmationTokenByEmailAsync(request.Email);
            if (!isSuccess || string.IsNullOrEmpty(token) || string.IsNullOrEmpty(userId))
            {
                return false;
            }

         
            var confirmationLink = $"https://localhost:7115/api/auth/confirm-email?userId={userId}&token={Uri.EscapeDataString(token)}";

      
            await _emailService.SendConfirmationEmailAsync(request.Email, confirmationLink);

            return true;
        }

    }
}
