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

using Microsoft.Win32;
using System.Reflection;
using System.Windows.Markup;
namespace mumu_dumpCLRNamespace
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string strFilter =
                        "动态链接库(*.dll)|*.dll";
            string strInitialDirectory="C:\\Program Files\\Reference Assemblies\\Microsoft\\Framework\\.NETFramework\\v4.0\\Profile\\Client";
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = strFilter;
            dlg.InitialDirectory = strInitialDirectory;

            if ((bool)dlg.ShowDialog(this))
            {
                ShowCLRNamespace(dlg.FileName);
            }
        }

        private void ShowCLRNamespace(string dllFileName)
        {
            //listbox.Items.Clear();
            string xmlnamespace = @"http://schemas.microsoft.com/winfx/2006/xaml/presentation";
            string xmlnamespace2 = @"http://schemas.microsoft.com/winfx/2006/xaml";
            Assembly assembly = Assembly.LoadFile(dllFileName);
            foreach(XmlnsDefinitionAttribute xmlattribute in assembly.GetCustomAttributes(typeof(XmlnsDefinitionAttribute), true))
            {
                if (xmlattribute.XmlNamespace == xmlnamespace)
                    listbox.Items.Add(xmlattribute.ClrNamespace);
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            listbox.Items.Clear();
        }
    }
}
