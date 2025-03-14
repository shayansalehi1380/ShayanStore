using Domain.Common;

namespace Domain.Entity.BasicInfo
{
    public class State : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public ICollection<City> Cities { get; set; } = default!;
    }
}
