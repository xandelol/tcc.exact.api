using System;
using System.Collections.Generic;

namespace exact.api.Model
{
    public class UserToken
    {
        public Guid? Id { get; set; }
        public IEnumerable<string> Roles { get; set; }
    }
}