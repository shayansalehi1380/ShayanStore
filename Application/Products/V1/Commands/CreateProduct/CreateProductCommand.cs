using Application.Common.ApiResult;
using Application.Dtos.Products;
using MediatR;

namespace Application.Products.V1.Commands.CreateProduct;

public class CreateProductCommand: IRequest<ApiResult<int>>
{
    public ProductDto Product { get; set; } = new();
    public int UserId { get; set; }
}