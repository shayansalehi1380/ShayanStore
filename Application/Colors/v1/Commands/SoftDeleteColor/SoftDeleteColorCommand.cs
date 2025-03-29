using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Colors.v1.Commands.SoftDeleteColor
{
    public class SoftDeleteColorCommand: IRequest<bool>
    {
        public int Id { get; set; }
    }
}
