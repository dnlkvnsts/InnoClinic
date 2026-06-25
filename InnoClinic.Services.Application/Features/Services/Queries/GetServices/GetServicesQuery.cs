using InnoClinic.Services.Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InnoClinic.Services.Application.Features.Services.Queries.GetServices
{
  
        public record GetServicesQuery() : IRequest<AllServicesDto>;
   
}
