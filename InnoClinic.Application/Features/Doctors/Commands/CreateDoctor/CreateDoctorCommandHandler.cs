using InnoClinic.Shared.Contracts;
using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InnoClinic.Auth.Application.Features.Doctors.Commands.CreateDoctor
{
    public class CreateDoctorCommandHandler : IRequestHandler<CreateDoctorCommand, Guid>
    {
        private readonly UserManager<IdentityUser> _userManager; 
        private readonly IPublishEndpoint _publishEndpoint;

        public CreateDoctorCommandHandler(
            UserManager<IdentityUser> userManager,
            IPublishEndpoint publishEndpoint)
        {
            _userManager = userManager;
            _publishEndpoint = publishEndpoint;
        }

        public async Task<Guid> Handle(CreateDoctorCommand request, CancellationToken cancellationToken)
        {
          
            var existingUser = await _userManager.FindByEmailAsync(request.Email);
            if (existingUser != null)
            {
                throw new InvalidOperationException($"User with email '{request.Email}' already exists.");
            }

            var user = new IdentityUser
            {
                Id = Guid.NewGuid().ToString(),
                UserName = request.Email,
                Email = request.Email,
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(user, request.Password);
            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                throw new InvalidOperationException($"Failed to create doctor account: {errors}");
            }

         

            var accountId = Guid.Parse(user.Id);

     
            await _publishEndpoint.Publish(new DoctorAccountCreated(
                AccountId: accountId,
                FirstName: request.FirstName,
                LastName: request.LastName,
                MiddleName: request.MiddleName,
                DateOfBirth: request.DateOfBirth,
                PhotoUrl: request.PhotoUrl,
                CareerStartYear: request.CareerStartYear,
                Status: request.Status,
                SpecializationId: request.SpecializationId,
                OfficeAddress: request.OfficeAddress
            ), cancellationToken);

            return accountId;
        }
    }
}
