using FluentValidation;
using InnoClinic.Profiles.Application.Features.Patients.Commands.CreatePatient;

namespace InnoClinic.Profiles.Application.Validators
{
    public class CreatePatientValidator : AbstractValidator<CreatePatientCommand>
    {
        public CreatePatientValidator()
        {

            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("Please, enter the first name");

          
            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("Please, enter the last name");

           
            RuleFor(x => x.Phone)
                .NotEmpty().WithMessage("Please, enter the phone number")
                .Must(phone => phone != null && phone.StartsWith("+"))
                .WithMessage("Phone number must contain + prefix")
                .Matches(@"^\+[0-9]+$")
                .WithMessage("You've entered an invalid phone number"); 

           
            RuleFor(x => x.DateOfBirth)
                .NotEmpty().WithMessage("Please, select the date")
                .LessThan(DateTime.UtcNow).WithMessage("Date of birth cannot be in the future");

           
            RuleFor(x => x.AccountId)
                .NotEmpty().WithMessage("Account ID is required");

        }
        
    }
}
