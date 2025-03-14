using Domain.Common;

namespace Domain.Entity.Products.Categories
{
    public class MainCategory : BaseEntity
    {
        public string Title { get; set; } = string.Empty;
        public ICollection<Category> Categories { get; set; } = default!;
    }
}
