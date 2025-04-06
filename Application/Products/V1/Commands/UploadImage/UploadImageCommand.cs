using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Products.V1.Commands.UploadImage;

public class UploadImageCommand : IRequest
{
    public List<IFormFile> Images { get; set; } = [];
    public int ProductId { get; set; }
}