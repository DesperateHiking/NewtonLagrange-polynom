using System;
using System.Collections.Generic;

namespace NewtonLagrange_polynom
{
    class Program
    {
        static void NewtonMethod(Dictionary<double, double> points)
        {
            var xList = new List<double>();
            var diffArr = new double[points.Count];
            var result = new double[points.Count];
            var i = 0;

            foreach (var p in points)
            {
                xList.Add(p.Key);
                diffArr[i] = GetDividedDifference(points, xList, i);
                if (i == 0)
                    result[0] += diffArr[i];
                if (i == 1)
                {
                    result[0] += -xList[0] * diffArr[i];
                    result[1] += diffArr[i];
                }
                if (i == 2)
                {
                    result[0] += xList[0] * xList[1] * diffArr[i];
                    result[1] += -(xList[0] + xList[1]) * diffArr[i];
                    result[2] += diffArr[i];
                }
                i++;
            }

            Console.WriteLine("Ньютон");
            Print(result);
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

            Console.WriteLine("Лагранж");
            Print(result);
        }

        static void Print(double[] result)
        {
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
            Console.WriteLine();
        }

        static void GetLagrangePolynom(double[] result, Dictionary<double, double> points, double xi, double y)
        {
            var tmpList = new List<double>();
            var denominator = 1.0;

            foreach (var p in points)
            {
                if (xi == p.Key)
                    continue;
                else
                {
                    tmpList.Add(p.Key);
                    denominator *= xi - p.Key;
                }
            }

            result[0] += tmpList[0] * tmpList[1] / denominator * y;
            result[1] += -(tmpList[0] + tmpList[1]) / denominator * y;
            result[2] += 1 / denominator * y;
        }

        static void Main(string[] args)
        {
            var points = new Dictionary<double, double>()
            {
                [-2] = 1,
                [1] = 4,
                [4] = 1
            };

            LagrangeMethod(points);
            NewtonMethod(points);
            Console.ReadKey();
        }
    }
}
