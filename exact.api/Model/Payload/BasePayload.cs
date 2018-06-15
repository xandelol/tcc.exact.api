using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace exact.api.Model.Payload
{
    public class BasePayload
    {

        public Guid? Id { get; set; }

        public bool IsActive { get; set; } = true;
    }
}
