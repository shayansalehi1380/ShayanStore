using Application.Interface;
using Domain.Entity.Products.Colors;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Colors.v1.Commands.SoftDeleteColor
{
    public class SoftDeleteColorCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<SoftDeleteColorCommand, bool>
    {
        public async Task<bool> Handle(SoftDeleteColorCommand request, CancellationToken cancellationToken)
        {
            var color = await unitOfWork.GenericRepository<Color>().Table.FirstOrDefaultAsync(x => x.Id == request.Id, CancellationToken.None);
            if (color == null)
            {
                return false;
            }
            color.IsDelete = true;

            await unitOfWork.GenericRepository<Color>().UpdateAsync(color, CancellationToken.None);
            return true;
        }
    }
}
