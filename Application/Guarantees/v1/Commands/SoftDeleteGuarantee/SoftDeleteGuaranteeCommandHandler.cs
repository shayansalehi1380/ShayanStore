using Application.Interface;
using Domain.Entity.Products.Guaranties;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Guarantees.v1.Commands.SoftDeleteGuarantee
{
    public class SoftDeleteGuaranteeCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<SoftDeleteGuaranteeCommand, bool>
    {
        public async Task<bool> Handle(SoftDeleteGuaranteeCommand request, CancellationToken cancellationToken)
        {
            var guarantee = await unitOfWork.GenericRepository<Guarantee>().Table.FirstOrDefaultAsync(x => x.Id == request.Id, CancellationToken.None);

            if (guarantee == null)
            {
                return false;
            }
            guarantee.IsDelete = true;

            await unitOfWork.GenericRepository<Guarantee>().UpdateAsync(guarantee, CancellationToken.None);
            return true;
        }
    }
}
