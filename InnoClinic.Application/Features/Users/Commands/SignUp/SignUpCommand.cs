using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InnoClinic.Application.Features.Users.Queries.SignUp
{
    public record SignUpRequest(string Email, string Password, string ReEnteredPassword)
    {
    }
}
