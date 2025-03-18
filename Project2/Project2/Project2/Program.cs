using Project2.Conversion_Processes;
using System.Linq.Expressions;

namespace Project2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<string> L1 = CSVFile.CSVDeserialize("Project 2_INFO_5101.csv");
            List<string> L2 = CSVFile.CSVDeserialize("Project 2_INFO_5101.csv");

            foreach (string line in L1)
            {
                string ex = InfixToPostfix.convertToPostfix(line);
                Console.WriteLine(ex);
            }

            Console.WriteLine();

            foreach (string line in L2)
            {
                string ex = InfixToPrefix.convertToPrefix(line);
                Console.WriteLine(ex);
            }

        }
    }
}
