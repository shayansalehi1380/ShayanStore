using Domain.Entity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Application.Users.v1.Commands.LoginAdmin;
using MediatR;

namespace AdminPanel.UI.Controllers
{
    public class AdminController(
        UserManager<User> userManager,
        SignInManager<User> signInManager,
        IMediator mediator,
        RoleManager<Role> roleManager)
        : Controller
    {
        public IActionResult AdminLogin()
        {
            return View();
        }

        public async Task CreateAdmin()
        {
            var user = new User
            {
                UserName = "admin",
                Email = "shayansalehi617@gmail.com",
                CityId = 1,
                ImageName = "admin.png",
                InsertDate = DateTime.UtcNow,
                Sheba = string.Empty,
                NationalCode = string.Empty,
                Name = "admin",
                PhoneNumber = "09015538133",
                ConcurrencyStamp = string.Empty,
                SecurityStamp = string.Empty,
                Family = string.Empty,
            };
            await userManager.CreateAsync(user);
            await userManager.AddPasswordAsync(user, "09011155");
            if (!roleManager.Roles.Any(x => x.Name == "Admin"))
            {
                await roleManager.CreateAsync(new Role
                {
                    Name = "Admin",
                    NormalizedName = "ADMIN"
                });
            }

            await userManager.AddToRoleAsync(user, "Admin");
        }

        [HttpPost]
        public async Task<IActionResult> AdminLogin(LoginAdminCommand request)
        {
            // var result = await mediator.Send(new LoginAdminCommand
            // {
            //     Email = request.Email,
            //     Password = request.Password
            // });
            string result = string.Empty;
            if (string.IsNullOrEmpty(request.Email) || string.IsNullOrEmpty(request.Password))
                result = "نام کاربری یا رمز عبور وارد نشده است";

            var user = await userManager.FindByEmailAsync(request.Email);
            if (user == null || !await userManager.CheckPasswordAsync(user, request.Password))
                result = "نام کاربری یا رمز عبور یافت نشد";

            if (!await userManager.IsInRoleAsync(user, "Admin"))
                result = "شما اجازه دسترسی به این بخش را ندارید";

            var resultIden =
                await signInManager.PasswordSignInAsync(user, request.Password, isPersistent: false,
                    lockoutOnFailure: false);
            if (!resultIden.Succeeded)
                result = "خطا در ورود به سیستم";
            if (!string.IsNullOrEmpty(result))
            {
                return RedirectToAction("AdminDashboard");
            }

            return RedirectToAction("AdminLogin");
        }

        public IActionResult AdminDashboard()
        {
            return View();
        }
    }
}