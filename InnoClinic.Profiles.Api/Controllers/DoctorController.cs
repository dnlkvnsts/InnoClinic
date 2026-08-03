using InnoClinic.Profiles.Application.Features.Doctors.Queries.GetDoctors;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InnoClinic.Profiles.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DoctorController : ControllerBase
    {
        private readonly IMediator _mediator;

        public DoctorController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpGet]


        public async Task<IActionResult> GetDoctors([FromQuery] GetDoctorsQuery query)
        {
            var doctors = await _mediator.Send(query);

            return Ok(doctors);
        }



    }

}
