using System;
using System.Collections.Generic;

namespace NewtonLagrange_polynom
{
    class Program
    {
        static void NewtonMethod(Dictionary<double, double> points)
        {
            var xList = new List<double>();
            var result = new double[points.Count];
            var i = 0;

            foreach (var p in points)
            {
                xList.Add(p.Key);
                result[i] = GetDividedDifference(points, xList, i);
                i++;
            }
        }

        static double GetDividedDifference(Dictionary<double, double> points, List<double> xList, int order)
        {
            double result;

            if (order == 0)
            {
                result = points[xList[0]];
            }
            else
            {
                var tmpList1 = new List<double>();
                var tmpList2 = new List<double>();

                foreach (var e in xList)
                {
                    tmpList1.Add(e);
                    tmpList2.Add(e);
                }
                tmpList1.RemoveAt(0);
                tmpList2.RemoveAt(tmpList2.Count - 1);

                var firstDiff = GetDividedDifference(points, tmpList1, order - 1);
                var secondDiff = GetDividedDifference(points, tmpList2, order - 1);
                var denominator = xList[xList.Count - 1] - xList[0];

                result = (firstDiff - secondDiff) / denominator;
            }
            return result;
        }

        static void LagrangeMethod(Dictionary<double, double> points)
        {
            var result = new double[points.Count];

            foreach (var p in points)
            {
                if (p.Value == 0)
                    continue;
                var xi = p.Key;
                GetLagrangePolynom(result, points, xi, p.Value);
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
            NewtonMethod(points);
            Console.ReadKey();
        }
    }
}
