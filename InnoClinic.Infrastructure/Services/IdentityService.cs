using InnoClinic.Domain.Interfaces;
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


        public async Task<bool> IsEmailUniqueAsync(string email) => _userManager.FindByEmailAsync(email) == null ;


        public async Task<(bool IsSuccess, string? UserId, string[]? Errors)> CreateUserAsync(string email, string password)
        {
            var user = new IdentityUser { UserName = email, Email = email };
            var result = await _userManager.CreateAsync(user, password);
            return (result.Succeeded, user.Id, result.Errors.Select(e => e.Description).ToArray());
        }
    }
}
