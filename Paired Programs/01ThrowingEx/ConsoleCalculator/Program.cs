using System;
using static System.Console;

namespace ConsoleCalculator
{
    class Program
    {
        static void Main(string[] args)
        {

            AppDomain currentAppDomain = AppDomain.CurrentDomain;
            currentAppDomain.UnhandledException += new UnhandledExceptionEventHandler(HandleException);

            WriteLine("Enter first number");
            int number1 = int.Parse(ReadLine());

            WriteLine("Enter second number");
            int number2 = int.Parse(ReadLine());

            WriteLine("Enter operation");
            string operation = ReadLine().ToUpperInvariant();


            var calculator = new Calculator();

            try
            {
                int result = calculator.Calculate(number1, number2, operation);
                DisplayResult(result);
            }
                //catch list for demos
                 //catch (ArgumentNullException ex) when (ex.ParamName == "operation") //specific ex w/ ex filter
            //{
            //    // Log.Error(ex)
            //    WriteLine($"The operation was not provided. {ex}");
            //}
            //catch (ArgumentNullException ex)//specific ex
            //{
            //    // Log.Error(ex)
            //    WriteLine($"An argument was null. {ex}");
            //}
            //catch (ArgumentOutOfRangeException ex) //specific ex
            //{
            //    // Log.Error(ex)
            //    WriteLine($"The operation is not supported. {ex}");
            //}

            catch (CalculationException ex)
            {
                //Log.Error(ex)
                WriteLine(ex);
            }
            catch (Exception ex) //non-specific ex
            {
                WriteLine($"Sorry, something went wrong. {ex}");
            }
            finally //executes whether ex has been thrown or not. (ALWAYS executes)
            {
                //Could be calling dispose..
                //used for cleanup
                WriteLine("...finally...");
            }
            
            
            WriteLine("\nPress enter to exit.");
            ReadLine();
        }

        private static void HandleException(object sender, UnhandledExceptionEventArgs e)
        {
            WriteLine($"Sorry, there was a problem and we need to close. Details: {e.ExceptionObject}");
        }

        private static void DisplayResult(int result)
        {
            WriteLine($"Result is: {result}");
        }
    }
}
