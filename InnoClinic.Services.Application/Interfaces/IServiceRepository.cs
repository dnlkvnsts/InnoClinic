using InnoClinic.Services.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InnoClinic.Services.Application.Interfaces
{
    public  interface  IServiceRepository
    {
        IQueryable<Service> GetServicesQuery();
    }
}
