using InnoClinic.Profiles.Application.Features.Doctors.Queries.GetDoctors;
using InnoClinic.Profiles.Application.Features.Patients.Commands.CreatePatient;
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

    }
}
