using System;
using System.ComponentModel.DataAnnotations.Schema;
using exact.api.Data.Enum;

namespace exact.api.Data.Model
{
    public class UserEntity : BaseEntity
    {
        public string Name { get; set; }
        
        public string Password { get; set; }
        
        public string Email { get; set; }
               
        /// <summary>
        /// Teacher CPF or student RA
        /// </summary>
        public string Identifier { get; set; }        

        public string ImageUrl { get; set; }
        
        public UserType Type { get; set; }

        //Authenticate
        public string Token { get; set; }

        public string Roles { get; set; }
        
        public string ResetPasswordCode { get; set; }
    }
}