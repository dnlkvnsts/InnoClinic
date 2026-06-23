using InnoClinic.Profiles.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InnoClinic.Profiles.Application.Interfaces
{
    public interface IDoctorRepository
    {
        IQueryable<Doctor> GetDoctorsQuery();

    }
}
