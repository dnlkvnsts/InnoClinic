using InnoClinic.Services.Application.Interfaces;
using InnoClinic.Services.Domain.Entities;
using InnoClinic.Services.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InnoClinic.Services.Infrastructure.Repositories
{
    public class ServiceRepository : IServiceRepository
    {

        private readonly ServicesDbContext _context;

        public ServiceRepository(ServicesDbContext context)
        {
            _context = context;
        }


  

        public async Task<List<Service>> GetServicesAsync(CancellationToken cancellationToken)
        {
            return await  _context
                .Services
                .Include(s => s.Category)
                .Include(s => s.Specialization)
                .Where(s => s.IsActive && (s.Specialization == null || s.Specialization.IsActive))
                .ToListAsync(cancellationToken);
        }

    }
}
