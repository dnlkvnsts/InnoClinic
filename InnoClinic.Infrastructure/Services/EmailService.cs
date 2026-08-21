using InnoClinic.Auth.Application.Interfaces;
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

        public async Task SendDoctorWelcomeEmailAsync(string email, string password, string confirmationLink)
        {
            Console.WriteLine("==================================================");
            Console.WriteLine($"[DOCTOR WELCOME EMAIL SENT TO {email}]");
            Console.WriteLine($"Your Temporary Password: {password}");
            Console.WriteLine($"Please confirm registration: {confirmationLink}");
            Console.WriteLine("==================================================");

            await Task.CompletedTask;
        }
    }
}
