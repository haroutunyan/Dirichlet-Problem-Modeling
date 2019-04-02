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

        public MultipleCalculationWindow(int n1, int n2, int n3, int n4)
        {
            N1 = n1;
            N2 = n2;
            N3 = n3;
            N4 = n4;

            InitializeComponent();
            LoadColumnChartData();
        }

        private void LoadColumnChartData()
        {
            Point[] n1Points = GenerateRandomData.GenerateRandomPoints(N1);
            List<List<Point>> n1Trajectories = GenerateRandomData.GenerateRandomTrajectories(0.1, n1Points);
            Point n1FirstPoint = n1Trajectories.FirstOrDefault().FirstOrDefault();
            double edgeValue1 = EdgeValue.GetEdgeValue(n1FirstPoint.Radius, n1FirstPoint.Angle);
            double difference1 = GetDifferenceBetweenEdgeValueAndSolution(n1Trajectories, edgeValue1);

            Point[] n2Points = GenerateRandomData.GenerateRandomPoints(N2);
            List<List<Point>> n2Trajectories = GenerateRandomData.GenerateRandomTrajectories(0.1, n2Points);
            Point n2FirstPoint = n2Trajectories.FirstOrDefault().FirstOrDefault();
            double edgeValue2 = EdgeValue.GetEdgeValue(n2FirstPoint.Radius, n2FirstPoint.Angle);
            double difference2 = GetDifferenceBetweenEdgeValueAndSolution(n2Trajectories, edgeValue2);

            Point[] n3Points = GenerateRandomData.GenerateRandomPoints(N3);
            List<List<Point>> n3Trajectories = GenerateRandomData.GenerateRandomTrajectories(0.1, n3Points);
            Point n3FirstPoint = n3Trajectories.FirstOrDefault().FirstOrDefault();
            double edgeValue3 = EdgeValue.GetEdgeValue(n3FirstPoint.Radius, n3FirstPoint.Angle);
            double difference3 = GetDifferenceBetweenEdgeValueAndSolution(n3Trajectories, edgeValue3);

            Point[] n4Points = GenerateRandomData.GenerateRandomPoints(N4);
            List<List<Point>> n4Trajectories = GenerateRandomData.GenerateRandomTrajectories(0.1, n4Points);
            Point n4FirstPoint = n4Trajectories.FirstOrDefault().FirstOrDefault();
            double edgeValue4 = EdgeValue.GetEdgeValue(n4FirstPoint.Radius, n4FirstPoint.Angle);
            double difference4 = GetDifferenceBetweenEdgeValueAndSolution(n4Trajectories, edgeValue4);

            ((ColumnSeries)dataChart.Series[0]).ItemsSource = new KeyValuePair<int, double>[]
                {
                    new KeyValuePair <int, double> (N1, difference1),
                    new KeyValuePair <int, double> (N2, difference2),
                    new KeyValuePair <int, double> (N3, difference3),
                    new KeyValuePair <int, double> (N4, difference4)
                };
        } 
        
        private double GetDifferenceBetweenEdgeValueAndSolution(List<List<Point>> trajectories, double edgeValue)
        {
            double solution = 0;
            foreach (var trajectory in trajectories)
            {
                Point lastPoint = trajectory.LastOrDefault();
                solution += EdgeValue.GetEdgeValue(lastPoint.Radius, lastPoint.Angle);
            }

            return Math.Abs(solution / trajectories.Count - edgeValue);
        }
    }
}
