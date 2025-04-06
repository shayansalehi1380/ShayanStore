using Domain.Common;
using Domain.Entity.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entity.BlogPosts
{
    public class BlogPost: BaseEntity
    {
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty; // اینجا از CKEditor استفاده می‌شود
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        [ForeignKey("UserId")]
        public int UserId { get; set; }
        public User User { get; set; } = default!;

    }
}
