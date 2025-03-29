using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.States.v1.Commands.SoftDeleteState
{
    public class SoftDeleteStateCommand: IRequest<bool>
    {
        public int Id { get; set; }
    }
}
