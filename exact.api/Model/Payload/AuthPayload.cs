using System.ComponentModel.DataAnnotations;
using exact.api.Data.Enum;

namespace exact.api.Model.Payload
{   
    /// <summary>
    ///  <see cref="User"/> login information
    /// </summary>
    public class AuthPayload
    {
        /// <summary>
        ///     <see cref="User.Username"/>
        /// </summary>
        [Required]
        public string Username { get; set; }
        /// <summary>
        ///     <see cref="User.Password"/>
        /// </summary>
        [Required]
        public string Password { get; set; }
        /// <summary>
        ///     <see cref="User.Type"/>
        /// </summary>
        [Required]
        public UserType Type { get; set; }
    }
}