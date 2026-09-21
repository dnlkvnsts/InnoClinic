using FluentAssertions;
using InnoClinic.Services.Api.Controllers;
using InnoClinic.Services.Application.DTOs;
using InnoClinic.Services.Application.Features.Services.Queries.GetServices;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;


namespace InnoClinic.Services.Tests.Api
{
    public  class ServiceControllerTests
    {
        private readonly Mock<IMediator> _mediatorMock;
        private readonly ServiceController _serviceController;



        public ServiceControllerTests()
        {
            _mediatorMock = new Mock<IMediator>();
            _serviceController = new ServiceController(_mediatorMock.Object);
        }


        [Fact]
        public async Task GetDoctors_ShouldReturnOk_WithAllServicesDto()
        {
            var expectedDto = new AllServicesDto(
                Consultations: new List<SpecializationServiceDto>(),
                Diagnostics: new List<ServiceDto>(),
                Analyses: new List<ServiceDto>()
            );

            _mediatorMock
                .Setup(m => m.Send(It.IsAny<GetServicesQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(expectedDto);




            var result = await _serviceController.GetServices(CancellationToken.None);



            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;

            var returnData = okResult.Value.Should().BeAssignableTo<AllServicesDto>().Subject;
            returnData.Should().BeEquivalentTo(expectedDto);


            _mediatorMock.Verify(
                m => m.Send(It.IsAny<GetServicesQuery>(), It.IsAny<CancellationToken>()),
                Times.Once
            );

        }

    }
}
