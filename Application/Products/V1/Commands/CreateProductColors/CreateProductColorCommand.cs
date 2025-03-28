using Application.Dtos.Products;
using MediatR;

namespace Application.Products.V1.Commands.CreateProductColors;

public class CreateProductColorCommand:IRequest
{
    public List<ProductColorDto> Colors { get; set; } = [];
    public int ProductId { get; set; }
}