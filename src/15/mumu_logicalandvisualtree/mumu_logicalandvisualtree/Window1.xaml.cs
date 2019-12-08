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

namespace mumu_logicalandvisualtree
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
            Console.WriteLine("逻辑树：");
            PrintLogicalTree(0, this);
            Console.WriteLine("---------------------------------------------------------");
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("可视化树：");
            PrintVisualTree(0, this);
            Console.WriteLine("---------------------------------------------------------");
        }
        void PrintLogicalTree(int depth, object obj)
        {            
            Console.WriteLine(new string(' ', depth) + obj.GetType().Name);
            
            if (!(obj is DependencyObject)) return;
            
            foreach (object child in LogicalTreeHelper.GetChildren(
            obj as DependencyObject))
                PrintLogicalTree(depth + 1, child);
        }

        void PrintVisualTree(int depth, DependencyObject obj)
        {

            Console.WriteLine(new string(' ', depth) + obj.GetType().Name);
            
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(obj); i++)
                PrintVisualTree(depth + 1, VisualTreeHelper.GetChild(obj, i));
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Console.Clear();
        }
  
    }
}
