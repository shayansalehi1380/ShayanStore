using Application.Interface;
using Domain.Entity.Products.Guaranties;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Guarantees.v1.Commands.CreateGuarantee
{
    public class CreateGuaranteeCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<CreateGuaranteeCommand, bool>
    {
        public async Task<bool> Handle(CreateGuaranteeCommand request, CancellationToken cancellationToken)
        {
            await unitOfWork.GenericRepository<Guarantee>().AddAsync(new Guarantee
            {
                Title = request.Name
            }, CancellationToken.None);
            return true;
        }
    }
}
