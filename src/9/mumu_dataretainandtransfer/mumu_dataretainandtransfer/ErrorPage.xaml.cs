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

namespace mumu_dataretainandtransfer
{
    /// <summary>
    /// Interaction logic for ErrorPage.xaml
    /// </summary>
    public partial class ErrorPage : Page
    {
        public ErrorPage()
        {
            InitializeComponent();
        }

        public ErrorPage(string reason) :this()
        {
            this.reason.Visibility = Visibility.Visible;
            this.reason.Text = reason;
        }

        private void Hyperlink_Click(object sender, RoutedEventArgs e)
        {        
            this.NavigationService.GoBack();
            this.NavigationService.RemoveBackEntry();
        }
    }
}
