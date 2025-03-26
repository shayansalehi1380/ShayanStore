using System.ComponentModel.DataAnnotations.Schema;
using Domain.Common;
using Domain.Entity.Products.Brands;
using Domain.Entity.Products.Categories;
using Domain.Entity.Products.Colors;
using Domain.Entity.Products.Features;
using Domain.Entity.Products.ImageAttachments;
using Domain.Entity.Products.Offers;
using Domain.Entity.Users;
using Domain.Enums;

namespace Domain.Entity.Products;

public class Product : BaseEntity
{
    public string EnTitle { get; set; } = string.Empty;
    public string FaTitle { get; set; } = string.Empty;
    public string Detail { get; set; } = string.Empty;
    public string FullDetail { get; set; } = string.Empty;
    public string ImageUri { get; set; } = string.Empty;
    public string? ProductGift { get; set; }
    public string Strengths { get; set; } = string.Empty;
    public string WeakPoints { get; set; } = string.Empty;
    public string UniqueCode { get; set; } = string.Empty;
    public double InterestRate { get; set; }
    public double DiscountAmount { get; set; }
    public bool IsActive { get; set; }
    public int OnClick { get; set; }
    public string? SeoTitle { get; set; }
    public string? SeoDesc { get; set; }
    public string? SeoCanonical { get; set; }
    public bool IsOffer { get; set; }
    
    public DateTime InsertDate { get; set; }
    public DateTime UpdateTime { get; set; }
    public ProductStatus Status { get; set; }

    public int? CategoryDetailId { get; set; }
    [ForeignKey(nameof(CategoryDetailId))] public CategoryDetail? CategoryDetail { get; set; }
    public int BrandId { get; set; }
    [ForeignKey(nameof(BrandId))] public Brand Brand { get; set; } = default!;
    public int? CreatorId { get; set; }
    [ForeignKey(nameof(CreatorId))] public User? Creator { get; set; }
    public int? UserUpdateId { get; set; }
    [ForeignKey(nameof(UserUpdateId))] public User? UserUpdate { get; set; }
    public int? OfferId { get; set; }
    [ForeignKey(nameof(OfferId))] public Offer? Offer { get; set; }
    

    public ICollection<ImageGallery> ImageGalleries { get; set; } = default!;
    public ICollection<ProductColor> ProductColors { get; set; } = default!;
    public ICollection<ProductFeature> ProductFeatures { get; set; } = default!;
}