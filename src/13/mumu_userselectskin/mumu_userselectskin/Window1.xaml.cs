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
using System.Windows.Markup;
using System.IO;

namespace mumu_userselectskin
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

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            MenuItem menuItem = (MenuItem)sender;
            if(menuItem == null) return;
            
            if (menuItem == simpleSkin)
            {
                ResourceDictionary newDictionary = new ResourceDictionary();
                newDictionary.Source = new Uri("Resources/button.xaml", UriKind.Relative);
                Application.Current.Resources.MergedDictionaries[0] = newDictionary;
                menuItem.IsChecked = true;
                fancySkin.IsChecked = false;
            }
            else if (menuItem == fancySkin)
            {
                ResourceDictionary newDictionary = new ResourceDictionary();
                newDictionary.Source = new Uri("Resources/fancyButton.xaml", UriKind.Relative);
                Application.Current.Resources.MergedDictionaries[0] = newDictionary;
                menuItem.IsChecked = true;
                simpleSkin.IsChecked = false;
            }

        }
    }
}
