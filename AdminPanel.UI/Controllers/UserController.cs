using Application.Interface;
using Domain.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace AdminPanel.UI.Controllers
{
    public class UserController(UserManager<User> userManager, SignInManager<User> signInManager) : Controller
    {
        public async Task<IActionResult> GetAllUser()
        {
            ViewBag.Users = await userManager.Users.ToListAsync();
            return View();
        }

        public async Task<IActionResult> Create(string name)
        {
            var newUser = new User
            {
                UserName = name,
                Family = string.Empty,
                Email = string.Empty,
                PhoneNumber = string.Empty,
                Name = string.Empty,
                CityId = 1,
                Sheba = string.Empty,
                ImageName = string.Empty,
                InsertDate = DateTime.Now,
                NationalCode = string.Empty,
                ConcurrencyStamp = string.Empty,
                SecurityStamp = string.Empty,
                ConfirmCode = string.Empty,
            };

            var result = await userManager.CreateAsync(newUser);

            if (result.Succeeded)
            {
                return RedirectToAction("GetAllUser");
            }

            return View("GetAllUser");
        }


        public async Task<IActionResult> Delete(int userId)
        {
            var user = await userManager.FindByIdAsync(userId.ToString());
            if (user == null)
            {
                ModelState.AddModelError("", "کاربر یافت نشد");
                return RedirectToAction("GetAllUser");
            }

            user.IsDelete = true;
            var result = await userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                return RedirectToAction("GetAllUser");
            }

            return View("GetAllUser");
        }
    }
}