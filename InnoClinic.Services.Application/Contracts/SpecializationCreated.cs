using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InnoClinic.Services.Application.Contracts
{
    public record SpecializationCreated(Guid Id, string SpecializationName);
}
