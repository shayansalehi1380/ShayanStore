using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entity
{
    public class Category : BaseEntity
    {
        public string Name { get; set; } = string.Empty;

        public int MainCategoryId { get; set; }
        [ForeignKey("MainCategoryId")]
        public MainCategory MainCategory { get; set; } = default!;
    }
}
