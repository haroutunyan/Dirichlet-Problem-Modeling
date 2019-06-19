using Animations.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls.DataVisualization.Charting;

namespace Animations.Views
{
    /// <summary>
    /// Interaction logic for MultipleCalculationWindow.xaml
    /// </summary>
    public partial class MultipleCalculationWindow : Window
    {
        readonly int N1;
        readonly int N2;
        readonly int N3;
        readonly int N4;
        readonly double Limit;

        public MultipleCalculationWindow(int n1, int n2, int n3, int n4, double limit)
        {
            N1 = n1;
            N2 = n2;
            N3 = n3;
            N4 = n4;
            Limit = limit;

            InitializeComponent();
            LoadColumnChartData();
        }

        private void LoadColumnChartData()
        {
            Point[] n1Points = GenerateRandomData.GenerateRandomPoints(N1);
            List<double> n1Differences = new List<double>();
            for (int i = 0; i < n1Points.Length; i++)
            {
                List<List<Point>> n1Trajectories = GenerateRandomData.GenerateRandomTrajectoriesByStartPoint(Limit, n1Points[i], n1Points);
                double edgeValue1 = EdgeValue.GetEdgeValue(n1Points[i].Radius, n1Points[i].Angle);
                double difference = GetSolutionByTrajectories(n1Trajectories);
                n1Differences.Add(Math.Round(Math.Abs(difference - edgeValue1), 3));
            }
            double difference1 = GetAverageDifference(n1Differences);

            Point[] n2Points = GenerateRandomData.GenerateRandomPoints(N2);
            List<double> n2Differences = new List<double>();
            for (int i = 0; i < n2Points.Length; i++)
            {
                List<List<Point>> n2Trajectories = GenerateRandomData.GenerateRandomTrajectoriesByStartPoint(Limit, n2Points[i], n2Points);
                double edgeValue2 = EdgeValue.GetEdgeValue(n2Points[i].Radius, n2Points[i].Angle);
                double difference = GetSolutionByTrajectories(n2Trajectories);
                n2Differences.Add(Math.Abs(difference - edgeValue2));
            }
            double difference2 = GetAverageDifference(n2Differences);

            Point[] n3Points = GenerateRandomData.GenerateRandomPoints(N3);
            List<double> n3Differences = new List<double>();
            for (int i = 0; i < n3Points.Length; i++)
            {
                List<List<Point>> n3Trajectories = GenerateRandomData.GenerateRandomTrajectoriesByStartPoint(Limit, n3Points[i], n3Points);
                double edgeValue3 = EdgeValue.GetEdgeValue(n3Points[i].Radius, n3Points[i].Angle);
                double difference = GetSolutionByTrajectories(n3Trajectories);
                n3Differences.Add(Math.Abs(difference - edgeValue3));
            }
            double difference3 = GetAverageDifference(n3Differences);

            Point[] n4Points = GenerateRandomData.GenerateRandomPoints(N4);
            List<double> n4Differences = new List<double>();
            for (int i = 0; i < n4Points.Length; i++)
            {
                List<List<Point>> n4Trajectories = GenerateRandomData.GenerateRandomTrajectoriesByStartPoint(Limit, n4Points[i], n4Points);
                double edgeValue4 = EdgeValue.GetEdgeValue(n4Points[i].Radius, n4Points[i].Angle);
                double difference = GetSolutionByTrajectories(n4Trajectories);
                n4Differences.Add(Math.Abs(difference - edgeValue4));
            }
            double difference4 = GetAverageDifference(n4Differences);
            MessageBox.Show(difference1.ToString());
            ((ColumnSeries)dataChart.Series[0]).ItemsSource = new KeyValuePair<int, double>[]
                {
                    new KeyValuePair <int, double> (N1, difference1),
                    new KeyValuePair <int, double> (N2, difference2),
                    new KeyValuePair <int, double> (N3, difference3),
                    new KeyValuePair <int, double> (N4, difference4)
                };
        }
        
        private double GetSolutionByTrajectories(List<List<Point>> trajectories)
        {
            double solution = 0;
            foreach (var trajectory in trajectories)
            {
                Point lastPoint = trajectory.LastOrDefault();
                solution += EdgeValue.GetEdgeValue(lastPoint.Radius, lastPoint.Angle);
            }

            return solution / trajectories.Count;
        }

        private double GetAverageDifference(List<double> differences)
        {
            double sum = 0;
            foreach(double dif in differences)
            {
                sum += dif * dif;
            }

            return Math.Sqrt(sum) / differences.Count;
        }
    }
}
