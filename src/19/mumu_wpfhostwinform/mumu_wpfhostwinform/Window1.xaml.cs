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

namespace mumu_wpfhostwinform
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

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxItem lbi = ((sender as ComboBox).SelectedItem as ComboBoxItem);
            UIElement ui = lbi.Tag as UIElement;
            ui.Focus();
            propertyGrid.SelectedObject = lbi.Tag;

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ComboBoxItem item1 = new ComboBoxItem();
            item1.Content = "Button";
            item1.Tag = btn;
            combobox.Items.Add(item1);
            
            ComboBoxItem item2 = new ComboBoxItem();
            item2.Content = "TextBox";
            item2.Tag = textbox;
            combobox.Items.Add(item2);

            ComboBoxItem item3 = new ComboBoxItem();
            item3.Content = "RadioButton";
            item3.Tag = radiobt;
            combobox.Items.Add(item3);

            ComboBoxItem item4 = new ComboBoxItem();
            item4.Content = "ComboBox";
            item4.Tag = combobox;
            combobox.Items.Add(item4);
        }
    }
}
