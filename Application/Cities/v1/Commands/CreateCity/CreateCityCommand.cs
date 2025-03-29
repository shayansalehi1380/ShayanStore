using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Cities.v1.Commands.CreateCity
{
    public class CreateCityCommand:IRequest<bool>
    {
        public string Name { get; set; }
        public int StateId { get; set; }
    }
}
