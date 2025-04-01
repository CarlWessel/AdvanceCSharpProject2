// Carl Wessel, Cody Sykes, Trishia Salamangkit
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project2.Conversion_Processes
{
    public class InfixToPrefix
    {
        // Convert infix expression to prefix notation
        public static string convertToPrefix(string infix)
        {
            char[] arr = infix.ToCharArray();
            Array.Reverse(arr);
            infix = new string(arr);

            for (int i = 0; i < arr.Length; i++)
            {
                if (arr[i] == '(')
                {
                    arr[i] = ')';
                }
                else if (arr[i] == ')')
                {
                    arr[i] = '(';
                }
            }

            string postfix = InfixToPostfix.convertToPostfix(new string(arr), true);
            char[] postArr = postfix.ToCharArray();
            Array.Reverse(postArr);
            return new string(postArr);
        }
    }
}
