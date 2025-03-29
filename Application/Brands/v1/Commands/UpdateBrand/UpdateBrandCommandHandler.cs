using Application.Common;
using Application.Interface;
using Domain.Entity.Products.Brands;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Application.Brands.v1.Commands.UpdateBrand
{
    public class UpdateBrandCommandHandler(IUnitOfWork unitOfWork, IWebHostEnvironment environment) : IRequestHandler<UpdateBrandCommand, bool>
    {
        private Upload upload = new Upload(environment);
        public async Task<bool> Handle(UpdateBrandCommand request, CancellationToken cancellationToken)
        {
            var brand = await unitOfWork.GenericRepository<Brand>().Table
                      .FirstOrDefaultAsync(x => x.Id == request.Id, CancellationToken.None);
            if (brand == null)
            {
                return false;
            }
            brand.Title = request.Name;
            brand.ImageUri = request.Image != null ? upload.UploadFile(request.Image, "Brands") : brand.ImageUri;

            await unitOfWork.GenericRepository<Brand>().UpdateAsync(brand, CancellationToken.None);
            return true;
        }
    }
}
