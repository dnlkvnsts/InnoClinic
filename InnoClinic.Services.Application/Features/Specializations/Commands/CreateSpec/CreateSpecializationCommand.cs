using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InnoClinic.Services.Application.Features.Specializations.Commands.CreateSpec
{
   public record CreateSpecializationCommand (string SpecializationName) : IRequest<Guid>;

}
