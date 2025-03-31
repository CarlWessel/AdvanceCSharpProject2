using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project2
{
    public static class XMLExtension
    {
        public static void WriteStartDocument(this StreamWriter writer)
        {
            writer.WriteLine("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
        }

        public static void WriteStartRootElement(this StreamWriter writer)
        {
            writer.WriteLine("<root>");
        }

        public static void WriteEndRootElement(this StreamWriter writer)
        {
            writer.WriteLine("</root>");
        }

        public static void WriteStartElement(this StreamWriter writer, string elementName)
        {
            writer.WriteLine($"<{elementName}>");
        }

        public static void WriteEndElement(this StreamWriter writer, string elementName)
        {
            writer.WriteLine($"</{elementName}>");
        }

        public static void WriteElement(this StreamWriter writer, string elementName, string value)
        {
            writer.WriteLine($"<{elementName}>{value}</{elementName}>");
        }

        public static void WriteAttribute(this StreamWriter writer, string attributeName, string value)
        {
            writer.Write($"{attributeName}=\"{value}\"");
        }
    }
}
