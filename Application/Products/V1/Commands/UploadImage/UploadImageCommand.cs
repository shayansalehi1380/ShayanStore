using MediatR;

namespace Application.Products.V1.Commands.UploadImage;

public class UploadImageCommand : IRequest
{
    public List<string> Base64Images { get; set; } = [];
    public int ProductId { get; set; }
}