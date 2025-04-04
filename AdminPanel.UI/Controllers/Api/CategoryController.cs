using Application.Categories.v1.Queries.GetCatDetailsBySubId;
using Application.Common.ApiResult;
using Domain.Entity.Products.Categories;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AdminPanel.UI.Controllers.Api;

[ApiVersion("1")]
public class CategoryController(IMediator mediator) : BaseApiController
{
    // [HttpGet("get-cat-details-by-sub-id/{id:int}")]
    [HttpGet("get-cat-details-by-sub-id")]
    public async Task<ActionResult<ApiResult<CategoryDetail>>> GetCategoryDetailBySubId(int id)
    {
        var result = await mediator.Send(new GetCateDetailsBySubIdQuery(id));
        return Ok(new ApiResult<List<CategoryDetail>>(result, "عملیات موفق", ApiResultStatusCode.Success));
    }
}