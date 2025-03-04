using Domain.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos.Users
{
    public class UserDto
    {
        public int Id { get; set; } 
        public string Name { get; set; } = string.Empty;
        public string Family { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public DateTime InsertDate { get; set; } = DateTime.UtcNow;
        public int CityId { get; set; } = default!;
        public City City { get; set; } = default!;
        public ICollection<string>? Roles { get; set; }
    }
}
