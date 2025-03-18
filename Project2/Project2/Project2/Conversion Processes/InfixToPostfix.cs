using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project2.Conversion_Processes
{
    public class InfixToPostfix
    {
        //Convert infix expression to postfix notation
        public static string convertToPostfix(string s)
        {
            Stack<char> st = new Stack<char>();
            string res = "";
            int sz = s.Length;

            for (int i = 0; i < sz; i++)
            {
                if (char.IsLetterOrDigit(s[i]))
                {
                    res += s[i];
                }
                else if (s[i] == '(')
                {
                    st.Push(s[i]);
                }
                else if (s[i] == ')')
                {
                    while (st.Count > 0 && st.Peek() != '(')
                    {
                        res += st.Pop();
                    }
                    st.Pop();
                }
                else
                {
                    while (st.Count > 0 && Operators.operatorPrecedence(s[i]) <= Operators.operatorPrecedence(st.Peek()))
                    {
                        res += st.Pop();
                    }
                    st.Push(s[i]);
                }
            }

            while (st.Count > 0)
            {
                res += st.Pop();
            }

            return res;
        }
    }
}
