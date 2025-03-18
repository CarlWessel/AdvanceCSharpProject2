using System;
using System.Collections.Generic;
using System.IO;

namespace Project2
{
    public class CSVFile
    {
        public static List<string> CSVDeserialize(string fileName)
        {
            var dataList = new List<string>();

            using (var reader = new StreamReader(Path.Combine("data", fileName)))
            {
                var headers = reader.ReadLine();
                if (headers == null) throw new Exception("CSV file is empty or has no headers.");

                string? line;
                while ((line = reader.ReadLine()) != null)
                {
                    var values = line.Split(',');
                    // Skip the first value, which is sno column
                    for (int i = 1; i < values.Length; i++)
                    {
                        dataList.Add(values[i].Trim());
                    }

                    //With sno column
                    //foreach (var value in values)
                    //{
                    //    dataList.Add(value.Trim());
                    //}
                }
            }

            return dataList;
        }
    }
}