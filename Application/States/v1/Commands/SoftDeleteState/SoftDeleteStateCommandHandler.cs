using Application.Interface;
using Azure.Core;
using Domain.Entity.BasicInfo;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.States.v1.Commands.SoftDeleteState
{
    public class SoftDeleteStateCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<SoftDeleteStateCommand, bool>
    {
        public async Task<bool> Handle(SoftDeleteStateCommand request, CancellationToken cancellationToken)
        {
            var state = await unitOfWork.GenericRepository<State>().GetByIdAsync(request.Id, CancellationToken.None);
            if (state == null)
            {
                return false;
            }

            state.IsDelete = true;
            await unitOfWork.GenericRepository<State>().UpdateAsync(state, CancellationToken.None);
            return true;
        }
    }
}
