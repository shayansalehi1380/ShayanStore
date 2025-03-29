using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Colors.v1.Commands.UpdateColor
{
    public class UpdateColorCommand: IRequest<bool>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string CodeColor { get; set; }
    }
}
