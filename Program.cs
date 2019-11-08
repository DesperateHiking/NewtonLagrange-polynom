using System;

namespace NewtonLagrange_polynom
{
    class Program
    {
        static void LagrangeMethod(double[] xArr, double[] yArr)
        {
            var result = new double[xArr.Length];

            for (var i = 0; i < xArr.Length; i++)
            {
                if (yArr[i] == 0)
                    continue;
                GetLagrangePolynom(result, xArr, yArr[i], i);
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

        static void GetLagrangePolynom(double[] result, double[] xArr, double y, int iteration)
        {
            var xi = xArr[iteration];
            var tmpArr = new double[xArr.Length - 1];
            var denominator = 1.0;

            for (int i = 0, j = 0; i < xArr.Length; i++, j++)
            {
                if (xi != xArr[i])
                {
                    tmpArr[j] = xArr[i];                    
                }
                else j--;

                if (i == iteration)
                    continue;
                denominator *= xi - xArr[i];
            }

            result[0] += tmpArr[0] * tmpArr[1] / denominator * y;
            result[1] += -(tmpArr[0] + tmpArr[1]) / denominator * y;
            result[2] += 1 / denominator * y;
        }

        static void Main(string[] args)
        {
            var xArr = new double[] { 2, 3, 4 };
            var yArr = new double[] { 0, 3, 1 };

            LagrangeMethod(xArr, yArr);
            Console.ReadKey();

        }
    }
}
