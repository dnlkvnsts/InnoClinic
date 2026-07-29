using FluentAssertions;
using InnoClinic.Appointments.Domain;
using InnoClinic.Profiles.Application.Features.Patients.Queries.GetPatients;
using InnoClinic.Profiles.Application.Interfaces;
using InnoClinic.Profiles.Domain.Entities;
using MassTransit;
using Moq;


namespace InnoClinic.Profiles.Tests.Application.Features.Patients.Queries
{
    public  class GetPatientsQueryHandlerTests
    {

        private readonly Mock<IPatientRepository> _patientRepositoryMock;
        private readonly Mock<IPublishEndpoint> _publishEndpointMock;
        private readonly GetPatientsQueryHandler _handler;

        public GetPatientsQueryHandlerTests()
        {
            _patientRepositoryMock = new Mock<IPatientRepository>();
            _publishEndpointMock = new Mock<IPublishEndpoint>();

            _handler = new GetPatientsQueryHandler(
                _patientRepositoryMock.Object,
                _publishEndpointMock.Object
            );
        }

        [Fact]
        public async Task Handle_ShouldReturnPatientDtos_AndPublishEventsForEveryPatient()
        {
           
            var patients = new List<Patient>
            {
                new() { Id = Guid.NewGuid(), FirstName = "John", LastName = "Doe", Phone = "+123", DateOfBirth = DateTime.UtcNow },
                new() { Id = Guid.NewGuid(), FirstName = "Jane", LastName = "Smith", Phone = "+456", DateOfBirth = DateTime.UtcNow }
            };

            _patientRepositoryMock
                .Setup(r => r.GetPatientsQuery())
                .Returns(patients.AsQueryable());

            var query = new GetPatientsQuery();

            
            var result = await _handler.Handle(query, CancellationToken.None);

         
            result.Should().HaveCount(2);
            result.First().FirstName.Should().Be("John");

            
            _publishEndpointMock.Verify(
                p => p.Publish(It.IsAny<PatientCreated>(), It.IsAny<CancellationToken>()),
                Times.Exactly(2)
            );
        }
    }
}
