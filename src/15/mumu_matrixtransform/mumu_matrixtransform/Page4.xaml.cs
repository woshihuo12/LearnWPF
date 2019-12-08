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

namespace mumu_matrixtransform
{
    /// <summary>
    /// Interaction logic for Page4.xaml
    /// </summary>
    public partial class Page4 : Page
    {
        public Page4()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Matrix translate = new Matrix(1, 0, 0, 1, -100, -100);
            Matrix rotate = new Matrix(Math.Cos(Math.PI / 4), Math.Sin(Math.PI / 4), -Math.Sin(Math.PI / 4), Math.Cos(Math.PI / 4), 0, 0);
            Matrix translate2 = new Matrix(1, 0, 0, 1, 100, 100);
            Matrix m = new Matrix();
            m = translate * rotate * translate2;
            matrix.Matrix = m;
        }
    }
}
