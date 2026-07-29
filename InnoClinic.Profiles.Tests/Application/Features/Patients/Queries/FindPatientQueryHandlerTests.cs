using InnoClinic.Profiles.Application.Features.Patients.Queries.FindPatient;
using InnoClinic.Profiles.Application.Interfaces;
using Moq;
using FluentAssertions;
using InnoClinic.Profiles.Domain.Entities;


namespace InnoClinic.Profiles.Tests.Application.Features.Patients.Queries
{
    public  class FindPatientQueryHandlerTests
    {
        private readonly Mock<IPatientRepository> _patientRepositoryMock;
        private readonly FindPatientQueryHandler _handler;

        public FindPatientQueryHandlerTests()
        {
            _patientRepositoryMock = new Mock<IPatientRepository>();
            _handler = new FindPatientQueryHandler(_patientRepositoryMock.Object);
        }

        [Fact]
        public async Task Handle_WhenScoreIs18_ShouldReturnMatchFound()
        {
           
            var unlinkedPatient = new Patient
            {
                Id = Guid.NewGuid(),
                FirstName = "James",
                LastName = "Wilson",
                MiddleName = "Robert",
                DateOfBirth = new DateTime(1988, 11, 23),
                IsLinkedToAccount = false
            };

            _patientRepositoryMock
                .Setup(r => r.GetUnlinkedPatientsAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(new List<Patient> { unlinkedPatient });

            var query = new FindPatientQuery("James", "Wilson", "Robert", new DateTime(1988, 11, 23));

           
            var result = await _handler.Handle(query, CancellationToken.None);

            
            result.IsMatchFound.Should().BeTrue();
            result.MatchedPatientId.Should().Be(unlinkedPatient.Id);
            result.MatchedProfile.Should().NotBeNull();
            result.MatchedProfile!.FirstName.Should().Be("James");
        }

        [Fact]
        public async Task Handle_WhenScoreIsExactly13_ShouldReturnMatchFound()
        {
           
            var unlinkedPatient = new Patient
            {
                Id = Guid.NewGuid(),
                FirstName = "James",
                LastName = "Brown",
                MiddleName = "Robert",
                DateOfBirth = new DateTime(1988, 11, 23),
                IsLinkedToAccount = false
            };

            _patientRepositoryMock
                .Setup(r => r.GetUnlinkedPatientsAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(new List<Patient> { unlinkedPatient });

            var query = new FindPatientQuery("James", "Wilson", "Robert", new DateTime(1988, 11, 23));

            
            var result = await _handler.Handle(query, CancellationToken.None);

            
            result.IsMatchFound.Should().BeTrue();
            result.MatchedPatientId.Should().Be(unlinkedPatient.Id);
        }

        [Fact]
        public async Task Handle_WhenScoreIs10_ShouldReturnMatchNotFound()
        {
            var unlinkedPatient = new Patient
            {
                Id = Guid.NewGuid(),
                FirstName = "James",
                LastName = "Wilson",
                MiddleName = "Smith",
                DateOfBirth = new DateTime(2000, 1, 1),
                IsLinkedToAccount = false
            };

            _patientRepositoryMock
                .Setup(r => r.GetUnlinkedPatientsAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(new List<Patient> { unlinkedPatient });

            var query = new FindPatientQuery("James", "Wilson", "Robert", new DateTime(1988, 11, 23));

            
            var result = await _handler.Handle(query, CancellationToken.None);

           
            result.IsMatchFound.Should().BeFalse();
            result.MatchedPatientId.Should().BeNull();
            result.MatchedProfile.Should().BeNull();
        }

        [Fact]
        public async Task Handle_WhenNoUnlinkedPatientsExist_ShouldReturnMatchNotFound()
        {
           
            _patientRepositoryMock
                .Setup(r => r.GetUnlinkedPatientsAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(new List<Patient>());

            var query = new FindPatientQuery("James", "Wilson", "Robert", new DateTime(1988, 11, 23));

          
            var result = await _handler.Handle(query, CancellationToken.None);

          
            result.IsMatchFound.Should().BeFalse();
        }



    }
}
