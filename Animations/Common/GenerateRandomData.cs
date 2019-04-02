using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Animations.Common
{
    public static class GenerateRandomData
    {
        public static List<List<Point>> GenerateRandomTrajectories(double limit, Point[] points)
        {
            Random random = new Random();

            List<List<Point>> AllTrajectories = new List<List<Point>>();
            int randomStartIndex = 0;
            do
            {
                randomStartIndex = random.Next(points.Length);
            } while (points[randomStartIndex].Radius >= limit);

            for (int i = 1; i < points.Length; i++)
            {
                List<Point> trajectoryPoints = new List<Point>();
                trajectoryPoints.Add(points[randomStartIndex]);

                Point point;
                while (true)
                {
                    int randomIndex = random.Next(points.Length);

                    trajectoryPoints.Add(points[randomIndex]);

                    if (points[randomIndex].Radius >= limit)
                    {
                        point = points[randomIndex];
                        break;
                    }
                }

                AllTrajectories.Add(trajectoryPoints);
            }

            return AllTrajectories;
        }

        public static Point[] GenerateRandomPoints(int n, int? pointRadius = null)
        {
            Random random = new Random();
            Point[] points = new Point[n];

            int index = 0;
            while (true)
            {
                if (index == n)
                    break;

                int k = random.Next(1, n + 1);
                int m = random.Next(1, n + 1);
                double radius = (double)k / n;
                double angle = (double)2 * Math.PI * m / n;

                if (!points.Any(p => p?.Angle == angle && p?.Radius == radius))
                {
                    Point point = new Point
                    {
                        Radius = radius,
                        Angle = angle,
                        Ellipse = new Ellipse()
                    };

                    if (pointRadius != null)
                    {
                        point.Ellipse.Width = (double)pointRadius;
                        point.Ellipse.Height = (double)pointRadius;
                        point.Ellipse.Fill = Brushes.Red;
                    }

                    points[index] = point;
                    index++;
                }
            }

            return points;
        }
    }
}
