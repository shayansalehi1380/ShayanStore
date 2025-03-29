using Application.Interface;
using Azure.Core;
using Domain.Entity.BasicInfo;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Cities.v1.Commands.UpdateCity
{
    public class UpdateCityCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<UpdateCityCommand, bool>
    {
        public async Task<bool> Handle(UpdateCityCommand request, CancellationToken cancellationToken)
        {
            var city = await unitOfWork.GenericRepository<City>().GetByIdAsync(request.Id, CancellationToken.None);
            if (city == null)
            {
                return false;
            }

            city.Name = request.Name;
            city.StateId = request.StateId;

            await unitOfWork.GenericRepository<City>().UpdateAsync(city, CancellationToken.None);
            return true;
        }
    }
}
