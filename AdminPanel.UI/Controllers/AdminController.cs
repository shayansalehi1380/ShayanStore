using Domain.Entity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Application.Users.v1.Commands.LoginAdmin;
using MediatR;

namespace AdminPanel.UI.Controllers
{
    public class AdminController(UserManager<User> userManager, SignInManager<User> signInManager, IMediator mediator)
        : Controller
    {
        public IActionResult AdminLogin()
        {
            return View();
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

        public async Task<IActionResult> AdminLoginClean(string email, string password)
        {
            var result = await mediator.Send(new LoginAdminCommand
            {
                Email = email,
                Password = password
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