using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Guarantees.v1.Commands.SoftDeleteGuarantee
{
    public class SoftDeleteGuaranteeCommand: IRequest<bool>
    {
        public int Id { get; set; }
    }
}
