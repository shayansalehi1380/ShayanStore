using Application.Common.Utilities;
using Application.Interface;
using Domain.Entity.Products.Colors;
using MediatR;

namespace Application.Products.V1.Commands.CreateProductColors;

public class CreateProductColorCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<CreateProductColorCommand>
{
    public async Task Handle(CreateProductColorCommand request, CancellationToken cancellationToken)
    {
        foreach (var i in request.Colors)
        {
            await unitOfWork.GenericRepository<ProductColor>()
                .AddAsync(new ProductColor
                {
                    ProductId = request.ProductId,
                    ColorId = i.ColorId.ToInt(),
                    Inventory = i.Inventory.ToInt(),
                    Price = i.Price.ToLong(),
                    GuaranteeId = i.GuaranteeId.ToInt(),
                    Priority = 1,
                }, cancellationToken);
        }
    }
}