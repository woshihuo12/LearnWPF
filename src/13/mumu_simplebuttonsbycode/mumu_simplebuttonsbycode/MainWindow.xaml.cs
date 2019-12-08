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


namespace mumu_simplebuttonsbycode
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

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            int count = wrappanel.Children.Count;
            for (int i = 0; i < count; i++)
            {
                Button btn = wrappanel.Children[i] as Button;
                RotateTransform rotatetransform = new RotateTransform();
                rotatetransform.Angle = 45;
                btn.RenderTransform = rotatetransform;

                btn.Background = new SolidColorBrush(Colors.Beige);
                btn.Height = 50;
                btn.Margin = new Thickness(35, 0, 0, 0);
            }
        }
    }
}
