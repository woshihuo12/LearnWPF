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

namespace mumu_logicresourcedemo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            //ImageBrush brush = (ImageBrush)this.Resources["TileBrush"];
            //ImageBrush newbrush = brush.Clone();
            //newbrush.Viewport = new Rect(0, 0, 5, 5);
            //this.Resources["TileBrush"] = newbrush;

            
            ImageBrush brush = (ImageBrush)this.Resources["TileBrush"];
            brush.Viewport = new Rect(0, 0, 5, 5);
            
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            
            String str = (String)this.Resources["ButtonContent"];
            str = "木木的再一次尝试";
            
           
        }
    } // this.Resources["ButtonContent"] = "木木的再一次尝试";
}
