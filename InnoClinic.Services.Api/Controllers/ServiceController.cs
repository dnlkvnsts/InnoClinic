using InnoClinic.Services.Application.Features.Services.Queries.GetServices;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace InnoClinic.Services.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ServiceController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ServiceController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpGet]
        public async Task<IActionResult> GetDoctors(CancellationToken cancellationToken)
        {

            var query = new GetServicesQuery();
            var services = await _mediator.Send(query, cancellationToken);
            return Ok(services);
        }



    }

}
