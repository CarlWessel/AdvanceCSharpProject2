using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project2
{
    public class CompareExpressions : IComparer<double>
    {
        public int Compare(double x, double y)
        {
            // Compare with a small tolerance for floating point precision
            const double tolerance = 0.0001;
            if (Math.Abs(x - y) < tolerance)
                return 0;
            return x.CompareTo(y);
        }
    }
}
