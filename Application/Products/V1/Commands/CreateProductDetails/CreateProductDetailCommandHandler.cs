using Application.Common.Utilities;
using Application.Interface;
using Domain.Entity.Products.Features;
using MediatR;

namespace Application.Products.V1.Commands.CreateProductDetails;

public class CreateProductDetailCommandHandler(IUnitOfWork unitOfWork):IRequestHandler<CreateProductDetailCommand>
{
    public async Task Handle(CreateProductDetailCommand request, CancellationToken cancellationToken)
    {
        foreach (var i in request.Details)
        {
            await unitOfWork.GenericRepository<ProductFeature>().AddAsync(new ProductFeature
            {
                Value = i.Value,
                ProductId = request.ProductId,
                FeatureDetailsId = i.Key.ToInt()
            },cancellationToken);
        }
    }
}