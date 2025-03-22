using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entity.BasicInfo
{
    public class ShippingOption : BaseEntity
    {
        public string Title { get; set; } = string.Empty;
        public string Price { get; set; } = string.Empty;
    }
}
