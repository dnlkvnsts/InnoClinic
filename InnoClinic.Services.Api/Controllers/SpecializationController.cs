using InnoClinic.Services.Application.Features.Specializations.Commands.CreateSpec;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace InnoClinic.Services.Api.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class SpecializationController : ControllerBase
    {
        private readonly IMediator _mediator;

        public SpecializationController(IMediator mediator)
        {
            _mediator = mediator;
        }



        [HttpPost]
        public async Task<IActionResult> CreateSpecialization([FromBody] CreateSpecializationCommand command)
        {
            var specializationId = await _mediator.Send(command);
            return Ok(specializationId);

        }


    }
}
