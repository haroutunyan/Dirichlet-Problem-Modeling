using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Animations
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        static int N;
        const int Radius = 150;
        const int PointRadius = 3;
        static double Limit;
        Point[] Points;
        List<List<Point>> AllTrajectories;
        static int step = 0;
        bool IsPointsDrawn = false;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void PrepareCanvas()
        {
            Ellipse circle = new Ellipse();
            circle.Height = 2 * Radius;
            circle.Width = 2 * Radius;
            circle.Fill = Brushes.LightGray;
            circle.Stroke = Brushes.Black;
            circle.StrokeThickness = 2;

            Canvas.SetLeft(circle, (canvas.ActualWidth - circle.Width) / 2);
            Canvas.SetTop(circle, (canvas.ActualHeight - circle.Height) / 2);

            canvas.Children.Add(circle);

        }

        private void PrepareSmallSpace()
        {
            if (double.TryParse(epsilionBox.Text, out Limit))
            {
                Ellipse smallCircle = new Ellipse();
                smallCircle.Height = 2 * Radius - 2 * Limit * Radius;
                smallCircle.Width = 2 * Radius - 2 * Limit * Radius;
                smallCircle.Fill = Brushes.WhiteSmoke;
                smallCircle.Stroke = Brushes.Black;

                Canvas.SetLeft(smallCircle, (canvas.ActualWidth - smallCircle.Width) / 2);
                Canvas.SetTop(smallCircle, (canvas.ActualHeight - smallCircle.Height) / 2);

                canvas.Children.Add(smallCircle);
            }
        }

        private async Task DrawPoints()
        {            
            for (int i = 0; i < Points.Length; i++)
            {
                double x = Points[i].Radius * Math.Cos(Points[i].Angle) * Radius;
                double y = Points[i].Radius * Math.Sin(Points[i].Angle) * Radius;

                Canvas.SetLeft(Points[i].Ellipse, canvas.ActualWidth / 2 + x);
                Canvas.SetTop(Points[i].Ellipse, canvas.ActualHeight / 2 - y);
                //await Task.Delay(100);

                canvas.Children.Add(Points[i].Ellipse);
            }

            IsPointsDrawn = true;
        }

        private void ClearPoints()
        {
            //for (int i = 0; i < Points?.Length; i++)
            //{
            //    canvas.Children.Remove(Points[i].Ellipse);
            //}
            canvas.Children.RemoveRange(0, canvas.Children.Count);
            IsPointsDrawn = false;
        }

        private void GeneratePoints()
        {
            Random random = new Random();
            Points = new Point[N];
            int index = 0;
            while (true)
            {
                if (index == N)
                    break;

                int k = random.Next(1, N + 1);
                int m = random.Next(1, N + 1);
                double radius = (double)k / N;
                double angle = (double)2 * Math.PI * m / N;

                if (!Points.Any(p => p?.Angle == angle && p?.Radius == radius))
                {
                    Point point = new Point
                    {
                        Radius = radius,
                        Angle = angle,
                        Ellipse = new Ellipse()
                    };
                    point.Ellipse.Width = PointRadius;
                    point.Ellipse.Height = PointRadius;
                    point.Ellipse.Fill = Brushes.Red;

                    Points[index] = point;
                    index++;
                }
            }
        }

        private async void StepByStepButton_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(countBox.Text, out N))
            {
                ClearPoints();
                PrepareCanvas();
                PrepareSmallSpace();
                if (Points == null)
                {
                    GeneratePoints();
                }
                await DrawPoints();
                if (AllTrajectories == null)
                {
                    GenerateRandomTrajectories();
                }

                DrawGeneratedTrajectories(step);
                step++;
            }
            else
            {
                MessageBox.Show("Please input valid count");
            }
        }

        private void ShowResultsButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private async void ShowAllButton_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(countBox.Text, out N))
            {
                ClearPoints();
                PrepareCanvas();
                PrepareSmallSpace();
                GeneratePoints();
                await DrawPoints();
                GenerateRandomTrajectories();
                
                for (int i = step; i < AllTrajectories.Count; i++)
                {
                    DrawGeneratedTrajectories(i);
                }
            }
            else
            {
                MessageBox.Show("Please input valid count");
            }
        }

        private void GenerateRandomTrajectories()
        {
            Random random = new Random();
            
            double limit = 1 - Limit;
            AllTrajectories = new List<List<Point>>();
            int randomStartIndex = 0;
            do
            {
                randomStartIndex = random.Next(N);
            } while (Points[randomStartIndex].Radius >= limit);

            for (int i = 1; i < N; i++)
            {
                List<Point> points = new List<Point>();
                points.Add(Points[randomStartIndex]);

                Point point;
                while (true)
                {
                    int randomIndex = random.Next(N);

                    points.Add(Points[randomIndex]);

                    if (Points[randomIndex].Radius >= limit)
                    {
                        point = Points[randomIndex];
                        break;
                    }
                }
                
                AllTrajectories.Add(points);
            }            
        }

        private async void DrawGeneratedTrajectories(int step)
        {
            if (step < AllTrajectories.Count)
            {
                var allTrajectoriesArray = AllTrajectories.ToArray();
                var pointsArray = allTrajectoriesArray[step].ToArray();

                for (int j = 0; j < pointsArray.Length - 1; j++)
                {
                    Line line = new Line();

                    double x1 = pointsArray[j].Radius * Math.Cos(pointsArray[j].Angle) * Radius + PointRadius / 2;
                    double y1 = pointsArray[j].Radius * Math.Sin(pointsArray[j].Angle) * Radius - PointRadius / 2;
                    double x2 = pointsArray[j + 1].Radius * Math.Cos(pointsArray[j + 1].Angle) * Radius + PointRadius / 2;
                    double y2 = pointsArray[j + 1].Radius * Math.Sin(pointsArray[j + 1].Angle) * Radius - PointRadius / 2;

                    line.X1 = canvas.ActualWidth / 2 + x1;
                    line.Y1 = canvas.ActualHeight / 2 - y1;
                    line.X2 = canvas.ActualWidth / 2 + x2;
                    line.Y2 = canvas.ActualHeight / 2 - y2;
                    line.Stroke = Brushes.Green;
                    line.StrokeThickness = 2;

                    canvas.Children.Add(line);
                    await Task.Delay(100);
                }
            }
            else
            {
                MessageBox.Show("That's All");
            }
        }

        double getFunctionValue(double radius, double angle)
        {
            return radius * Math.Cos(angle);
        }
    }
}
