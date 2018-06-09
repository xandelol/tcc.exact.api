using System.ComponentModel.DataAnnotations;
using exact.api.Data.Enum;

namespace exact.api.Model.Payload
{
    public class AuthPayload
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
        
        [Required]
        public UserType Type { get; set; }
    }
}