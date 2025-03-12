using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entity;

public class SubCategory:BaseEntity
{
    public string Title { get; set; }=string.Empty;
    public int? CategoryId { get; set; }
    [ForeignKey("CategoryId")] public Category? Category { get; set; }
}