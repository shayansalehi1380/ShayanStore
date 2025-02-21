using Domain.Entity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AdminPanel.UI.Controllers
{
    public class AdminController : Controller
    {
        private UserManager<User> _userManager;

        public AdminController(UserManager<User> userManager)
        {
            _userManager = userManager;
        }
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string email ,string password)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                ModelState.AddModelError("","نام کاربری یا رمز عبور وارد نشده است");
                return View();
            }

            var user = await _userManager.FindByEmailAsync(email);
            
            if (user == null)
            {
                ModelState.AddModelError("", "نام کاربری یا رمز عبور یافت نشد");
                return View();
            }

            var isPasswordValid = await _userManager.CheckPasswordAsync(user, password);

            if (!isPasswordValid)
            {
                ModelState.AddModelError("", "نام کاربری یا رمز عبور یافت نشد");
                return View();
            }

            return RedirectToAction("AdminDashboard");
        }

        public IActionResult AdminDashboard()
        {
            return View();
        }
    }
}