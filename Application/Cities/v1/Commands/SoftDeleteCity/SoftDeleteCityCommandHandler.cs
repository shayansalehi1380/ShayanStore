using Application.Interface;
using Domain.Entity;
using MediatR;

namespace Application.Cities.v1.Commands.SoftDeleteCity;

public class SoftDeleteCityCommandHandler(IUnitOfWork unitOfWork):IRequestHandler<SoftDeleteCityCommand,bool>
{
    public async Task<bool> Handle(SoftDeleteCityCommand request, CancellationToken cancellationToken)
    {
        var city = await unitOfWork.GenericRepository<City>().GetByIdAsync(request.Id, CancellationToken.None);
        if (city == null)
        {
            return false;
        }
        city.IsDelete = true;
        await unitOfWork.GenericRepository<City>().UpdateAsync(city, CancellationToken.None);
        return true;
    }
}