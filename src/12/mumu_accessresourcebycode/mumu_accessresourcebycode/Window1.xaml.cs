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

namespace mumu_accessresourcebycode
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();

           
        }

        private void window1_Loaded(object sender, RoutedEventArgs e)
        {
            
            // 添加新的画刷资源
            window1.Resources.Add("yellowbrush", new SolidColorBrush(Colors.Yellow));

            button1.Background = (Brush)window1.Resources["yellowbrush"];
            
        }
    }
}
