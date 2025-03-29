using Application.Interface;
using Domain.Entity.BasicInfo;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.States.v1.Commands.CreateState
{
    public class CreateStateCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<CreateStateCommand, bool>
    {
        public async Task<bool> Handle(CreateStateCommand request, CancellationToken cancellationToken)
        {
            await unitOfWork.GenericRepository<State>().AddAsync(new State
            {
                Name = request.Name,
            }, CancellationToken.None);
            return true;
        }
    }
}
