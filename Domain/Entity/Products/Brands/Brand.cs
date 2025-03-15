using Domain.Common;

namespace Domain.Entity.Products.Brands;

public class Brand : BaseEntity
{
    public string Title { get; set; } = string.Empty;
    public string ImageUri { get; set; } = string.Empty;
}