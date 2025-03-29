using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.States.v1.Commands.DeleteState
{
    public class DeleteStateCommand: IRequest<bool>
    {
        public int Id { get; set; }
    }
}
