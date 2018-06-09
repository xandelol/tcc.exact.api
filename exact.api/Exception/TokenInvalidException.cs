namespace exact.api.Exception
{
    /// <summary>
    /// When token was expired
    /// When has been changed token (of it was logged in another device)
    /// </summary>
    public class TokenInvalidException: System.Exception
    {
        public TokenInvalidException(string message) : base (message)
        {

        }
    }
}