using Animations.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Animations.Views
{
    /// <summary>
    /// Interaction logic for SingleCalculationWindow.xaml
    /// </summary>
    public partial class SingleCalculationWindow : Window
    {
        readonly double Limit;
        readonly int N;
        Point[] Points;
        List<List<Point>> AllTrajectories;
        static int step = 0;
        const int Radius = 150;
        const int PointRadius = 3;

        public SingleCalculationWindow(int count, double limit)
        {
            Limit = limit;
            N = count;

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
            Ellipse smallCircle = new Ellipse();
            smallCircle.Height = 2 * Radius - 2 * Limit * Radius;
            smallCircle.Width = 2 * Radius - 2 * Limit * Radius;
            smallCircle.Fill = Brushes.WhiteSmoke;
            smallCircle.Stroke = Brushes.Black;

            Canvas.SetLeft(smallCircle, (canvas.ActualWidth - smallCircle.Width) / 2);
            Canvas.SetTop(smallCircle, (canvas.ActualHeight - smallCircle.Height) / 2);

            canvas.Children.Add(smallCircle);
        }

        private void DrawPoints()
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
        }

        private void ClearPoints()
        {
            canvas.Children.RemoveRange(0, canvas.Children.Count);
        }

        private void ShowAllButton_Click(object sender, RoutedEventArgs e)
        {
            ClearPoints();
            PrepareCanvas();
            PrepareSmallSpace();
            Points = GenerateRandomData.GenerateRandomPoints(N, PointRadius);
            DrawPoints();
            AllTrajectories = GenerateRandomData.GenerateRandomTrajectories(1 - Limit, Points);

            for (int i = step; i < AllTrajectories.Count; i++)
            {
                DrawGeneratedTrajectories(i);
            }
        }

        private void ShowResultsButton_Click(object sender, RoutedEventArgs e)
        {
            double solution = 0;
            foreach (var trajectory in AllTrajectories)
            {
                Point lastPoint = trajectory.LastOrDefault();

                solution += EdgeValue.GetEdgeValue(lastPoint.Radius, lastPoint.Angle);
            }

            Point firstPoint = AllTrajectories.FirstOrDefault().FirstOrDefault();
            double functionValue = EdgeValue.GetEdgeValue(firstPoint.Radius, firstPoint.Angle);

            MessageBox.Show(string.Format("Solution: {0} \nFunction: {1}", solution / AllTrajectories.Count, functionValue));
        }

        private void StepByStepButton_Click(object sender, RoutedEventArgs e)
        {
            ClearPoints();
            PrepareCanvas();
            PrepareSmallSpace();
            if (Points == null)
            {
                Points = GenerateRandomData.GenerateRandomPoints(N, PointRadius);
            }
            DrawPoints();
            if (AllTrajectories == null)
            {
                AllTrajectories = GenerateRandomData.GenerateRandomTrajectories(1 - Limit, Points);
            }

            DrawGeneratedTrajectories(step);
            step++;
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
    }
}
