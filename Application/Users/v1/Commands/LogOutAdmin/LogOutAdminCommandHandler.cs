using Domain.Entity.Users;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Users.v1.Commands.LogOutAdmin;

public class LogOutAdminCommandHandler(SignInManager<User> signInManager) : IRequestHandler<LogOutAdminCommand>
{
    public async Task Handle(LogOutAdminCommand request, CancellationToken cancellationToken)
    {
        await signInManager.SignOutAsync();
    }
}