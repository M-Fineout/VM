using System;

namespace ConsoleCalculator
{
    public class CalculationException : Exception
    {
        private static readonly string DefaultMessage = "An error occurred during a calculation. Ensure that the operator is supported and the values are within the valid ranges for the requested operation.";


        /// <summary>
        /// Creates a new <see cref="CalculationException"/> with a predefined message.
        /// </summary>
        public CalculationException() : base(DefaultMessage)
        {
        }

        /// <summary>
        /// Creates a new <see cref="CalculationException"/> with a user-defined message.
        /// </summary>
        public CalculationException(string message) : base(message)
        {
        }

        /// <summary>
        /// Creates a new <see cref="CalculationException"/> with a predefined message and a wrapped inner exception.
        /// </summary>
        public CalculationException(Exception innerException) : base(DefaultMessage, innerException)
        {
        }

        /// <summary>
        /// Creates a new <see cref="CalculationException"/> with a user-defined message and a wrapped inner exception.
        /// </summary>
        public CalculationException(string message, Exception innerException) : base(message, innerException)
        { 
        }
    }
}
