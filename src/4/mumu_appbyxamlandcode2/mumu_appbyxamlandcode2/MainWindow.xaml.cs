using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace mumu_appbyxamlandcode2
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btn_Click(object sender, RoutedEventArgs e)
        {
            if (elip != null)
                elip.Stroke = new SolidColorBrush(Colors.Blue);

            int[] array1 = new int[3]{1,2,3};
            int[] array2 = new int[2]{1,3};
            if (array1[array2[0]]> 5)
            {
                ;
            }
        }



 

    }
}
