using FluentValidation;
using InnoClinic.Application.Validators;
using InnoClinic.Application.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace InnoClinic.Application.Features.Users.Commands.SignUp
{
    public  class SignUpCommandHandler : IRequestHandler<SignUpCommand, SignUpResponse>
    {


        private readonly IIdentityService _identityService;
        private readonly IValidator<SignUpCommand> _validator;
        private readonly IEmailService _emailService;


        public SignUpCommandHandler(IIdentityService identityService, IValidator<SignUpCommand> validator, IEmailService emailService)
        {
            _identityService = identityService;
            _validator = validator;
            _emailService = emailService;
        }

        public async Task<SignUpResponse> Handle(SignUpCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToArray();
                return new SignUpResponse(false, errors);
            }

            var (isSuccess, userId, identityErrors) = await _identityService.CreateUserAsync(request.Email, request.Password);

            if (!isSuccess)
            {
                return new SignUpResponse(false, identityErrors);
            }


            var token = await _identityService.GenerateEmailConfirmationTokenAsync(userId!);

            var confirmationLink = $"https://localhost:7155/api/auth/confirm-email?userId={userId}&token={Uri.EscapeDataString(token)}";


            await _emailService.SendConfirmationEmailAsync(request.Email, confirmationLink);

            return new SignUpResponse(true, null);

        }





    }
}
