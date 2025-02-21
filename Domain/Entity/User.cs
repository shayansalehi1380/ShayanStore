using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entity
{
    public class User : IdentityUser<int>
    {
        public string Name { get; set; } = string.Empty;
        public string Family { get; set; } = string.Empty;
        public string ImageName { get; set; } = string.Empty;
        public string ConfirmCode { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string NationalCode { get; set; } = string.Empty;
        public string Sheba { get; set; } = string.Empty;
        public DateTime InsertDate { get; set; } = DateTime.Now;
        public DateTime ConfirmCodeExpireTime { get; set; }

        [ForeignKey("State")]
        public int CityId { get; set; }
        public State State { get; set; } = default!;

    }
}
