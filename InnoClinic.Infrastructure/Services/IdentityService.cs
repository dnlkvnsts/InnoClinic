using InnoClinic.Application.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InnoClinic.Infrastructure.Services
{
    public  class IdentityService : IIdentityService
    {
        private readonly UserManager<IdentityUser> _userManager;

        public IdentityService(UserManager<IdentityUser> userManager) => _userManager = userManager;


        public async Task<bool> IsEmailUniqueAsync(string email) =>  await _userManager.FindByEmailAsync(email) == null ;


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
    }
}
