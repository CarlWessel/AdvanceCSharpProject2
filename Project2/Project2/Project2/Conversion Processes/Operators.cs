// Carl Wessel, Cody Sykes, Trishia Salamangkit
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project2.Conversion_Processes
{
    public static class Operators
    {
        // Check if character is an operator
        public static bool isOperator(char ch)
        {
            return (ch == '+' || ch == '-' ||
                    ch == '*' || ch == '/' || ch == '^');
        }

        // Get precedence of operators
        public static int operatorPrecedence(char op)
        {
            if (op == '^') return 3;
            if (op == '*' || op == '/') return 2;
            if (op == '+' || op == '-') return 1;
            return -1;
        }
    }
}
