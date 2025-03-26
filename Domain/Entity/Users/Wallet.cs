using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entity.Users
{
    public class Wallet : BaseEntity
    {
        public int WalletBalance { get; set; }
        public int UserId { get; set; }
        public User User { get; set; } = default!;
    }
}
