using InnoClinic.Auth.Application.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InnoClinic.Infrastructure.Services
{
    public class IdentityService : IIdentityService
    {
        private readonly UserManager<IdentityUser> _userManager;

        public IdentityService(UserManager<IdentityUser> userManager) => _userManager = userManager;


        public async Task<bool> IsEmailUniqueAsync(string email) => await _userManager.FindByEmailAsync(email) == null;


        public async Task<(bool IsSuccess, string? UserId, string[]? Errors)> CreateUserAsync(string email, string password)
        {
            var user = new IdentityUser { UserName = email, Email = email };
            var result = await _userManager.CreateAsync(user, password);
            return (result.Succeeded, user.Id, result.Errors.Select(e => e.Description).ToArray());
        }



        public async Task<(bool IsSuccess, string? UserId)> CheckPasswordAsync(string email, string password)
        {

            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return (false, null);
            }


            var isEmailConfirmed = await _userManager.IsEmailConfirmedAsync(user);
            if (!isEmailConfirmed)
            {

                return (false, null);
            }




            var isPasswordValid = await _userManager.CheckPasswordAsync(user, password);

            if (!isPasswordValid)
            {
                return (false, null);
            }

            return (true, user.Id);
        }


        public async Task<bool> UserExistsAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            return user != null;
        }



        public async Task<(bool IsSuccess, string? Token, string[]? Errors)> GenerateEmailConfirmationTokenAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
                return (false, null, new[] { "User not found" });

            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            return (true, token, null);
        }

        public async Task<(bool IsSuccess, string[]? Errors)> ConfirmEmailAsync(string userId, string token)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return (false, new[] { "User not found" });

            var result = await _userManager.ConfirmEmailAsync(user, token);
            return (result.Succeeded, result.Errors.Select(e => e.Description).ToArray());


        }

        public async Task<bool> IsEmailConfirmedAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null) return false;

            return await _userManager.IsEmailConfirmedAsync(user);
        }

        public async Task<(bool IsSuccess, string? UserId, string? Token, string[]? Errors)> GenerateEmailConfirmationTokenByEmailAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return (false, null, null, new[] { "Пользователь не найден." });
            }

            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);


            return (true, user.Id, token, null);
        }



        public async Task<(bool IsSuccess, string? UserId, string? GeneratedPassword, string[]? Errors)> CreateDoctorAsync(string email)
        {

            var existingUser = await _userManager.FindByEmailAsync(email);
            if (existingUser != null)
            {
                return (false, null, null, new[] { "User with this email already exists" });
            }


            var generatedPassword = GenerateRandomPassword();


            var user = new IdentityUser
            {
                UserName = email,
                Email = email,
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(user, generatedPassword);

            if (!result.Succeeded)
            {
                return (false, null, null, result.Errors.Select(e => e.Description).ToArray());
            }



            return (true, user.Id, generatedPassword, null);
        }


        private static string GenerateRandomPassword()
        {
            const string uppercase = "ABCDEFGHJKLMNOPQRSTUVWXYZ";
            const string lowercase = "abcdefghijkmnopqrstuvwxyz";
            const string digits = "0123456789";
            const string nonAlphanumeric = "!@#$%^&*";

            var random = new Random();
            var chars = new List<char>
            {
                uppercase[random.Next(uppercase.Length)],
                lowercase[random.Next(lowercase.Length)],
                digits[random.Next(digits.Length)],
                nonAlphanumeric[random.Next(nonAlphanumeric.Length)]
            };

            string allChars = uppercase + lowercase + digits + nonAlphanumeric;
            for (int i = chars.Count; i < 10; i++)
            {
                chars.Add(allChars[random.Next(allChars.Length)]);
            }

            return new string(chars.OrderBy(_ => random.Next()).ToArray());
        }

    }
}
