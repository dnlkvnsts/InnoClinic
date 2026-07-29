using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InnoClinic.Profiles.Application.DTOs
{
    public record PatientMatchResultDto(
         bool IsMatchFound,
         PatientDto? MatchedProfile,
         Guid? MatchedPatientId
     );
}
