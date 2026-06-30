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


        public IQueryable<Doctor> GetDoctorsQuery()
        {
            return _context.Doctors.Include(d => d.Specialization).AsNoTracking();
        }


    }
}
