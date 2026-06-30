using InnoClinic.Profiles.Application.Interfaces;
using InnoClinic.Profiles.Domain.Entities;
using InnoClinic.Profiles.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InnoClinic.Profiles.Infrastructure.Repositories
{
    public class DoctorRepository : IDoctorRepository
    {
        private readonly ProfilesDbContext _context;

        public DoctorRepository(ProfilesDbContext context)
        {
            _context = context;
        }


        public async Task<List<Doctor>> GetDoctorsAsync(string? fullName, Guid? specializationId, CancellationToken cancellationToken)
        {
            var doctors =  _context.Doctors.Include(d => d.Specialization).AsNoTracking();


            doctors = doctors.Where(d => d.Status == "At work");



            if (!string.IsNullOrEmpty(fullName))
            {
                var searchTerm = fullName.Trim().ToLower();
                doctors = doctors.Where(
                    d => d.FirstName.ToLower().Contains(searchTerm) ||
                    d.LastName.ToLower().Contains(searchTerm) ||
                    (d.MiddleName != null && d.LastName.ToLower().Contains(searchTerm))

                    );
            }

            if (specializationId.HasValue)
            {
                doctors = doctors.Where(d => d.SpecializationId == specializationId);
            }

            return await  doctors.ToListAsync(cancellationToken);

        }

    }
}
