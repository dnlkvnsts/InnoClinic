using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InnoClinic.Application.Interfaces
{
    public interface IEmailService
    {
        Task SendConfirmationEmailAsync(string email);
    }
}
