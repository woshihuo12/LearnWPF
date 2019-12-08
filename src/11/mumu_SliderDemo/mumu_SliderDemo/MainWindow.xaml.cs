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

namespace mumu_SliderDemo
{
    /// <summary>
    /// Window1.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        private Color color;
        public MainWindow()
        {
            InitializeComponent();
            
        }

        private void BSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            Update();
        }

        private void GSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            Update();
        }

        private void RSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            Update();
        }

        private void Update()
        {
            color = Color.FromRgb(Convert.ToByte(RSlider.Value), Convert.ToByte(GSlider.Value), Convert.ToByte(BSlider.Value));
            rec.Fill = new SolidColorBrush(color);
        }
    }
}
