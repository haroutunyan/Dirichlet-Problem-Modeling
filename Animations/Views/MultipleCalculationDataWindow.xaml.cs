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
    /// Interaction logic for MultipleCalculationDataWindow.xaml
    /// </summary>
    public partial class MultipleCalculationDataWindow : Window
    {
        public MultipleCalculationDataWindow()
        {
            InitializeComponent();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            multipleCalculationDataWindow.Close();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(n1Box.Text, out int n1) && int.TryParse(n2Box.Text, out int n2) &&
                int.TryParse(n3Box.Text, out int n3) && int.TryParse(n4Box.Text, out int n4))
            {
                MultipleCalculationWindow multipleCalculationWindow = new MultipleCalculationWindow(n1, n2, n3, n4);
                multipleCalculationWindow.Show();
                multipleCalculationDataWindow.Close();
            }
            else
            {
                MessageBox.Show("Invalid Credentials");
            }
        }
    }
}
