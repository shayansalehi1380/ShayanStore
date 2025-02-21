﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entity
{
    public class State : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public ICollection<City> Cities { get; set; } = default!;
    }
}
