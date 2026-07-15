using InnoClinic.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InnoClinic.Auth.Infrastructure.Services
{
    public  class EmailService : IEmailService
    {
        public async Task SendConfirmationEmailAsync(string email, string confirmationLink)
        {
            Console.WriteLine("==================================================");
            Console.WriteLine($"[EMAIL SENT TO {email}]");
            Console.WriteLine($"Please confirm registration: {confirmationLink}");
            Console.WriteLine("==================================================");

            await Task.CompletedTask;
        }
    }
}
