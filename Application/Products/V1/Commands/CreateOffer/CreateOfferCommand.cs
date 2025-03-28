using Application.Dtos.Products;
using MediatR;

namespace Application.Products.V1.Commands.CreateOffer;

public class CreateOfferCommand:IRequest
{
    public OfferDto Offer { get; set; }=new OfferDto();
    public int ProductId { get; set; }
}