using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InnoClinic.Appointments.Domain
{
    public record ServiceCreated(Guid Id, string ServiceName, decimal Price);
}
