using InnoClinic.Services.Application.Contracts;
using InnoClinic.Services.Application.Interfaces;
using InnoClinic.Services.Domain.Entities;
using MassTransit;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InnoClinic.Services.Application.Features.Specializations.Commands.CreateSpec
{
    public  class CreateSpecializationCommandHandler : IRequestHandler<CreateSpecializationCommand, Guid>
    {

        private readonly ISpecializationRepository _repository;
        private readonly IPublishEndpoint _publishEndpoint;

        public CreateSpecializationCommandHandler(
            ISpecializationRepository repository,
            IPublishEndpoint publishEndpoint)
        {
            _repository = repository;
            _publishEndpoint = publishEndpoint;
        }

        public async Task<Guid> Handle(CreateSpecializationCommand request, CancellationToken cancellationToken)
        {
            var specialization = new Specialization
            {
                Id = Guid.NewGuid(),
                SpecializationName = request.SpecializationName,
                IsActive = true
            };



            await _repository.CreateSpecializationAsync(specialization, cancellationToken);

            await _publishEndpoint.Publish(
                new SpecializationCreated(specialization.Id, specialization.SpecializationName),
                cancellationToken
                );


            return specialization.Id;
        }
    }
}
