using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InnoClinic.Application.Interfaces
{
    public interface  IIdentityService
    {
        Task<bool> IsEmailUniqueAsync(string email);

        Task<(bool IsSuccess,string? UserId, string[]? Errors)> CreateUserAsync(string email, string password);

        Task<(bool IsSuccess, string? UserId)> CheckPasswordAsync(string email, string password);

        Task<bool> UserExistsAsync(string email);
    }
}
