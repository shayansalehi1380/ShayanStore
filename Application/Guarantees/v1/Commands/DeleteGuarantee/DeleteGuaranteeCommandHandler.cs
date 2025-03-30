using Application.Interface;
using Domain.Entity.Products.Guaranties;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Guarantees.v1.Commands.DeleteGuarantee
{
    public class DeleteGuaranteeCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<DeleteGuaranteeCommand, bool>
    {
        public async Task<bool> Handle(DeleteGuaranteeCommand request, CancellationToken cancellationToken)
        {
            var guarantee = await unitOfWork.GenericRepository<Guarantee>().Table.FirstOrDefaultAsync(x => x.Id == request.Id, CancellationToken.None);

            if (guarantee == null)
            {
                return false;
            }

            await unitOfWork.GenericRepository<Guarantee>().DeleteAsync(guarantee, CancellationToken.None);
            return true;
        }
    }
}
