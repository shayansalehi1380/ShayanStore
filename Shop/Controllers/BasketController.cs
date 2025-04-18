using Application.Interface;
using Domain.Entity.Products;
using Domain.Entity.Products.Categories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Shop.Controllers;

public class BasketController(IUnitOfWork _unitOfWork) : Controller
{
    public async Task<ActionResult> Basket()
    {
        #region Required

        var mcategories = await _unitOfWork.GenericRepository<MainCategory>()
            .TableNoTracking
            .Include(m => m.Categories)
            .ThenInclude(c => c.SubCategories)
            .ToListAsync();

        var categories = await _unitOfWork.GenericRepository<Category>()
            .TableNoTracking
            .Include(x => x.MainCategory)
            .ToListAsync();

        var subcategories = await _unitOfWork.GenericRepository<SubCategory>()
            .TableNoTracking
            .Include(x => x.Category)
            .ToListAsync();

        var firstMainCategory = categories
            .FirstOrDefault(c => c.MainCategory != null)?.MainCategory;

        ViewBag.FirstMainCategory = firstMainCategory;
        ViewBag.SubCategory = subcategories;
        ViewBag.Categories = categories;
        ViewBag.MainCategories = mcategories;

        #endregion

        var basketlist = new List<int>();
        if (HttpContext.Session.GetString("basket") != null)
        {
            basketlist = JsonConvert.DeserializeObject<List<int>>(HttpContext.Session.GetString("basket")).ToList();
        }

        var prods = new List<Product>();

        foreach (var item in basketlist)
        {
            var prod = await _unitOfWork.GenericRepository<Product>().TableNoTracking
                .Include(x=>x.ProductColors).ThenInclude(x=>x.Color)
                .Include(x=>x.ProductColors).ThenInclude(x=>x.Guarantee)
                .FirstOrDefaultAsync(x => x.Id == item);
            if (prod != null)
            {
                prods.Add(prod);
            }
        }

        ViewBag.Prods = prods;
        return View();
    }

    public async Task<ActionResult> AddToBasket(int productId)
    {
        var basketlist = new List<int>();
        if (HttpContext.Session.GetString("basket") != null)
        {
            basketlist = JsonConvert.DeserializeObject<List<int>>(HttpContext.Session.GetString("basket")).ToList();
            basketlist.Add(productId);
            HttpContext.Session.SetString("basket", JsonConvert.SerializeObject(basketlist));
        }
        else
        {
            basketlist.Add(productId);
            HttpContext.Session.SetString("basket", JsonConvert.SerializeObject(basketlist));
        }

        return RedirectToAction("Index", "Home");
    }

    public async Task<ActionResult> RemoveBasket(int id)
    {
        var basketList = new List<int>();
        if (HttpContext.Session.GetString("basket") != null)
        {
            basketList = JsonConvert.DeserializeObject<List<int>>(HttpContext.Session.GetString("basket")).ToList();
            basketList.RemoveAll(item => item == id);
            HttpContext.Session.SetString("basket", JsonConvert.SerializeObject(basketList));
        }

        return RedirectToAction("Basket");

    }
}