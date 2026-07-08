using InnoClinic.Appointments.Application.Features.Appointments.Commands.CreateAppointment;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace InnoClinic.Appointments.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AppointmentController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AppointmentController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpPost("createappointment")]
        public async Task<IActionResult> CreateAppointment([FromBody] CreateAppointmentCommand request, CancellationToken cancellationToken)
        {
            Guid appointmentid =  await _mediator.Send(request);
            return Ok(new {Id =  appointmentid});

        }

    }
}
