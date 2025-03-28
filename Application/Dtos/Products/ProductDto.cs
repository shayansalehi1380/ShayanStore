namespace Application.Dtos.Products;

public class ProductDto
{
    public int Id { get; set; }
    public string? EnTitle { get; set; }
    public string? FaTitle { get; set; }
    public string? Detail { get; set; }
    public string? FullDetail { get; set; }
    public string? ImageUri { get; set; }
    public string? ProductGift { get; set; }
    public string? DiscountAmount { get; set; }
    public string? BrandId { get; set; }
    public string? CategoryDetailId { get; set; }
    public string UniqueCode { get; set; } = string.Empty;
    public string? ProductStatus { get; set; }
    public bool IsActive { get; set; }
    public bool IsOffer { get; set; }
    public string Strengths { get; set; } = string.Empty;
    public string WeakPoints { get; set; } = string.Empty;
    public bool MomentaryOffer { get; set; }
    public string SeoTitle { get; set; } = string.Empty;
    public string SeoDesc { get; set; } = string.Empty;
    public string SeoCanonical { get; set; } = string.Empty;
    public double InterestRate { get; set; }
    public List<ProductColorDto> ProductColors { get; set; } = [];
    public List<ProductFeatureDto> ProductDetails { get; set; } = [];
    public OfferDto? Offer { get; set; }
    public List<string> Images { get; set; } = [];

}

