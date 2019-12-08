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

namespace mumu_navigateanyone
{
    /// <summary>
    /// Interaction logic for Page1.xaml
    /// </summary>
    public partial class Page1 : Page
    {
        public Page1()
        {
            InitializeComponent();
        }

        private void Hyperlink_Click(object sender, RoutedEventArgs e)
        {
            Hyperlink link = sender as Hyperlink;
            if (link == link1)
                NavigationService.Navigate(new Uri("pack://application:,,,/Page2.xaml"));
            else if (link == link2)
                NavigationService.Navigate(new Page2("使用NavigationService导航到Page2.xaml"));
            else if (link == link3)
                NavigationService.Navigate(new Uri("pack://application:,,,/LooseXaml.xaml"));
            else if (link == link4)
                NavigationService.Navigate(new Person("木木", "男", 20));
            else if (link == link5)
                NavigationService.Navigate(new Uri("http://www.cnblogs.com/helloj2ee/"));
            
        }

    }
}
