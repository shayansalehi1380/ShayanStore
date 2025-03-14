using System.ComponentModel.DataAnnotations.Schema;
using Domain.Common;

namespace Domain.Entity.Products.Categories
{
    public class Category : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public ICollection<SubCategory> SubCategories { get; set; } = default!;

        [ForeignKey("MainCategoryId")]
        public int MainCategoryId { get; set; }
        public MainCategory MainCategory { get; set; } = default!;
    }
}
