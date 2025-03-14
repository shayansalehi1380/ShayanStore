using System.ComponentModel.DataAnnotations.Schema;
using Domain.Common;

namespace Domain.Entity.Products.Features;

public class FeatureDetails : BaseEntity
{
    public string Title { get; set; } = string.Empty;
    public int FeatureId { get; set; }
    [ForeignKey(nameof(FeatureId))] public Feature Feature { get; set; } = default!;
    public int Priority { get; set; }
}