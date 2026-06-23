using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InnoClinic.Application.Features.Users.Commands.SignIn
{
    public record SignInCommand(string Email, string Password) : IRequest<string>;
   
}
