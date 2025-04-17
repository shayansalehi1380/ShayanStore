using Application.Interface;
using Domain.Entity.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Shop.Controllers;

public class AuthController(SignInManager<User> signInManager, UserManager<User> userManager, IUnitOfWork unitOfWork)
    : Controller
{
    public async Task<ActionResult> Login()
    {
        return View();
    }
    public async Task<ActionResult> Register(string username, string password)
    {
        return View();
    }
    public async Task<ActionResult> UserRegister(string username, string password)
    {
        var user = await userManager.FindByNameAsync(username);
        if (user != null)
            return RedirectToAction("Register", "Auth");
        var userCreate = new User
        {
            UserName = username,
            Email = string.Empty,
            Name = string.Empty,
            Family = string.Empty,
            Sheba = string.Empty,
            ConcurrencyStamp = string.Empty,
            ConfirmCode = string.Empty,
            ImageName = string.Empty,
            InsertDate = DateTime.Now,
            CityId = 1
        };
        await userManager.CreateAsync(userCreate, password);
        
        // var wallet = new Wallet
        // {
        //     UserId = user.Id,
        //     WalletBalance = 0
        // };
        // await unitOfWork.GenericRepository<Wallet>().AddAsync(wallet, CancellationToken.None);
        // userCreate.WalletId = wallet.Id;
        // await userManager.UpdateAsync(userCreate);
        
        await signInManager.SignInAsync(userCreate, false);
        return RedirectToAction("Panel", "UserPanel");
    }


    public async Task<ActionResult> UserLogin(string username, string password)
    {
        var user = await userManager.FindByNameAsync(username);
        if (user != null)
        {
            if (await userManager.CheckPasswordAsync(user, password))
            {
                await signInManager.SignInAsync(user, false);
            }
        }
        else
        {
            return RedirectToAction("Login", "Auth");
        }

        return RedirectToAction("Panel", "UserPanel");
    }


    public async Task<ActionResult> Logout()
    {
        if (User.Identity.IsAuthenticated)
        {
            await signInManager.SignOutAsync();
        }

        return RedirectToAction("Index", "Home");
    }
}