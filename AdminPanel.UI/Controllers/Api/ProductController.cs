using Application.Common.ApiResult;
using Application.Common.Utilities;
using Application.Dtos.Products;
using Application.Products.V1.Commands;
using Application.Products.V1.Commands.CreateOffer;
using Application.Products.V1.Commands.CreateProduct;
using Application.Products.V1.Commands.CreateProductColors;
using Application.Products.V1.Commands.CreateProductDetails;
using Application.Products.V1.Commands.UploadImage;
using Domain.Entity.Users;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AdminPanel.UI.Controllers.Api;

[ApiVersion("1")]
public class ProductController(IMediator mediator, UserManager<User> userManager) : BaseApiController
{
    [HttpPost("create")]
    public async Task<ActionResult<ApiResult<bool>>> Create([FromForm] ProductDto? request)
    {
        var user = await userManager.FindByNameAsync(User.Identity.Name);

        //create product
        var prodResult = await mediator.Send(new CreateProductCommand
        {
            Product = request,
            UserId = user.Id
        }, CancellationToken.None);

        
        //create gallery
        await mediator.Send(new UploadImageCommand
        {
            ProductId = prodResult.Data,
            Base64Images = request.Images
        }, CancellationToken.None);

        //create feature
        await mediator.Send(new CreateProductDetailCommand
        {
            ProductId = prodResult.Data,
            Details = request.Features,
        }, CancellationToken.None);

        //create color
        await mediator.Send(new CreateProductColorCommand
        {
            ProductId = prodResult.Data,
            Colors = request.Colors,
        }, CancellationToken.None);

        if (request.IsSpecialOffer)
        {
            await mediator.Send(new CreateOfferCommand
            {
                ProductId = prodResult.Data,
                Offer = request.Offer,
            });
        }

        var result = new ApiResult<bool>(true, ApiResultStatusCode.Success.ToDisplay(), ApiResultStatusCode.Success);
        return Ok(result);
    }
}