using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MedEcommerce_Core.CustomExceptions
{
    public class UseremailAlreadyExistsException : Exception
    {
        public UseremailAlreadyExistsException()
        {
        }

        public UseremailAlreadyExistsException(string? message) : base(message)
        {
        }

        public UseremailAlreadyExistsException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected UseremailAlreadyExistsException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
