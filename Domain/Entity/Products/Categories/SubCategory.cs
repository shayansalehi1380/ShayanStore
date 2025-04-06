using System.ComponentModel.DataAnnotations.Schema;
using Domain.Common;
using Domain.Entity.Products.Features;

namespace Domain.Entity.Products.Categories
{
    public class SubCategory : BaseEntity
    {
        public string Title { get; set; }=string.Empty;
        public ICollection<CategoryDetail> CategoryDetails { get; set; } = default!;

        [ForeignKey("CategoryId")]
        public int CategoryId { get; set; }
        public Category Category { get; set; } = default!;
    }
}