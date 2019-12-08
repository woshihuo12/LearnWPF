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

using System.Threading;
namespace mumu_navigationevents
{
    /// <summary>
    /// Interaction logic for Page1.xaml
    /// </summary>
    public partial class Page1 : Page
    {
        public Page1()
        {
            System.Console.WriteLine("------------------------------------------");
            System.Console.WriteLine("Page1 被创建出来");
            InitializeComponent();
        }

        private void Hyperlink_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("pack://application:,,,/Page2.xaml"));
            Thread.Sleep(500);
            NavigationService.StopLoading();
        }

        private void Hyperlink_Click_1(object sender, RoutedEventArgs e)
        {
            NavigationService.Refresh();
            
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            System.Console.WriteLine("------------------------------------------");
            System.Console.WriteLine("Page1 Loaded");
            // NavigationService.Navigating += new NavigatingCancelEventHandler(NavigationService_Navigating);
        }

        private void Page_Unloaded(object sender, RoutedEventArgs e)
        {
            System.Console.WriteLine("------------------------------------------");
            System.Console.WriteLine("Page1 UnLoaded");
        }

        //void NavigationService_Navigating(object sender, NavigatingCancelEventArgs e)
        //{
        //    MessageBoxResult result;
        //    result = MessageBox.Show("是否要离开当前页面", "提示", MessageBoxButton.YesNo);

        //    // If the user doesn't want to navigate away, cancel the navigation
        //    if (result == MessageBoxResult.No) e.Cancel = true;

        //}
    }
}
