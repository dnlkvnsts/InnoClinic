using InnoClinic.Application.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InnoClinic.Application.Features.Users.Commands.SignIn
{
    public class SignInCommandHandler : IRequestHandler<SignInCommand,string>
    {

        private readonly IIdentityService _identityService;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;


        public SignInCommandHandler(IIdentityService identityService, IJwtTokenGenerator jwtTokenGenerator)
        {
            _identityService = identityService;
            _jwtTokenGenerator = jwtTokenGenerator;
        }

        public async Task<string> Handle(SignInCommand request,CancellationToken cancellationToken)
        {
            var userExists = await _identityService.UserExistsAsync(request.Email);

            if (!userExists)
            {
               
                throw new Exception("User with this email doesn’t exist");
            }


            var (isSuccess, userId) = await _identityService.CheckPasswordAsync(request.Email, request.Password);

            if(!isSuccess || userId == null)
            {
                throw new Exception("Either an email or a password is incorrect");
            }

            var token = _jwtTokenGenerator.GenerateToken(userId, request.Email);

            return token;
        }
    }
}
