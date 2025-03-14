using System.ComponentModel.DataAnnotations.Schema;
using Domain.Common;

namespace Domain.Entity.BasicInfo
{
    public class City : BaseEntity
    {
        public string Name { get; set; } = string.Empty;

        [ForeignKey("StateId")]
        public int StateId { get; set; }
        public State State { get; set; } = default!;

    }
}
