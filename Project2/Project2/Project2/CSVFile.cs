// Carl Wessel, Cody Sykes, Trishia Salamangkit
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project2
{
    public static class CSVFile
    {

        public static List<(int sno, string infix)> CSVDeserialize(string fileName)
        {
            // De-serialize CSV input to C# List named InFix (CSVFile class)
            var InFix = new List<(int sno, string infix)>();
            string filePath = Path.Combine("Data", fileName);

            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException($"CSV file not found at: {Path.GetFullPath(filePath)}");
            }

            var lines = File.ReadAllLines(filePath);

            // Skip header row
            foreach (var line in lines.Skip(1))
            {
                if (string.IsNullOrWhiteSpace(line)) continue;

                var separator = line.Contains('\t') ? '\t' : ',';
                var values = line.Split(separator);

                if (values.Length >= 2)
                {
                    string infixExpr = values[1].Trim();

                    if (infixExpr.Contains("-") && !infixExpr.Any(c => "+*/()".Contains(c)))
                    {
                        try
                        {
                            if (DateTime.TryParseExact(infixExpr, new[] { "dd-MMM", "yyyy-MM-dd" },
                                CultureInfo.InvariantCulture,
                                DateTimeStyles.None,
                                out DateTime dt))
                            {
                                infixExpr = $"{dt.Month}-{dt.Day}";
                            }
                        }
                        catch
                        {
                            Console.WriteLine($"Invalid date format: {infixExpr}");
                        }
                    }

                    InFix.Add((int.Parse(values[0].Trim()), infixExpr));
                }
            }

            return InFix;
        }
    }
}
