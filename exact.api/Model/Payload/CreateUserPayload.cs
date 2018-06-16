﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace exact.api.Model.Payload
{
    public class CreateUserPayload
    {
        public string Name { get; set; }

        public string Email { get; set; }

        public string Identifier { get; set; }

        public string Password { get; set; }

        public string Roles { get; set; }
    }
}
