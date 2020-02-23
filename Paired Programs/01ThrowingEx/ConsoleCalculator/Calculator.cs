using System;

namespace ConsoleCalculator
{
    public class Calculator
    {
        public int Calculate(int number1, int number2, string operation)
        {
            //if (operation is null)
            //{
            //    throw new ArgumentNullException(nameof(operation));
            //}

            //throw new ArgumentNullException(nameof(number1));

            string nonNullOperation = operation ?? throw new ArgumentNullException(nameof(operation));

            if (nonNullOperation == "/")
            {
               
                try
                {
                    return Divide(number1, number2);
                }
                catch (ArithmeticException ex)
                {
                    //Log.Error(ex)
                    //throw(ex) -- do NOT do this, it will not return the proper call (location) where the exception occurred
                    //throw;
                    throw new CalculationException("An error occurred during division.", ex); //pass inner exception
                }
            }
            else
            {
                throw new CalculationOperationNotSupportedException(operation);
                

                //Console.WriteLine("Unknown operation.");
                //return 0;

            }
        }
        
        private int Divide(int number, int divisor)
        {
            return number / divisor;
        }
    }
}

