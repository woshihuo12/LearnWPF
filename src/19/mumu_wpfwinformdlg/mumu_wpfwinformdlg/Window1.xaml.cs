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

using System.Windows.Forms.Integration;

namespace mumu_wpfwinformdlg
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

        private void modelbtn_Click(object sender, RoutedEventArgs e)
        {
            Form1 frm = new Form1();
            frm.Text = "模态对话框";
            if (frm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                MessageBox.Show("模态对话框点击确定按钮后关闭");
            }
        }

        private void modelessbtn_Click(object sender, RoutedEventArgs e)
        {
            WindowsFormsHost.EnableWindowsFormsInterop();

            Form1 frm = new Form1();
            frm.Text = "非模态对话框";
            frm.Show();
        }

    }
}
