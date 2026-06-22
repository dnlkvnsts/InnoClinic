using FluentValidation;
using InnoClinic.Application.Features.Users.Commands.SignIn;
using InnoClinic.Application.Features.Users.Commands.SignUp;
using InnoClinic.Application.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Authorization;
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


    [HttpPost("signin")]
    public async Task<IActionResult> SignIn([FromBody] SignInCommand request)
    {
        try
        {
            var token = await _mediator.Send(request);

            return Ok(new { Token = token });


        }
        catch (Exception ex) 
        {
            return BadRequest(new { Message = ex.Message});

        }
    }

   



}
