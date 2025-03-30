using Application.Interface;
using Domain.Entity.Products.Brands;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Brands.v1.Commands.DeleteBrand
{
    public class DeleteBrandCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<DeleteBrandCommand, bool>
    {
        public async Task<bool> Handle(DeleteBrandCommand request, CancellationToken cancellationToken)
        {
            var brand = await unitOfWork.GenericRepository<Brand>().Table
                        .FirstOrDefaultAsync(x => x.Id == request.Id, CancellationToken.None);

            if (brand == null)
            {
                return false;
            }

            await unitOfWork.GenericRepository<Brand>().DeleteAsync(brand, CancellationToken.None);
            return true;
        }
    }
}
