using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Brands.v1.Commands.CreateBrand
{
    public class CreateBrandCommand: IRequest<bool>
    {
        public string Name { get; set; }
        public IFormFile Image { get; set; }
    }
}
