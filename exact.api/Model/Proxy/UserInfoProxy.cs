using System;
using System.Collections.Generic;

namespace exact.api.Model.Proxy
{
    public class UserInfoProxy
    {
        public Guid Id { get; set; }

        public string Name { get; set; }
        
        public string Email { get; set; }
        
        public string Phone { get; set; } 
        
        public string ImageUrl { get; set; }
        
        public string Identifier { get; set; }
        
        public DateTime? LastLogin { get; set; }

        public Guid GroupId { get; set; }

        public List<SettingProxy> Settings { get; set; }
        
        public List<ActionProxy> Actions { get; set; }
    }
}