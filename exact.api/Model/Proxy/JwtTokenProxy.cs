using System;

namespace exact.api.Model.Proxy
{
    public class JwtTokenProxy
    {
        public string Token { get; set; }
        
        public DateTime Expiration { get; set; }
    }
}