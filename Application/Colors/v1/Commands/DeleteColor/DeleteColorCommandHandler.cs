using Application.Interface;
using Domain.Entity.Products.Colors;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Domain.Entity.Products.Colors;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Colors.v1.Commands.DeleteColor
{
    public class DeleteColorCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<DeleteColorCommand, bool>
    {
        public async Task<bool> Handle(DeleteColorCommand request, CancellationToken cancellationToken)
        {
            var color = await unitOfWork.GenericRepository<Domain.Entity.Products.Colors.Color>()
                .Table.FirstOrDefaultAsync(x => x.Id == request.Id, CancellationToken.None);

            if (color == null)
            {
                return false;
            }

            await unitOfWork.GenericRepository<Domain.Entity.Products.Colors.Color>().DeleteAsync(color, CancellationToken.None);
            return true;
        }
    }
}
