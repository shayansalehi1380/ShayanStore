using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.States.v1.Commands.CreateState
{
    public class CreateStateCommand: IRequest<bool>
    {
        public string Name { get; set; }
    }
}
