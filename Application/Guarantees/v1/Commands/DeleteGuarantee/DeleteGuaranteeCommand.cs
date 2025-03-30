using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Guarantees.v1.Commands.DeleteGuarantee
{
    public class DeleteGuaranteeCommand: IRequest<bool>
    {
        public int Id { get; set; }
    }
}
