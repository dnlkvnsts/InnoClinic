
using InnoClinic.Profiles.Application.Features.Patients.Commands.CreatePatient;
using InnoClinic.Profiles.Application.Features.Patients.Commands.LinkExistingPatient;
using InnoClinic.Profiles.Application.Features.Patients.Queries.FindPatient;
using InnoClinic.Profiles.Application.Features.Patients.Queries.GetPatients;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace InnoClinic.Profiles.Api.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class PatientController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PatientController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpGet]


        public async Task<IActionResult> GetPatients()
        {
            var query = new GetPatientsQuery();
            var patients = await _mediator.Send(query);

            return Ok(patients);
        }



        [HttpPost("createpatient")]
        public async Task<IActionResult> CreatePatient([FromBody] CreatePatientCommand request, CancellationToken cancellationToken)
        {
            Guid patientId = await _mediator.Send(request, cancellationToken);
            return Ok(new { Id = patientId });

        }

        [HttpPost("check-match")]
        public async Task<IActionResult> CheckMatch([FromBody] FindPatientQuery query, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(query, cancellationToken);
            return Ok(result);
        }


        [HttpPost("link-existing")]
        public async Task<IActionResult> LinkExisting([FromBody] LinkExistingPatientCommand request, CancellationToken cancellationToken)
        {
            bool success = await _mediator.Send(request, cancellationToken);
            if (!success) return NotFound("Patient not found");
            return Ok();
        }

    }
}
