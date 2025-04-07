using Microsoft.AspNetCore.Http;

namespace Application.Dtos.Products;

public class ProductDto
{
    public int Id { get; set; }
    public string? Title { get; set; }
    public string? PersianTitle { get; set; }
    public string? Detail { get; set; }
    public string? UniqueCode { get; set; }
    public string? MetaKeyword { get; set; }
    public string? ProductGift { get; set; }
    public string? Strengths { get; set; }
    public string? WeakPoints { get; set; }
    public string? ProductStatus { get; set; }
    public string? DiscountAmount { get; set; }
    public string? ProfitPercentage { get; set; }
    public int BrandId { get; set; }
    public int CategoryId { get; set; }
    public int SubCategoryId { get; set; }
    public List<string> SelectedColors { get; set; } = new List<string>();
    public List<ProductColorDto> Colors { get; set; } = new List<ProductColorDto>();
    public string? SeoTitle { get; set; }
    public string? SeoCanonicalUrl { get; set; }
    public string? SeoDescription { get; set; }
    public List<IFormFile> Images { get; set; } = [];
    public List<FeatureDto> Features { get; set; } = new ();
    public string? FullDesc { get; set; }
    public bool IsFlashOffer { get; set; }
    public bool IsSpecialOffer { get; set; }
    public bool IsSuggested { get; set; }
    public OfferDto? Offer { get; set; }

}
public class FeatureDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Value { get; set; }
}

