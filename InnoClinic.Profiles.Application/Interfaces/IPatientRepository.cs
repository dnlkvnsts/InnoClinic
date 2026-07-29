using InnoClinic.Profiles.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InnoClinic.Profiles.Application.Interfaces
{
    public interface IPatientRepository
    {

        IQueryable<Patient> GetPatientsQuery();

        Task AddAsync(Patient patient, CancellationToken cancellationToken);


        Task<List<Patient>> GetUnlinkedPatientsAsync(CancellationToken cancellationToken);
        Task<Patient?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
        Task UpdateAsync(Patient patient, CancellationToken cancellationToken);

    }
}
