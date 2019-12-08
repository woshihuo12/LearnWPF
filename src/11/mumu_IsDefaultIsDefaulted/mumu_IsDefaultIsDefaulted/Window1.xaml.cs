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

namespace mumu_IsDefaultIsDefaulted
{
    /// <summary>
    /// Window1.xaml 的交互逻辑
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
        }

        private void checkBt_Checked(object sender, RoutedEventArgs e)
        {
            if (checkBt.IsChecked == true)
                textbox.AcceptsReturn = true;
            else
                textbox.AcceptsReturn = false;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            UpdateTextBlock();
        }

        private void UpdateTextBlock()
        {
            textblock.Text = "";
            string str = "";
            if (btnDefault.IsDefault)
                str = "Button.IsDefault = true;";
            if (btnDefault.IsDefaulted)
            {
                if (str.Length > 0)
                    str += "\n";
                str += "Button.IsDefaulted = true";
            }

            textblock.Text = str;
        }


    }
}
