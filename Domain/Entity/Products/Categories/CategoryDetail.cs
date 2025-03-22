using Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entity.Products.Categories
{
    public class CategoryDetail : BaseEntity
    {
        public string Title { get; set; } = string.Empty;

        [ForeignKey("SubCategoryId")]
        public int SubCategoryId { get; set; }
        public SubCategory SubCategory { get; set; } = default!;
    }
}