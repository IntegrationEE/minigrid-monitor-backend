using System;
using System.Net;

namespace Monitor.Common
{
    public class CustomException : Exception
    {
        public int? StatusCode { get; private set; }
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="message"></param>
        public CustomException(string message)
            : base(message)
        { }

        public CustomException(string message, HttpStatusCode statusCode)
            : base(message)
        {
            StatusCode = (int)statusCode;
        }
    }
}
