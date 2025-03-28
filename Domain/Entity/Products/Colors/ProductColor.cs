using System.ComponentModel.DataAnnotations.Schema;
using Domain.Common;
using Domain.Entity.Products.Guaranties;

namespace Domain.Entity.Products.Colors;

public class ProductColor:BaseEntity
{
    public long Price { get; set; }//RIT
    public int Priority { get; set; }
    public int ProductId { get; set; }
    [ForeignKey(nameof(ProductId))] public Product? Product { get; set; } 
    public int ColorId { get; set; }
    [ForeignKey(nameof(ColorId))] public Color? Color { get; set; } 
    public int GuaranteeId { get; set; }
    [ForeignKey(nameof(GuaranteeId))] public Guarantee? Guarantee { get; set; } 
    public int Inventory { get; set; }
}