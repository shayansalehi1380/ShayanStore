using Domain.Common;

namespace Domain.Entity.Products.Colors;

public class Color : BaseEntity
{
    public string Title { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;
}