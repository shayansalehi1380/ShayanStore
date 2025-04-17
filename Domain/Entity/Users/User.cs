using System.ComponentModel.DataAnnotations.Schema;
using Domain.Entity.BasicInfo;
using Domain.Entity.BlogPosts;
using Microsoft.AspNetCore.Identity;

namespace Domain.Entity.Users
{
    public class User : IdentityUser<int>
    {
        public string Name { get; set; } = string.Empty;
        public string Family { get; set; } = string.Empty;
        public string ImageName { get; set; } = string.Empty;
        public string ConfirmCode { get; set; } = string.Empty;
        public string NationalCode { get; set; } = string.Empty;
        public string Sheba { get; set; } = string.Empty;
        public bool IsDelete { get; set; } = false;
        public DateTime InsertDate { get; set; } = DateTime.UtcNow;
        public DateTime ConfirmCodeExpireTime { get; set; } = DateTime.UtcNow.AddMinutes(3);
        [ForeignKey("CityId")] public int CityId { get; set; }
        public City City { get; set; } = default!;
        // [ForeignKey("WalletId")] public int WalletId { get; set; }
        //  public int WalletId1 { get; set; }
        // public Wallet Wallet { get; set; } = default!;

        public ICollection<BlogPost> BlogPosts { get; set; } = default!;
    }
}