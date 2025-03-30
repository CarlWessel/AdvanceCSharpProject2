using Project2.Conversion_Processes;
using Project2.EvaluatingExpressions;
using System.Linq.Expressions;

namespace Project2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Testing to see if I 
            List<string> L1 = CSVFile.CSVDeserialize("Project 2_INFO_5101.csv");
            List<string> L2 = CSVFile.CSVDeserialize("Project 2_INFO_5101.csv");

            //double result1 = ExpressEvaluation.EvaluatePostfix("54+");

            //foreach (string line in L1)
            //{
            //    try
            //    {
            //        string ex = InfixToPostfix.convertToPostfix(line);
            //        Console.WriteLine($"Infix: {line} → Postfix: {ex}");

            //        double re = ExpressEvaluation.EvaluatePostfix(ex);
            //        Console.WriteLine($"Result: {re}");
            //    }
            //    catch (Exception e)
            //    {
            //        Console.WriteLine($"Error evaluating '{line}': {e.Message}\n");
            //    }
            //}

            foreach (string line in L2)
            {
                try
                {
                    string ex = InfixToPrefix.convertToPrefix(line);
                    Console.WriteLine($"Infix: {line} → prefix: {ex}");

                    double re = ExpressEvaluation.EvaluatePrefix(ex);
                    Console.WriteLine($"Result: {re}");
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Error evaluating '{line}': {e.Message}\n");
                }
            }

            Console.WriteLine();

            //foreach (string line in L2)
            //{
            //    string ex = InfixToPrefix.convertToPrefix(line);
            //    Console.WriteLine(ex);
            //}

        }
    }
}
