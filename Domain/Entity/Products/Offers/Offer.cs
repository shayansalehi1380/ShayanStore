using System.ComponentModel.DataAnnotations.Schema;
using Domain.Common;
using Domain.Entity.Products.Colors;

namespace Domain.Entity.Products.Offers;

public class Offer:BaseEntity
{
    public int Hours { get; set; }
    public int Minutes { get; set; }
    public DateTime StartDate { get; set; }
    public int ColorId { get; set; }
    [ForeignKey(nameof(ColorId))] public Color? Color { get; set; }
    public int? ProductId { get; set; }
    public long OfferAmount { get; set; }
}