namespace exact.api.Exception
{
    /// <summary>
    /// When a invaid argument is sent.
    /// Param is no null but it is still not valid
    /// </summary>
    public class InvalidArgumentException : System.Exception
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="param">Invalid param's name</param>
        public InvalidArgumentException(string param): base($"Invalid {param}")
        {
            //Param = param;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="param">Invalid param's name</param>
        /// <param name="message">Error message</param>
        public InvalidArgumentException(string param, string  message): base(message)
        {
            // Param = param;
        }
        
     
    }
}