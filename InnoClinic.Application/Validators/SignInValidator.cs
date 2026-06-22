using FluentValidation;
using InnoClinic.Application.Features.Users.Commands.SignIn;
using InnoClinic.Application.Features.Users.Commands.SignUp;
using InnoClinic.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InnoClinic.Application.Validators
{
    public class SignInValidator : AbstractValidator<SignInCommand>
    {
        public SignInValidator(IIdentityService identityService)
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Please, enter the email")
                .EmailAddress().WithMessage("You've entered an invalid email");
                

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Please,enter the password")
                .Length(6, 15).WithMessage("Password must be between 6 and 15 symbols");
        }
    }
}
