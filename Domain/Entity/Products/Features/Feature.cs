using Domain.Common;

namespace Domain.Entity.Products.Features;

public class Feature : BaseEntity
{
    public string Title { get; set; } = string.Empty;
    public int Priority { get; set; }
    public ICollection<FeatureDetails> Details { get; set; } = default!;
}