using Domain.Entity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Application.Users.v1.Commands.LoginAdmin;
using MediatR;

namespace AdminPanel.UI.Controllers
{
    public class AdminController(UserManager<User> userManager, SignInManager<User> signInManager, IMediator mediator, RoleManager<Role> roleManager)
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
                Email = "admin@admin.com",
                CityId = 1,
                ImageName = "admin.png",
                InsertDate = DateTime.Now,
                Sheba = string.Empty,
                NationalCode = string.Empty,
                Name = "admin",
                PhoneNumber = "0921342141",
                ConcurrencyStamp = string.Empty,
                SecurityStamp = string.Empty,
                Family = string.Empty,
            };
            await userManager.CreateAsync(user);
            await userManager.AddPasswordAsync(user, "123456");
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
        public async Task<IActionResult> AdminLogin(string email, string password)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                ModelState.AddModelError("", "نام کاربری یا رمز عبور وارد نشده است");
                return View();
            }

            var user = await userManager.FindByEmailAsync(email);

            if (user == null)
            {
                ModelState.AddModelError("", "نام کاربری یا رمز عبور یافت نشد");
                return View();
            }

            var isPasswordValid = await userManager.CheckPasswordAsync(user, password);

            if (!isPasswordValid)
            {
                ModelState.AddModelError("", "نام کاربری یا رمز عبور یافت نشد");
                return View();
            }

            // بررسی نقش کاربر (اگر نیاز باشد)
            var isAdmin = await userManager.IsInRoleAsync(user, "Admin");
            if (!isAdmin)
            {
                ModelState.AddModelError("", "شما اجازه دسترسی به این بخش را ندارید");
                return View();
            }

            // ورود کاربر
            var result =
                await signInManager.PasswordSignInAsync(user, password, isPersistent: false, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                return RedirectToAction("AdminDashboard");
            }

            ModelState.AddModelError("", "خطا در ورود به سیستم");
            return View();
        }

        public IActionResult AdminDashboard()
        {
            return View();
        }

        public async Task<IActionResult> AdminLoginClean(LoginAdminCommand request)
        {
            var result = await mediator.Send(new LoginAdminCommand
            {
                Email = request.Email,
                Password = request.Password
            });
            if (!string.IsNullOrEmpty(result))
            {
                return RedirectToAction("AdminDashboard");
            }
            return RedirectToAction("AdminLogin");
        }

// متد کمکی برای افزودن پیام خطا و بازگرداندن View          
        private IActionResult ReturnWithError(string message)
        {
            ModelState.AddModelError("", message);
            return View();
        }
    }
}