
using FluentValidation;
using InnoClinic.Profiles.Application.Features.Doctors.Commands.CreateDoctors;

namespace InnoClinic.Profiles.Application.Validators
{
    public class CreateDoctorsCommandValidator : AbstractValidator<CreateDoctorsCommand>
    {

        public CreateDoctorsCommandValidator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("Please, enter the first name");

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("Please, enter the last name");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Please, enter the email")
                .EmailAddress().WithMessage("You've entered an invalid email");

            RuleFor(x => x.DateOfBirth)
                .NotEmpty().WithMessage("Please, select the date")
                .LessThanOrEqualTo(DateTime.UtcNow.Date).WithMessage("Date of birth cannot be in the future");

          
            RuleFor(x => x.CareerStartYear)
                .NotEmpty().WithMessage("Please, select the year")
                .LessThanOrEqualTo(DateTime.UtcNow.Year).WithMessage("Career start year cannot be in the future");

            RuleFor(x => x.SpecializationId)
                .NotEmpty().WithMessage("Please, choose the specialization");

            RuleFor(x => x.OfficeAddress)
                .NotEmpty().WithMessage("Please, choose the office");
        }
    }
}
