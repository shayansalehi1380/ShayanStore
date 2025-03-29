using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Brands.v1.Commands.SoftDeleteBrand
{
    public class SoftDeleteBrandCommand: IRequest<bool>
    {
        public int Id { get; set; }
    }
}
