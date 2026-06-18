using FluentValidation;
using InnoClinic.Application.Features.Users.Queries.SignUp;
using InnoClinic.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InnoClinic.Application.Validators
{
    public  class SignUpValidator : AbstractValidator<SignUpRequest>
    {
        public SignUpValidator(IIdentityService identityService)
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Please,enter the email")
                .EmailAddress().WithMessage("Please,enter the correct form of email")
                .MustAsync(async (email, _) => await identityService.IsEmailUniqueAsync(email))
                .WithMessage("The user with this email has already existeed");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Please,enter the password")
                .Length(6, 15).WithMessage("Password must be between 6 and 15 symbols");

            RuleFor(x => x.ReEnteredPassword)
                .NotEmpty().WithMessage("Please, reenter the password")
                .Equal(x => x.Password).WithMessage("The passwords you have been entered dont coincide");
        }
    }
}
