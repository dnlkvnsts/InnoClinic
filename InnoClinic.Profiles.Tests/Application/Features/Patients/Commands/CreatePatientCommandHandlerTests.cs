using FluentAssertions;
using InnoClinic.Profiles.Application.Features.Patients.Commands.CreatePatient;
using InnoClinic.Profiles.Application.Interfaces;
using InnoClinic.Profiles.Domain.Entities;
using Moq;

namespace InnoClinic.Profiles.Tests.Application.Features.Patients.Commands
{
    public class CreatePatientCommandHandlerTests
    {
        private readonly Mock<IPatientRepository> _patientRepositoryMock;
        private readonly CreatePatientCommandHandler _handler;

        public CreatePatientCommandHandlerTests()
        {
            _patientRepositoryMock = new Mock<IPatientRepository>();
            _handler = new CreatePatientCommandHandler(_patientRepositoryMock.Object);
        }

        [Fact]
        public async Task Handle_ShouldCreatePatient_AndSetIsLinkedToAccountToTrue()
        {
           
            var accountId = Guid.NewGuid();
            var command = new CreatePatientCommand(
                FirstName: "Robert",
                LastName: "Downey",
                MiddleName: "Junior",
                Phone: "+19998887766",
                DateOfBirth: new DateTime(1965, 4, 4),
                PhotoUrl: "https://example.com/photo.jpg",
                AccountId: accountId
            );

            Patient createdPatient = null!;

            
            _patientRepositoryMock
                .Setup(r => r.AddAsync(It.IsAny<Patient>(), It.IsAny<CancellationToken>()))
                .Callback<Patient, CancellationToken>((patient, _) => createdPatient = patient)
                .Returns(Task.CompletedTask);

            
            var resultId = await _handler.Handle(command, CancellationToken.None);

           
            resultId.Should().NotBeEmpty();

            
            _patientRepositoryMock.Verify(
                r => r.AddAsync(It.IsAny<Patient>(), It.IsAny<CancellationToken>()),
                Times.Once
            );

       
            createdPatient.Should().NotBeNull();
            createdPatient.Id.Should().Be(resultId);
            createdPatient.FirstName.Should().Be("Robert");
            createdPatient.LastName.Should().Be("Downey");
            createdPatient.MiddleName.Should().Be("Junior");
            createdPatient.Phone.Should().Be("+19998887766");
            createdPatient.PhotoUrl.Should().Be("https://example.com/photo.jpg");
            createdPatient.DateOfBirth.Should().Be(new DateTime(1965, 4, 4));

           
            createdPatient.IsLinkedToAccount.Should().BeTrue();
            createdPatient.AccountId.Should().Be(accountId);
        }




    }
}
