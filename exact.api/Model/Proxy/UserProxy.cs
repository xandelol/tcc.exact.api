using lavasim.data.Enum;
using System;
using System.Collections.Generic;
using System.Text;
using exact.api.Data.Enum;

namespace lavasim.common.Model.Proxy
{
    public class UserProxy
    {
        public Guid? Id { get; set; }

        public string Name { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

       public string Identifier { get; set; }

        public string ImageUrl { get; set; }

        public UserType Type { get; set; }

        public DateTime? LastLogin { get; set; }

        public Guid? GroupId { get; set; }
    }
}
