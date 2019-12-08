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

namespace mumu_documentreader
{
    /// <summary>
    /// Interaction logic for BookOrCommandItem.xaml
    /// </summary>
    /// 
    public enum BookOrCommandItemType
    {
        Book,
        Command
    };
    public partial class BookOrCommandItem : StackPanel
    {
        BookOrCommandItemType _type;
        public BookOrCommandItem(BookOrCommandItemType type )
        {
             InitializeComponent();
             _type = type;
        }

        private void StackPanel_Loaded(object sender, RoutedEventArgs e)
        {
            if (_type == BookOrCommandItemType.Book)
            {
                SolidColorBrush brush = new SolidColorBrush(Colors.Yellow);
                MarkPath.Fill = brush;
            }
            else
            {
                SolidColorBrush brush = new SolidColorBrush(Colors.Green);
                MarkPath.Fill = brush;
            }
        }
    }
}
