using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Animations.Views
{
    /// <summary>
    /// Interaction logic for AppStartWindow.xaml
    /// </summary>
    public partial class AppStartWindow : Window
    {
        public AppStartWindow()
        {
            InitializeComponent();
        }

        private void SingleCalculation_Click(object sender, RoutedEventArgs e)
        {
            SingleCalculationDataWindow singleCalculationDataWindow = new SingleCalculationDataWindow();
            singleCalculationDataWindow.Show();
        }

        private void MultipleCalculation_Click(object sender, RoutedEventArgs e)
        {
            MultipleCalculationDataWindow multipleCalculationDataWindow = new MultipleCalculationDataWindow();
            multipleCalculationDataWindow.Show();
        }

        private void About_Click(object sender, RoutedEventArgs e)
        {

        }

        private void English_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Armenian_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
