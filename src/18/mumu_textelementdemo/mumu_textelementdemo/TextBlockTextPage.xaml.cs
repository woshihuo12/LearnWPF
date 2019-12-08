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

namespace mumu_textelementdemo
{
    /// <summary>
    /// Interaction logic for TextBlockTextPage.xaml
    /// </summary>
    public partial class TextBlockTextPage : Page
    {
        public TextBlockTextPage()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Clicked");
        }
    }
}
