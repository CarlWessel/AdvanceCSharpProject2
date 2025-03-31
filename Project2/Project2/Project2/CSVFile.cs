﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace Project2
{
    public class CSVData
    {
        public int sno { get; set; }
        public string infix { get; set; }
    }

    public static class CSVFile
    {
        public static List<CSVData> CSVDeserialize(string fileName)
        {
            var dataList = new List<CSVData>();
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

                // Handle both tab and comma separated files
                var separator = line.Contains('\t') ? '\t' : ',';
                var values = line.Split(separator);

                if (values.Length >= 2)
                {
                    string infixExpr = values[1].Trim();

                    // Convert date-formatted expressions (like "03-Apr" to "4-3")
                    if (infixExpr.Contains("-") && !infixExpr.Any(c => "+*/()".Contains(c)))
                    {
                        try
                        {
                            DateTime dt = DateTime.ParseExact(infixExpr, "dd-MMM", CultureInfo.InvariantCulture);
                            infixExpr = $"{dt.Month}-{dt.Day}";
                        }
                        catch
                        {
                            // If parsing fails, leave as is
                            // NOTE: Basically "fuck it, we ball"... pls fix this I'm getting a System.FormatException
                        }
                    }

                    dataList.Add(new CSVData
                    {
                        sno = int.Parse(values[0].Trim()),
                        infix = infixExpr
                    });
                }
            }

            return dataList;
        }
    }
}