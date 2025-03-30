using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Guarantees.v1.Commands.CreateGuarantee
{
    public class CreateGuaranteeCommand: IRequest<bool>
    {
        public string Name { get; set; } = string.Empty;
    }
}
