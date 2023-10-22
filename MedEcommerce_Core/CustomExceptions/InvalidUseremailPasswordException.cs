using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MedEcommerce_Core.CustomExceptions
{
    public class InvalidUseremailPasswordException : Exception
    {
        public InvalidUseremailPasswordException()
        {
        }

        public InvalidUseremailPasswordException(string? message) : base(message)
        {
        }

        public InvalidUseremailPasswordException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected InvalidUseremailPasswordException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
