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

namespace mumu_ListBoxDemo
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
        private void Radio_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton currentRadioBtn = (RadioButton)e.OriginalSource;
            string selectMode = currentRadioBtn.Name;
            switch (selectMode)
            {
                case "radioSingle":
                    lb.SelectionMode = SelectionMode.Single;
                    break;
                case "radioMultiple":
                    lb.SelectionMode = SelectionMode.Multiple;
                    break;
                case "radioExtended":
                    lb.SelectionMode = SelectionMode.Extended;
                    break;
            }
        }

    }
}
