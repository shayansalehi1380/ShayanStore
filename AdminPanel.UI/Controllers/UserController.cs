using Application.Interface;
using Domain.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Application.Dtos.Users;
namespace AdminPanel.UI.Controllers
{
    public class UserController(UserManager<User> userManager, RoleManager<Role> roleManager, IUnitOfWork work) : Controller
    {

        public async Task<IActionResult> GetAllUser(string? search)
        {
            var users = new List<UserDto>();
            var query = userManager.Users.AsTracking()
                .Include(c => c.City).AsQueryable();

            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(x => x.PhoneNumber.Contains(search) || x.Name.Contains(search) || x.UserName.Contains(search));
            }

            var entityUsers = await query.ToListAsync();
            ViewBag.Roles = await roleManager.Roles.ToListAsync();
            ViewBag.Cities = await work.GenericRepository<City>().TableNoTracking.ToListAsync();

            //var newUser = new User
            //{
            //    UserName = "09015538133",
            //    Family = string.Empty,
            //    Email = string.Empty,
            //    PhoneNumber = string.Empty,
            //    Name = string.Empty,
            //    CityId = 1,
            //    Sheba = string.Empty,
            //    ImageName = string.Empty,
            //    InsertDate = DateTime.Now,
            //    NationalCode = string.Empty,
            //    ConcurrencyStamp = string.Empty,
            //    SecurityStamp = string.Empty,
            //    ConfirmCode = string.Empty,
            //};
            //await userManager.CreateAsync(newUser);
            //var newUser1 = new User
            //{
            //    UserName = "0921129482",
            //    Family = string.Empty,
            //    Email = string.Empty,
            //    PhoneNumber = string.Empty,
            //    Name = string.Empty,
            //    CityId = 1,
            //    Sheba = string.Empty,
            //    ImageName = string.Empty,
            //    InsertDate = DateTime.Now,
            //    NationalCode = string.Empty,
            //    ConcurrencyStamp = string.Empty,
            //    SecurityStamp = string.Empty,
            //    ConfirmCode = string.Empty,
            //};
            //await userManager.CreateAsync(newUser1);

            //if (!await roleManager.RoleExistsAsync("Admin"))
            //{
            //    await roleManager.CreateAsync(new Role
            //    {
            //        Name = "Admin",
            //    });
            //}
            //if (!await roleManager.RoleExistsAsync("User"))
            //{
            //    await roleManager.CreateAsync(new Role
            //    {
            //        Name = "User",
            //    });
            //}
            //await userManager.AddToRoleAsync(newUser, "Admin");
            //await userManager.AddToRoleAsync(newUser1, "Admin");
            //await userManager.AddToRoleAsync(newUser1, "User");
            //await userManager.AddToRoleAsync(newUser, "User");



            foreach (var i in entityUsers)
            {

                users.Add(new UserDto
                {
                    City = i.City,
                    Email = i.Email,
                    Family = i.Family,
                    InsertDate = i.InsertDate,
                    Name = i.Name,
                    PhoneNumber = i.PhoneNumber,
                    Roles = await userManager.GetRolesAsync(i),
                    UserName = i.UserName,
                    Id = i.Id
                });
            }
            ViewBag.Users = users;

            return View();
        }

        public async Task<IActionResult> Create(UserDto request)
        {
            var newUser = new User
            {
                UserName = request.PhoneNumber,
                Family = request.Family,
                Email = request.Email,
                PhoneNumber = request.PhoneNumber,
                Name = request.Name,
                CityId = request.CityId,
                Sheba = string.Empty,
                ImageName = string.Empty,
                InsertDate = DateTime.Now,
                NationalCode = string.Empty,
                ConcurrencyStamp = string.Empty,
                SecurityStamp = string.Empty,
                ConfirmCode = string.Empty,
            };

            var result = await userManager.CreateAsync(newUser);

            foreach(var i in request.Roles)
            {
                await userManager.AddToRoleAsync(newUser, i);
            }

            if (result.Succeeded)
            {
                return RedirectToAction("ManageUser","Admin");
            }

            return View("ManageUser", "Admin");
        }


        public async Task<IActionResult> Update(UserDto request) 
        {
            var user = await userManager.FindByIdAsync(request.Id.ToString());

            if (user == null)
            {
                ModelState.AddModelError("", "کاربر یافت نشد");
                return RedirectToAction("ManageUser", "Admin");
            }

            user.UserName = request.PhoneNumber;
            user.Family = request.Family;
            user.Email = request.Email;
            user.PhoneNumber = request.PhoneNumber;
            user.Name = request.Name;
            user.CityId = request.CityId;

            var result = await userManager.UpdateAsync(user);

            if (result.Succeeded)
            {

                var currentRoles = await userManager.GetRolesAsync(user);
                await userManager.RemoveFromRolesAsync(user, currentRoles);

                foreach (var role in request.Roles)
                {
                    await userManager.AddToRoleAsync(user, role);
                }

                return RedirectToAction("ManageUser", "Admin");
            }

            return View("ManageUser", "Admin");
        }


        public async Task<IActionResult> Delete(UserDto request)
        {

            var user = await userManager.FindByIdAsync(request.Id.ToString());

            if (user == null)
            {
                ModelState.AddModelError("", "کاربر یافت نشد");
                return RedirectToAction("ManageUser", "Admin");
            }

            var result = await userManager.DeleteAsync(user);

            if (result.Succeeded)
            {
                return RedirectToAction("ManageUser", "Admin");
            }
            return RedirectToAction("ManageUser", "Admin");
        }
    }
}