using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entity
{
    public class MainCategory : BaseEntity
    {
        public string Title { get; set; } = string.Empty;
        public ICollection<Category> categories { get; set; } = default!;
    }
}
