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
        Task<List<Doctor>> GetDoctorsAsync(string? fullName, Guid? specializationId, CancellationToken cancellationToken);

        Task AddAsync(Doctor doctor, CancellationToken cancellationToken);

        Task<Doctor?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
        Task UpdateAsync(Doctor doctor, CancellationToken cancellationToken);

    }
}
