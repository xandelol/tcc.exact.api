using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace exact.api.Model.Proxy
{
    public class BaseProxy
    {
        public Guid Id { get; set; }

        public bool IsActive { get; set; } = true;
    }
}
