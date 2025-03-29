using Application.Interface;
using Domain.Entity.Products.Brands;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Brands.v1.Commands.SoftDeleteBrand
{
    public class SoftDeleteBrandCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<SoftDeleteBrandCommand, bool>
    {
        public async Task<bool> Handle(SoftDeleteBrandCommand request, CancellationToken cancellationToken)
        {
            var brand = await unitOfWork.GenericRepository<Brand>().Table
                    .FirstOrDefaultAsync(x => x.Id == request.Id, CancellationToken.None);

            if (brand == null)
            {
                return false;
            }
            brand.IsDelete = true;

            await unitOfWork.GenericRepository<Brand>().UpdateAsync(brand, CancellationToken.None);
            return true;
        }
    }
}
