// Carl Wessel, Cody Sykes, Trishia Salamangkit
using Project2.Conversion_Processes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 This class is used to create a node to traverse through the expression tree
 */

namespace Project2.EvaluatingExpressions
{
    public class Node
    {
        public string Value { get; set; }
        public Node Left { get; set; }
        public Node Right { get; set; }

        public Node(string value)
        {
            Value = value;
            Left = null;
            Right = null;
        }

        public bool IsOperator => Value.Length == 1 && Operators.isOperator(Value[0]);
    }
}
