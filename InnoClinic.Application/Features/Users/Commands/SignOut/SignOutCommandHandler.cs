using InnoClinic.Application.Features.Users.Commands.SignIn;
using InnoClinic.Application.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InnoClinic.Application.Features.Users.Commands.SignOut
{
    public  class SignOutCommandHandler : IRequestHandler<SignOutCommand, bool>
    {

        public async Task<bool> Handle(SignOutCommand request, CancellationToken cancellationToken)
        {
            return await Task.FromResult(true);
        }


    }
}
