using Application.Interface;
using Domain.Entity.Products.Colors;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Colors.v1.Commands.CreateColor
{
    public class CreateColorCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<CreateColorCommand, bool>
    {
        public async Task<bool> Handle(CreateColorCommand request, CancellationToken cancellationToken)
        {
            await unitOfWork.GenericRepository<Color>().AddAsync(new Color
            {
                Title = request.Name,
                Code = request.CodeColor
            }, CancellationToken.None);
            return true;
        }
    }
}
