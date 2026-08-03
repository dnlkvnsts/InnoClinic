using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InnoClinic.Auth.Application.Features.Users.Commands.ConfirmEmail
{
    public record ConfirmEmailCommand(string UserId, string Token) : IRequest<ConfirmEmailResponse>;

    public record ConfirmEmailResponse(bool IsSuccess, string[]? Errors);
}
