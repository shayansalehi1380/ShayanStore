using Application.Interface;
using Domain.Entity.BasicInfo;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Cities.v1.Commands.CreateCity
{
    class CreateCityCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<CreateCityCommand, bool>
    {
        public async Task<bool> Handle(CreateCityCommand request, CancellationToken cancellationToken)
        {
            await unitOfWork.GenericRepository<City>().AddAsync(new City
            {
                Name = request.Name,
                StateId = request.StateId,
            }, CancellationToken.None);
            return true;
        }
    }
}
