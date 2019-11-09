using System;
using System.Collections.Generic;

namespace NewtonLagrange_polynom
{
    class Program
    {
        static void LagrangeMethod(Dictionary<double, double> points)
        {
            var result = new double[points.Count];
            var iteration = 0;
            foreach (var p in points)
            {
                if (p.Value == 0)
                    continue;
                var xi = p.Key;
                GetLagrangePolynom(result, points, xi, p.Value);
                iteration++;
            }

            for (var i = result.Length - 1; i >= 0; i--)
            {
                char sign;
                if (result[i] < 0)
                {
                    sign = '-';
                    result[i] = Math.Abs(result[i]);
                }                 
                else sign = '+';
                Console.Write("{0} {1} * X^{2} ", sign, result[i], i);
            }
        }

        static void GetLagrangePolynom(double[] result, Dictionary<double, double> points, double xi, double y)
        {
            var tmpArr = new double[points.Count - 1];
            var denominator = 1.0;
            int j = 0;

            foreach (var p in points)
            {
                if (xi != p.Key)
                {
                    tmpArr[j] = p.Key;
                    denominator *= xi - p.Key;
                }
                else j--;
                j++;
            }

            result[0] += tmpArr[0] * tmpArr[1] / denominator * y;
            result[1] += -(tmpArr[0] + tmpArr[1]) / denominator * y;
            result[2] += 1 / denominator * y;
        }

        static void Main(string[] args)
        {
            var points = new Dictionary<double, double>() { [2] = 0, [3] = 3, [4] = 1 };

            LagrangeMethod(points);
            Console.ReadKey();
        }
    }
}
