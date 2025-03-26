using System.ComponentModel.DataAnnotations.Schema;
using Domain.Common;

namespace Domain.Entity.Products.Features;

public class ProductFeature : BaseEntity
{
    public int FeatureDetailsId { get; set; }
    [ForeignKey(nameof(FeatureDetailsId))] public FeatureDetails? FeatureDetails { get; set; }
    public int ProductId { get; set; }

    public string? Value { get; set; }
}