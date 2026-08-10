using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InnoClinic.Auth.Application.Interfaces
{
    public interface  IIdentityService
    {
        Task<bool> IsEmailUniqueAsync(string email);

        Task<(bool IsSuccess,string? UserId, string[]? Errors)> CreateUserAsync(string email, string password);

        Task<(bool IsSuccess, string? UserId)> CheckPasswordAsync(string email, string password);

        Task<bool> UserExistsAsync(string email);

        Task<(bool IsSuccess, string? Token, string[]? Errors)> GenerateEmailConfirmationTokenAsync(string userId);

        Task<(bool IsSuccess, string[]? Errors)> ConfirmEmailAsync(string userId, string token);

        Task<bool> IsEmailConfirmedAsync(string email);

        Task<(bool IsSuccess, string? UserId, string? Token, string[]? Errors)> GenerateEmailConfirmationTokenByEmailAsync(string email);

        Task<(bool IsSuccess, string? UserId, string? GeneratedPassword, string[]? Errors)> CreateDoctorAsync(string email);

    }
}
