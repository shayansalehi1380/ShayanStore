using Application.Interface;
using Domain.Entity.BasicInfo;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.States.v1.Commands.UpdateState
{
    public class UpdateStateCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<UpdateStateCommand, bool>
    {
        public async Task<bool> Handle(UpdateStateCommand request, CancellationToken cancellationToken)
        {
            var state = await unitOfWork.GenericRepository<State>().Table.FirstOrDefaultAsync(x => x.Id == request.Id, CancellationToken.None);
            if (state == null)
            {
                return false;
            }
            state.Name = request.Name;

            await unitOfWork.GenericRepository<State>().UpdateAsync(state, CancellationToken.None);
            return true;
        }
    }
}
