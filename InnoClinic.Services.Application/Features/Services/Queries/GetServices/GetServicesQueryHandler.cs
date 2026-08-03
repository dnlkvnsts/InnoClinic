using InnoClinic.Services.Application.DTOs;
using InnoClinic.Services.Application.Interfaces;
using MassTransit;
using InnoClinic.Appointments.Domain;
using MediatR;


namespace InnoClinic.Services.Application.Features.Services.Queries.GetServices
{
    public class GetServicesQueryHandler : IRequestHandler<GetServicesQuery, AllServicesDto>
    {
        private readonly IServiceRepository _serviceRepository;
        private readonly IPublishEndpoint _publishEndpoint;

        public GetServicesQueryHandler(IServiceRepository serviceRepository, IPublishEndpoint publishEndpoint)
        {
            _serviceRepository = serviceRepository;
            _publishEndpoint = publishEndpoint;
        }
       


        public async Task<AllServicesDto> Handle(GetServicesQuery request, CancellationToken cancellationToken)
        {
            var services = await _serviceRepository.GetServicesAsync(cancellationToken);


            foreach (var service in services)
            {
                await _publishEndpoint.Publish(new ServiceCreated(service.Id, service.ServiceName, service.Price),cancellationToken);
            }


            var consultations = services
                .Where(s => s.Category.CategoryName.Equals("consultations", StringComparison.OrdinalIgnoreCase))
                .GroupBy(s => s.Specialization)
                .Where(g => g.Key != null)
                .Select(g => new SpecializationServiceDto(g.Key!.Id, g.Key.SpecializationName, g.Select(s => new ServiceDto(s.Id, s.ServiceName, s.Price)).ToList()))
                .ToList();

            var diagnostics = services
                .Where(s => s.Category.CategoryName.Equals("diagnostics", StringComparison.OrdinalIgnoreCase))
                .Select(s => new ServiceDto(s.Id, s.ServiceName, s.Price)).ToList();
            

            var analyses = services
                .Where(s => s.Category.CategoryName.Equals("analyses", StringComparison.OrdinalIgnoreCase))
                .Select(s => new ServiceDto(s.Id, s.ServiceName, s.Price)).ToList();
            ;

            return new AllServicesDto(consultations, diagnostics, analyses);
        }

    }
}
