using FluentValidation.TestHelper;
using InnoClinic.Profiles.Application.Features.Patients.Commands.CreatePatient;
using InnoClinic.Profiles.Application.Validators;


namespace InnoClinic.Profiles.Tests.Application.Validators
{
    public class CreatePatientValidatorTests
    {
        private readonly CreatePatientValidator _validator;

        public CreatePatientValidatorTests()
        {
            _validator = new CreatePatientValidator();
        }

        [Fact]
        public void Should_Have_Error_When_Required_Fields_Are_Empty()
        {
            var command = new CreatePatientCommand(
                FirstName: "",
                LastName: "",
                MiddleName: null,
                Phone: "",
                DateOfBirth: default,
                PhotoUrl: null,
                AccountId: Guid.Empty
            );

            var result = _validator.TestValidate(command);

            result.ShouldHaveValidationErrorFor(x => x.FirstName)
                .WithErrorMessage("Please, enter the first name");

            result.ShouldHaveValidationErrorFor(x => x.LastName)
                .WithErrorMessage("Please, enter the last name");

            result.ShouldHaveValidationErrorFor(x => x.Phone)
                .WithErrorMessage("Please, enter the phone number");

            result.ShouldHaveValidationErrorFor(x => x.DateOfBirth)
                .WithErrorMessage("Please, select the date");

            result.ShouldHaveValidationErrorFor(x => x.AccountId)
                .WithErrorMessage("Account ID is required");
        }

        [Theory]
        [InlineData("123456789", "Phone number must contain + prefix")]
        [InlineData("+123abc456", "You've entered an invalid phone number")]
        public void Should_Have_Error_When_Phone_Is_Invalid(string invalidPhone, string expectedErrorMessage)
        {
            var command = new CreatePatientCommand(
                FirstName: "John",
                LastName: "Doe",
                MiddleName: null,
                Phone: invalidPhone,
                DateOfBirth: new DateTime(1990, 1, 1),
                PhotoUrl: null,
                AccountId: Guid.NewGuid()
            );

            var result = _validator.TestValidate(command);

            result.ShouldHaveValidationErrorFor(x => x.Phone)
                .WithErrorMessage(expectedErrorMessage);
        }

        [Fact]
        public void Should_Have_Error_When_DateOfBirth_Is_In_Future()
        {
            var command = new CreatePatientCommand(
                FirstName: "John",
                LastName: "Doe",
                MiddleName: null,
                Phone: "+375291234567",
                DateOfBirth: DateTime.UtcNow.AddDays(1),
                PhotoUrl: null,
                AccountId: Guid.NewGuid()
            );

            var result = _validator.TestValidate(command);

            result.ShouldHaveValidationErrorFor(x => x.DateOfBirth)
                .WithErrorMessage("Date of birth cannot be in the future");
        }

        [Fact]
        public void Should_Not_Have_Errors_When_Command_Is_Valid()
        {
            var command = new CreatePatientCommand(
                FirstName: "John",
                LastName: "Doe",
                MiddleName: "Robert",
                Phone: "+375291234567",
                DateOfBirth: new DateTime(1990, 1, 1),
                PhotoUrl: "https://example.com/photo.jpg",
                AccountId: Guid.NewGuid()
            );

            var result = _validator.TestValidate(command);

            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}