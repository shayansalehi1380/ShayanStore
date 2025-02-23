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
        public async Task<IActionResult> AdminLogin(LoginAdminCommand request)
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

        public IActionResult AdminDashboard()
        {
            return View();
        }
    }
}