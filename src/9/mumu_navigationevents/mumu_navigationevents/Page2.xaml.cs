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

namespace mumu_navigationevents
{
    /// <summary>
    /// Interaction logic for Page2.xaml
    /// </summary>
    public partial class Page2 : Page
    {
        public Page2()
        {
            System.Console.WriteLine("------------------------------------------");
            System.Console.WriteLine("Page2 被创建出来");
            InitializeComponent();
        }

        private void Hyperlink_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("pack://application:,,,/Page2.xaml#first"));
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            System.Console.WriteLine("------------------------------------------");
            System.Console.WriteLine("Page2 Loaded");
        }

        private void Page_Unloaded(object sender, RoutedEventArgs e)
        {
            System.Console.WriteLine("------------------------------------------");
            System.Console.WriteLine("Page2 UnLoaded");
        }
    }
}
