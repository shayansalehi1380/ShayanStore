using Application.Common.ApiResult;
using Application.Dtos.Features;
using Application.Features.v1.Queries.GetFeatureDetailBySubId;
using Domain.Entity.Products.Categories;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AdminPanel.UI.Controllers.Api;

[ApiVersion("1")]
public class FeatureController(IMediator mediator) : BaseApiController
{
    [HttpGet("get-feature-details-by-sub-id")]
    public async Task<ActionResult<ApiResult<FeatureDto>>> GetFeatureDetailBySubId(int id)
    {
        var result = await mediator.Send(new GetFeatureDetailBySubIdQuery(id));
        return Ok(new ApiResult<List<FeatureDto>>(result, "عملیات موفق", ApiResultStatusCode.Success));
    }
}