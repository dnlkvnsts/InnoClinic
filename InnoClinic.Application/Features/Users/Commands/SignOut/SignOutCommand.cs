using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InnoClinic.Auth.Application.Features.Users.Commands.SignOut
{
    public record SignOutCommand : IRequest<bool>;
   
}
