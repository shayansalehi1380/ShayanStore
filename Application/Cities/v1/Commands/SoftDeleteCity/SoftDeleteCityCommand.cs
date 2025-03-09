using MediatR;

namespace Application.Cities.v1.Commands.SoftDeleteCity;

public class SoftDeleteCityCommand:IRequest<bool>
{
    public int Id { get; set; }
}