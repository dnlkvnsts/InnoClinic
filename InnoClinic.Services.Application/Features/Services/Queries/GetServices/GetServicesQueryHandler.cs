using InnoClinic.Services.Application.DTOs;
using InnoClinic.Services.Application.Interfaces;
using MediatR;
using System.Linq;


namespace InnoClinic.Services.Application.Features.Services.Queries.GetServices
{
    public class GetServicesQueryHandler : IRequestHandler<GetServicesQuery, AllServicesDto>
    {
        private readonly IServiceRepository _serviceRepository;


        public GetServicesQueryHandler(IServiceRepository serviceRepository)
        {
            _serviceRepository = serviceRepository;
        }
       


        public async Task<AllServicesDto> Handle(GetServicesQuery request, CancellationToken cancellationToken)
        {
            var services = await _serviceRepository.GetServicesAsync(cancellationToken);

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
