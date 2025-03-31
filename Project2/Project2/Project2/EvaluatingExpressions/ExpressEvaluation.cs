using Project2.Conversion_Processes;
using System;
using System.Collections.Generic;

namespace Project2.EvaluatingExpressions
{
    public class ExpressEvaluation
    {
        //Tokenize expressions
        private static List<string> TokenizeExpression(string expr)
        {
            List<string> tokens = new List<string>();
            int i = 0;

            while (i < expr.Length)
            {
                if (char.IsDigit(expr[i]) || Operators.isOperator(expr[i]))
                {
                    tokens.Add(expr[i].ToString());
                }
                i++;
            }

            return tokens;
        }

        //Tree for postfix tokens
        private static Node Postfix(List<string> tokens)
        {
            Stack<Node> stack = new Stack<Node>();

            foreach (string token in tokens)
            {
                if (double.TryParse(token, out _))
                {
                    stack.Push(new Node(token));
                }
                else if (Operators.isOperator(token[0]))
                {
                    if (stack.Count < 2)
                        throw new InvalidOperationException($"Not enough operands for operator '{token}'");

                    Node right = stack.Pop();
                    Node left = stack.Pop();
                    stack.Push(new Node(token)
                    {
                        Left = left,
                        Right = right
                    });
                }
                else
                {
                    throw new ArgumentException($"Invalid token '{token}' in expression");
                }
            }

            if (stack.Count != 1)
                throw new InvalidOperationException("Invalid postfix expression");

            return stack.Pop();
        }

        //Tree for prefix tokens
        private static Node Prefix(List<string> tokens, ref int index)
        {
            if (index >= tokens.Count)
                throw new InvalidOperationException("Unexpected end of expression");

            string token = tokens[index++];
            Node node = new Node(token);

            if (node.IsOperator)
            {
                node.Left = Prefix(tokens, ref index);
                node.Right = Prefix(tokens, ref index);
            }

            return node;
        }

        private static double EvaluateTree(Node root)
        {
            if (root == null)
                throw new ArgumentException("Empty expression tree");

            if (!root.IsOperator)
            {
                if (double.TryParse(root.Value, out double result))
                    return result;
                throw new ArgumentException($"Invalid number: {root.Value}");
            }

            double left = EvaluateTree(root.Left);
            double right = EvaluateTree(root.Right);

            switch (root.Value[0])
            {
                case '+': return left + right;
                case '-': return left - right;
                case '*': return left * right;
                case '/':
                    if (right == 0) throw new DivideByZeroException();
                    return left / right;
                case '^': return Math.Pow(left, right);
                default:
                    throw new ArgumentException($"Unknown operator: {root.Value}");
            }
        }

        public static double EvaluatePostfix(string postfixExpr)
        {
            if (string.IsNullOrWhiteSpace(postfixExpr))
                throw new ArgumentException("Postfix expression cannot be empty");

            try
            {
                List<string> tokens = TokenizeExpression(postfixExpr);
                Node tree = Postfix(tokens);
                return EvaluateTree(tree);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Postfix evaluation failed: {ex.Message}");
            }
        }

        public static double EvaluatePrefix(string prefixExpr)
        {
            if (string.IsNullOrWhiteSpace(prefixExpr))
                throw new ArgumentException("Prefix expression cannot be empty");

            try
            {
                List<string> tokens = TokenizeExpression(prefixExpr);
                int index = 0;
                Node tree = Prefix(tokens, ref index);

                if (index != tokens.Count)
                    throw new InvalidOperationException("Invalid prefix expression - extra tokens found");

                return EvaluateTree(tree);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Prefix evaluation failed: {ex.Message}");
            }
        }
    }
}