using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entity
{
    public class SubCategory : BaseEntity
    {
        public string Title { get; set; }=string.Empty;

        [ForeignKey("CategoryId")]
        public int CategoryId { get; set; }
        public Category Category { get; set; } = default!;
    }
}