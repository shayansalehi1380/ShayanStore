using Domain.Common;

namespace Domain.Entity.Products.ImageAttachments;

public class ImageGallery:BaseEntity
{
    public string ImageUri { get; set; } = string.Empty;
    public int ProductId { get; set; }
}