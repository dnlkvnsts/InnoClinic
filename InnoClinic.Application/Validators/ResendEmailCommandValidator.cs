using FluentValidation;
using InnoClinic.Auth.Application.Features.Users.Commands.ResendEmail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InnoClinic.Auth.Application.Validators
{
    public class ResendEmailCommandValidator : AbstractValidator<ResendEmailCommand>
    {
        public ResendEmailCommandValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Invalid email format.");
        }

    }
}
