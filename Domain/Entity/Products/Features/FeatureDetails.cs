using System.ComponentModel.DataAnnotations.Schema;
using Domain.Common;
using Domain.Entity.Products.Categories;

namespace Domain.Entity.Products.Features;

public class FeatureDetails : BaseEntity
{
    public string Title { get; set; } = string.Empty;
    public int FeatureId { get; set; }
    [ForeignKey(nameof(FeatureId))] public Feature Feature { get; set; } = default!;
    public int SubCategoryId { get; set; }
    [ForeignKey(nameof(SubCategoryId))] public SubCategory SubCategory{ get; set; } = default!;
    public int Priority { get; set; }
}