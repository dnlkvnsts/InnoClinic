using FluentValidation;
using InnoClinic.Application.Features.Users.Commands.SignUp;
using InnoClinic.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace InnoClinic.Api.Controllers;


[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IMediator _mediator;

    public AuthController(IMediator mediator)
    {
        _mediator = mediator;
    }

    
    [HttpPost("signup")]
    public async Task<IActionResult> SignUp([FromBody] SignUpCommand request)
    {

        var result = await _mediator.Send(request);

        if (!result.IsSuccess)
        {
            return BadRequest(result.Errors);
        }


        return Ok("Registration successuful!!!");
    }
   

}
