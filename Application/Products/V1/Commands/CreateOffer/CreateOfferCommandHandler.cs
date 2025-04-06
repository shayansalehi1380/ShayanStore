using Application.Common.Utilities;
using Application.Interface;
using Domain.Entity.Products;
using Domain.Entity.Products.Offers;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Products.V1.Commands.CreateOffer;

public class CreateOfferCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<CreateOfferCommand>
{
    public async Task Handle(CreateOfferCommand request, CancellationToken cancellationToken)
    {
        var offer = new Offer
        {
            ProductId = request.ProductId,
            ColorId = request.Offer.ColorId.ToInt(),
            Hours = request.Offer.Hours,
            Minutes = request.Offer.Minutes,
            OfferAmount = request.Offer.DiscountAmount.ToLong(),
            StartDate = DateTime.Parse(request.Offer.StartDate),
        };
        await unitOfWork.GenericRepository<Offer>().AddAsync(offer, cancellationToken);
        var prod = await unitOfWork.GenericRepository<Product>().Table
            .FirstOrDefaultAsync(x => x.Id == request.ProductId, cancellationToken);
        prod.OfferId = offer.Id;
        await unitOfWork.GenericRepository<Product>().UpdateAsync(prod, cancellationToken);
    }
}