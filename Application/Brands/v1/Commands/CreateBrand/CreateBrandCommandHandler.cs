using Application.Common.ApiResult;
using Application.Common;
using Application.Interface;
using Domain.Entity.Products.Brands;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Application.Brands.v1.Commands.CreateBrand
{
    public class CreateBrandCommandHandler(IUnitOfWork unitOfWork, IWebHostEnvironment environment) : IRequestHandler<CreateBrandCommand, bool>
    {
        private Upload upload = new Upload(environment);
        public async Task<bool> Handle(CreateBrandCommand request, CancellationToken cancellationToken)
        {
            if (request.Image == null)
            {
                return false;
            }

            await unitOfWork.GenericRepository<Brand>().AddAsync(new Brand
            {
                Title = request.Name,
                ImageUri = upload.UploadFile(request.Image, "Brands")
            }, CancellationToken.None);
            return true;
        }
    }
}
