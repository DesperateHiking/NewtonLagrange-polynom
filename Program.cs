using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                Console.Write("{0} * X^{1} + ", result[i], i);
            }
        }

        static void GetLagrangePolynom(double[] result, double[] xArr, double y, int iteration)
        {
            var xi = xArr[iteration];

            var denominator = 1.0;

            for (int i = 0; i < xArr.Length; i++)
            {                
                if (i == iteration)
                    continue;
                denominator *= xi - xArr[i];
            }

            if (iteration == xArr.Length)
            {
                result[0] += xArr[iteration - 1] * xArr[iteration + 1] / denominator * y;
                result[1] += (xArr[iteration - 1] + xArr[iteration + 1]) / denominator * y;
            }

            result[0] += xArr[iteration - 1] * xArr[iteration + 1] / denominator * y;
            result[1] += (xArr[iteration - 1] + xArr[iteration + 1]) / denominator * y;
            result[2] += 1 / denominator;
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
