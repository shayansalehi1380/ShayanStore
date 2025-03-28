using Application.Common;
using Application.Interface;
using Domain.Entity.Products.ImageAttachments;
using MediatR;
using Microsoft.AspNetCore.Hosting;

namespace Application.Products.V1.Commands.UploadImage;

public class UploadImageCommandHandler(IUnitOfWork unitOfWork, IWebHostEnvironment env)
    : IRequestHandler<UploadImageCommand>
{
    public async Task Handle(UploadImageCommand request, CancellationToken cancellationToken)
    {
        Upload up = new Upload(env);
        foreach (var i in request.Base64Images)
        {
            await unitOfWork.GenericRepository<ImageGallery>().AddAsync(new ImageGallery
            {
                ImageUri = up.AddImage(i, "Images/Product",
                    Guid.NewGuid().ToString().Substring(0, 6))
            }, cancellationToken);
        }
    }
}