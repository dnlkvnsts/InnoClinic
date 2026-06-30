using InnoClinic.Services.Application.Interfaces;
using InnoClinic.Services.Domain.Entities;
using InnoClinic.Services.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InnoClinic.Services.Infrastructure.Repositories
{
    public  class SpecializationRepository : ISpecializationRepository
    {
        private readonly ServicesDbContext _context;

        public SpecializationRepository(ServicesDbContext context)
        {
            _context = context;
        }


        public async Task CreateSpecializationAsync(Specialization specialization, CancellationToken cancellationToken)
        {

            await _context.Specializations.AddAsync(specialization, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

        }

    }
}
