using Application.Dtos.Products;
using MediatR;

namespace Application.Products.V1.Commands.CreateProductDetails;

public class CreateProductDetailCommand:IRequest
{
    public List<ProductFeatureDto> Details { get; set; } = [];
    public int ProductId { get; set; }
}