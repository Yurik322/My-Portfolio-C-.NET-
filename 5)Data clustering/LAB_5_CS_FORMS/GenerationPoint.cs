using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Lab5AI
{
        // (2):
    // Згенерувати тестову послідовність з N значень(для визначеності, можна
    // покласти N≥1000), що є парами дійсних чисел на одиничному квадраті:
    class GenerationPoint
    {
        List<Point> _point = new List<Point>();

        public GenerationPoint(int n)
        {
            Generate(n);
        }

        void Generate(int nPoint)
        {
            // Функція рандому:
            Random rnd = new Random();

            for (int i = 0; i < nPoint; ++i)
            {
                 _point.Add(new Point(rnd.NextDouble(),rnd.NextDouble()));
            }
            #region MyRegion
            //double temp;
            //for (int i = 0; i < nPoint; ++i)
            //{
            //    temp = rnd.NextDouble();
            //    if (i < nPoint / 2)
            //        _point.Add(new Point(temp, temp * temp));
            //    else _point.Add(new Point(temp, temp));
            //}
            #endregion
        }
        public List<Point> TwoDPoint => _point;

    }
}