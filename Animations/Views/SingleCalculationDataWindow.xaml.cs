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
    /// Interaction logic for SingleCalculationDataWindow.xaml
    /// </summary>
    public partial class SingleCalculationDataWindow : Window
    {
        public SingleCalculationDataWindow()
        {
            InitializeComponent();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(countBox.Text, out int count) && double.TryParse(limitBox.Text, out double limit))
            {
                SingleCalculationWindow singleCalculationWindow = new SingleCalculationWindow(count, limit);
                singleCalculationWindow.Show();
                singleCalculationDataWindow.Close();
            }
            else
            {
                MessageBox.Show("Invalid Credetials");
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            singleCalculationDataWindow.Close();
        }
    }
}
