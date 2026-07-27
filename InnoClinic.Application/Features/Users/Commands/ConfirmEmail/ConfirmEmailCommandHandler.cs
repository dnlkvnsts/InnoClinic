using InnoClinic.Application.Interfaces;
using MediatR;


namespace InnoClinic.Auth.Application.Features.Users.Commands.ConfirmEmail
{
    public class ConfirmEmailCommandHandler : IRequestHandler<ConfirmEmailCommand, ConfirmEmailResponse>
    {
        private readonly IIdentityService _identityService;

        public ConfirmEmailCommandHandler(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        public async Task<ConfirmEmailResponse> Handle(ConfirmEmailCommand request, CancellationToken cancellationToken)
        {
            var (isSuccess, errors) = await _identityService.ConfirmEmailAsync(request.UserId, request.Token);

            return new ConfirmEmailResponse(isSuccess, errors);
        }
    }
}
