using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCalculator
{
    public class CalculationOperationNotSupportedException : CalculationException
    {
        public string Operation { get; }

        /// <summary>
        /// Creates a new <see cref="CalculationOperationNotSupportedException"/> with a predefined message.
        /// </summary>
        public CalculationOperationNotSupportedException() : base ("Specified operation was out of the range of valid values")
        {
        }

        /// <summary>
        /// Creates a new <see cref="CalculationOperationNotSupportedException"/> with the operation that is not supported.
        /// </summary>
        public CalculationOperationNotSupportedException(string operation) : this() // this() - sets default msg. stated above
        {
            Operation = operation;
        }

        /// <summary>
        /// Creates a new <see cref="CalculationOperationNotSupportedException"/> with a user-defined message and wrapped inner exception.
        /// </summary>
        public CalculationOperationNotSupportedException(string message, Exception innerException)  
            : base (message, innerException)
        {
        }

        /// <summary>
        /// Creates a new <see cref="CalculationOperationNotSupportedException"/> with a user-defined message and the operation that is not supported.
        /// </summary>
        public CalculationOperationNotSupportedException(string operation, string message) : base (message)
        {
            Operation = operation;
        }

        public override string Message
        {
            get
            {
                string message = base.Message;

                if (Operation != null)
                {
                    return message + Environment.NewLine + $"Unsupported operation {Operation}";
                }

                return message;
            }
        }
    }
}
