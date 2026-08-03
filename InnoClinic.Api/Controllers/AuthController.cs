using FluentValidation;
using InnoClinic.Auth.Application.Features.Users.Commands.SignIn;
using InnoClinic.Auth.Application.Features.Users.Commands.SignOut;
using InnoClinic.Auth.Application.Features.Users.Commands.SignUp;
using InnoClinic.Auth.Application.Features.Users.Commands.ConfirmEmail;
using InnoClinic.Auth.Application.Features.Users.Commands.ResendEmail;
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

    [HttpGet("confirm-email")]

    public async Task<IActionResult> ConfirmEmail([FromQuery] string userId, [FromQuery] string token)
    {

        if (string.IsNullOrWhiteSpace(userId) || string.IsNullOrWhiteSpace(token))
        {
            return BadRequest("User ID and Token are required.");
        }

        var result = await _mediator.Send(new ConfirmEmailCommand(userId, token));


        if (!result.IsSuccess)
        {
            return BadRequest(result.Errors);
        }

        return Ok("Email confirmed successfully! You can now sign in.");

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

    [Authorize]
    [HttpPost("signout")]
    public async Task<IActionResult> SignOut()
    {
        try
        {
            await _mediator.Send(new SignOutCommand());
            return Ok(new { Message = "Logged out successfully. Please delete the token on the client side." });
        }
        catch (Exception ex)
        {
            return BadRequest(new { Message = ex.Message });
        }
    }

    [HttpPost("resend-confirmation-email")]
    public async Task<IActionResult> ResendConfirmationEmail([FromBody] ResendEmailCommand command)
    {
        var result = await _mediator.Send(command);

        if (!result)
        {
            return BadRequest(new { Message = "Failed to send confirmation email." });
        }

        return Ok(new { Message = "If the account is registered and not yet confirmed, a confirmation link has been sent to the email." });
    }

}
