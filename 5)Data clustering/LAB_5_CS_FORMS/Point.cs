using System;
using System.Collections.Generic;
using System.Diagnostics;


namespace Lab5AI
{
    delegate double Metric(Point p1, Point p2);

    struct Point
    {
        double _x;
        double _y;

        public double x => _x;
        public double y => _y;
        static Metric _currentMetric;


            // (3):
        //  Реалізувати допоміжну функцію для обчислення міри віддалі:
        static public Metric CurrentMetric => _currentMetric;
        public void Clear()
        {
            _x = 0;
            _y = 0;
            _currentMetric = EuclideanDistance;
        }


        public override string ToString()
        {
            return x.ToString() + ", " + y.ToString();
        }


        public static double EuclideanDistance(Point p1,Point p2)
        {
            return Math.Sqrt(Math.Pow((p1.x - p2.x), 2) + Math.Pow((p1.y - p2.y), 2));
        }


        public static double SquareEuclideanDistance(Point p1,Point p2)
        {
            return Math.Pow((p1.x - p2.x), 2) + Math.Pow((p1.y - p2.y), 2);
        }


        public static Point operator +(Point p1,Point p2)
        {
            return new Point(p1.x + p2.x, p1.y + p2.y);
        }


        public static Point operator -(Point p1, Point p2)
        {
            return new Point(p1.x - p2.x, p1.y - p2.y);
        }


        public static Point operator /(Point p1, int num)
        {
            return new Point(p1.x/num, p1.y /num);
        }


        public Point(double x, double y)
        {
            _x = x;
            _y = y;
        }


        public static double[][] ConvertToDouble(List<Point> points)
        {
            double[][] outPoints = new double[2][];
            outPoints[0] = new double[points.Count];
            outPoints[1] = new double[points.Count];
            for (int i=0;i<points.Count;++i)
            {
                outPoints[0][i] = points[i].x;
                outPoints[1][i] = points[i].y;
                Debug.WriteLine(outPoints[0][i] + ", " + outPoints[1][i]);

            }
            return outPoints;    
        }


        public static double Norm(Point p)
        {
            return Math.Sqrt(p.x * p.x + p.y * p.y);
        }
    }
}