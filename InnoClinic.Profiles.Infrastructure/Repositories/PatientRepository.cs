using InnoClinic.Profiles.Application.Interfaces;
using InnoClinic.Profiles.Domain.Entities;
using InnoClinic.Profiles.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace InnoClinic.Profiles.Infrastructure.Repositories
{
    public class PatientRepository : IPatientRepository
    {

        private readonly ProfilesDbContext _context;

        public PatientRepository (ProfilesDbContext context)
        {
            _context = context;
        }


        public IQueryable<Patient> GetPatientsQuery()
        {
            return _context.Patients.AsNoTracking();
        }

        public async Task AddAsync(Patient patient, CancellationToken cancellationToken)
        {
            await _context.Patients.AddAsync(patient, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }



    }
}
