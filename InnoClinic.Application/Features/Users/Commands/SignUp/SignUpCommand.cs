using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InnoClinic.Application.Features.Users.Commands.SignUp
{
    public record SignUpCommand(string Email, string Password, string ReEnteredPassword) : IRequest<SignUpResponse>;

    public record SignUpResponse(bool IsSuccess, string[]? Errors);
   
}
