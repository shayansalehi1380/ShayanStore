using Domain.Entity;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Users.v1.Commands.LoginAdmin;

public class LoginAdminCommandHandler(UserManager<User> userManager, SignInManager<User> signInManager)
    : IRequestHandler<LoginAdminCommand, string>
{
    public async Task<string> Handle(LoginAdminCommand request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(request.Username) || string.IsNullOrEmpty(request.Password))
            return "نام کاربری یا رمز عبور وارد نشده است";

        var user = await userManager.FindByEmailAsync(request.Username);
        if (user == null || !await userManager.CheckPasswordAsync(user, request.Password))
            return "نام کاربری یا رمز عبور یافت نشد";

        if (!await userManager.IsInRoleAsync(user, "Admin"))
            return "شما اجازه دسترسی به این بخش را ندارید";

        var result =
            await signInManager.PasswordSignInAsync(user, request.Password, isPersistent: false,
                lockoutOnFailure: false);
        if (!result.Succeeded)
            return "خطا در ورود به سیستم";

        return string.Empty;
    }
}