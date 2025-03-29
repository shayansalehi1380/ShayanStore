using Application.Interface;
using Azure.Core;
using Domain.Entity.BasicInfo;
using MediatR;

namespace Application.Cities.v1.Commands.DeleteCity
{
    public class DeleteCityCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<DeleteCityCommand, bool>
    {
        public async Task<bool> Handle(DeleteCityCommand request, CancellationToken cancellationToken)
        {
            var city = await unitOfWork.GenericRepository<City>().GetByIdAsync(request.Id, CancellationToken.None);
            if (city == null)
            {
                return false;
            }

            await unitOfWork.GenericRepository<City>().DeleteAsync(city, CancellationToken.None);
            return true;
        }
    }
}
