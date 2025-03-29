using Application.Interface;
using Azure.Core;
using Domain.Entity.BasicInfo;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.States.v1.Commands.DeleteState
{
    public class DeleteStateCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<DeleteStateCommand, bool>
    {
        public async Task<bool> Handle(DeleteStateCommand request, CancellationToken cancellationToken)
        {
            var state = await unitOfWork.GenericRepository<State>().GetByIdAsync(request.Id, CancellationToken.None);
            if (state == null)
            {
                return false;
            }

            await unitOfWork.GenericRepository<State>().DeleteAsync(state, CancellationToken.None);
            return true;
        }
    }
}
