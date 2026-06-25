using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InnoClinic.Services.Application.DTOs
{
    public record AllServicesDto(
        List<SpecializationServiceDto> Consultations, 
        List<ServiceDto> Diagnostics,                 
        List<ServiceDto> Analyses                    
    );

}
