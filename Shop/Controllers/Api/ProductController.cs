using Application.Interface;
using Domain.Entity.Products;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Shop.Controllers.Api;

[ApiVersion("1")]
public class ProductController(IUnitOfWork _unitOfWork) : BaseApiController
{
    [HttpGet("search")]
    public async Task<List<Product>> SearchProds(string keyword)
    {
        var keys = keyword.Split(" ");
        var query = _unitOfWork.GenericRepository<Product>().TableNoTracking.AsQueryable();
        if (keys.Length <= 0) return await query.ToListAsync();
        foreach (var k in keys)
        {
            query = query.Where(x => x.FaTitle.Contains(k));
        }
        return await query.ToListAsync();
    }
}