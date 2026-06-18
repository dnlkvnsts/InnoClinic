using FluentValidation;
using InnoClinic.Application.Features.Users.Queries.SignUp;
using InnoClinic.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace InnoClinic.Api.Controllers;


[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    
    [HttpPost("signup")]

    public async Task<IActionResult> SignUp(
        [FromBody] SignUpRequest request,
        [FromServices] IValidator<SignUpRequest> validator,
        [FromServices] IIdentityService identityService,
        [FromServices] IEmailService emailService)
    {
        var validationResult = await validator.ValidateAsync(request);
        if (!validationResult.IsValid) return BadRequest(validationResult.Errors.Select(e => e.ErrorMessage));

        var result = await identityService.CreateUserAsync(request.Email, request.Password);
        if(!result.IsSuccess) return BadRequest(result.Errors);

        await emailService.SendConfirmationEmailAsync(request.Email);

        return Ok("Registration successuful!!!Confirmation email sent");
    }

}
