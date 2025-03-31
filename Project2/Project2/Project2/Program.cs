using Project2.Conversion_Processes;
using Project2.EvaluatingExpressions;
using System.Linq.Expressions;

namespace Project2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Infix to Prefix/Postfix Converter and Evaluator | Carl Wessel, Cody Sykes, Trishia Salamangkit");
            Console.WriteLine("================================================");

            try
            {
                // Step 1: Deserialize CSV input
                Console.WriteLine("\nStep 1: Reading and parsing CSV file...");
                var csvData = CSVFile.CSVDeserialize("Project 2_INFO_5101.csv");
                Console.WriteLine($"Found {csvData.Count} expressions in CSV file.");

                // Initialize lists for storage
                var prefixList = new List<string>();
                var postfixList = new List<string>();
                var prefixResults = new List<double>();
                var postfixResults = new List<double>();
                var comparisonResults = new List<bool>();

                // Step 2: Convert Infix to Prefix
                Console.WriteLine("\nStep 2: Converting Infix to Prefix notation...");
                foreach (var item in csvData)
                {
                    string prefix = InfixToPrefix.convertToPrefix(item.infix);
                    prefixList.Add(prefix);
                    Console.WriteLine($"{item.sno}. Infix: {item.infix} => Prefix: {prefix}");
                }

                // Step 3: Convert Infix to Postfix
                Console.WriteLine("\nStep 3: Converting Infix to Postfix notation...");
                foreach (var item in csvData)
                {
                    string postfix = InfixToPostfix.convertToPostfix(item.infix);
                    postfixList.Add(postfix);
                    Console.WriteLine($"{item.sno}. Infix: {item.infix} => Postfix: {postfix}");
                }

                // Step 4: Evaluate Prefix expressions
                Console.WriteLine("\nStep 4: Evaluating Prefix expressions...");
                foreach (var prefix in prefixList)
                {
                    double result = ExpressEvaluation.EvaluatePrefix(prefix);
                    prefixResults.Add(result);
                    Console.WriteLine($"Prefix: {prefix} => Result: {result}");
                }

                // Step 5: Evaluate Postfix expressions
                Console.WriteLine("\nStep 5: Evaluating Postfix expressions...");
                foreach (var postfix in postfixList)
                {
                    double result = ExpressEvaluation.EvaluatePostfix(postfix);
                    postfixResults.Add(result);
                    Console.WriteLine($"Postfix: {postfix} => Result: {result}");
                }

                // Step 6: Compare results
                Console.WriteLine("\nStep 6: Comparing evaluation results...");
                var comparer = new CompareExpressions();
                for (int i = 0; i < prefixResults.Count; i++)
                {
                    bool match = comparer.Compare(prefixResults[i], postfixResults[i]) == 0;
                    comparisonResults.Add(match);
                    Console.WriteLine($"Expression {i + 1}: Prefix and Postfix results {(match ? "match" : "do not match")}");
                }

                // Step 7: Generate XML file
                Console.WriteLine("\nStep 7: Generating XML output file...");
                string xmlFilePath = "Data/results.xml";
                using (var writer = new StreamWriter(xmlFilePath))
                {
                    // Call extension methods properly
                    writer.WriteStartDocument();
                    writer.WriteStartRootElement();

                    for (int i = 0; i < csvData.Count; i++)
                    {
                        writer.WriteStartElement("elements");

                        writer.WriteElement("sno", csvData[i].sno.ToString());
                        writer.WriteElement("infix", csvData[i].infix);
                        writer.WriteElement("prefix", prefixList[i]);
                        writer.WriteElement("postfix", postfixList[i]);
                        writer.WriteElement("evaluation", prefixResults[i].ToString());
                        writer.WriteElement("comparison", comparisonResults[i].ToString().ToLower());

                        writer.WriteEndElement("elements");
                    }

                    writer.WriteEndRootElement();
                }

                Console.WriteLine($"XML file successfully generated at: {Path.GetFullPath(xmlFilePath)}");

                // Display summary report
                Console.WriteLine("\nSummary Report:");
                Console.WriteLine("===========================================================================================================");
                Console.WriteLine("| SNO |   Infix Expression  | Postfix Notation | Prefix Notation | Prefix Result | Postfix Result | Match |");
                Console.WriteLine("===========================================================================================================");

                for (int i = 0; i < csvData.Count; i++)
                {
                    // NOTE: TEMPORARY DIRTY FIX BELOW, DO NOT KEEP IN PROGRAM.CS UNLESS NO OTHER SOLUTIONS ARE FOUND
                    string prefixResultStr = prefixResults[i] % 1 == 0 ?
                        prefixResults[i].ToString("0") : prefixResults[i].ToString("0.00");
                    string postfixResultStr = postfixResults[i] % 1 == 0 ?
                        postfixResults[i].ToString("0") : postfixResults[i].ToString("0.00");

                    // NOTE: Actual printing sequence
                    Console.WriteLine($"| {csvData[i].sno,3} | {csvData[i].infix,-19} | {postfixList[i],-16} | {prefixList[i],-15} | " +
                        $"{prefixResultStr,12} | {postfixResultStr,14} | {comparisonResults[i],6} |");
                }
                Console.WriteLine("===========================================================================================================");

                // Prompt user to open XML file
                Console.Write("\nWould you like to open the XML file in your browser? (Y/N): ");
                var key = Console.ReadKey();
                if (key.KeyChar == 'Y' || key.KeyChar == 'y')
                {
                    System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo()
                    {
                        FileName = Path.GetFullPath(xmlFilePath),
                        UseShellExecute = true
                    });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nError: {ex.Message}");
            }

            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
        }
    }
}
