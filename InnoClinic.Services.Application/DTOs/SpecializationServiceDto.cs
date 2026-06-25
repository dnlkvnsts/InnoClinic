using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InnoClinic.Services.Application.DTOs
{
    public record SpecializationServiceDto(

         Guid SpecializationId,
         string SpecializationName,
         List<ServiceDto> Services
        );
   
}
