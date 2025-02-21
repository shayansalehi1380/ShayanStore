using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entity
{
    public class City : BaseEntity
    {
        public string Name { get; set; } = string.Empty;


        [ForeignKey("State")]
        public int StateId { get; set; }
        public State State { get; set; } = default!;

    }
}
