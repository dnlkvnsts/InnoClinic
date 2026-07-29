using FluentAssertions;
using InnoClinic.Profiles.Application.Features.Patients.Commands.LinkExistingPatient;
using InnoClinic.Profiles.Application.Interfaces;
using InnoClinic.Profiles.Domain.Entities;
using Moq;


namespace InnoClinic.Profiles.Tests.Application.Features.Patients.Commands
{
    public class LinkExistingPatientCommandHandlerTests
    {
        private readonly Mock<IPatientRepository> _patientRepositoryMock;
        private readonly LinkExistingPatientCommandHandler _handler;

        public LinkExistingPatientCommandHandlerTests()
        {
            _patientRepositoryMock = new Mock<IPatientRepository>();
            _handler = new LinkExistingPatientCommandHandler(_patientRepositoryMock.Object);
        }

        [Fact]
        public async Task Handle_WhenPatientExists_ShouldLinkAndReturnTrue()
        {
           
            var patientId = Guid.NewGuid();
            var accountId = Guid.NewGuid();
            var existingPatient = new Patient
            {
                Id = patientId,
                FirstName = "James",
                LastName = "Wilson",
                IsLinkedToAccount = false,
                AccountId = null
            };

            _patientRepositoryMock
                .Setup(r => r.GetByIdAsync(patientId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(existingPatient);

            var command = new LinkExistingPatientCommand(patientId, accountId);

           
            var result = await _handler.Handle(command, CancellationToken.None);

            result.Should().BeTrue();
            existingPatient.IsLinkedToAccount.Should().BeTrue();
            existingPatient.AccountId.Should().Be(accountId);

            _patientRepositoryMock.Verify(
                r => r.UpdateAsync(existingPatient, It.IsAny<CancellationToken>()),
                Times.Once
            );
        }

        [Fact]
        public async Task Handle_WhenPatientNotFound_ShouldReturnFalse()
        {
            
            var patientId = Guid.NewGuid();

            _patientRepositoryMock
                .Setup(r => r.GetByIdAsync(patientId, It.IsAny<CancellationToken>()))
                .ReturnsAsync((Patient?)null);

            var command = new LinkExistingPatientCommand(patientId, Guid.NewGuid());

           
            var result = await _handler.Handle(command, CancellationToken.None);

           
            result.Should().BeFalse();

            _patientRepositoryMock.Verify(
                r => r.UpdateAsync(It.IsAny<Patient>(), It.IsAny<CancellationToken>()),
                Times.Never
            );
        }




    }
}
