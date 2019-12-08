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
using System.Windows.Resources;

namespace mumu_resourcedemo
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // 1 引用资源文件 Sunset.jpg
            // img.Source = new BitmapImage(new Uri("/image/Sunset.jpg", UriKind.Relative));
            /*

            Uri uri = new Uri("/image/Sunset.jpg", UriKind.Relative);
            StreamResourceInfo info = Application.GetResourceStream(uri);
            BitmapImage bitmapimg = new BitmapImage();
            // 注意给BitmapImage赋值需要调用BeginInit和EndInit两个函数
            bitmapimg.BeginInit();
            bitmapimg.StreamSource = info.Stream;
            bitmapimg.EndInit();
            img.Source = bitmapimg;
            // this.pageframe.Content = page;
            */            

            // 2 引用内容文件 contentfile.xaml
            /*
            Uri uri = new Uri("/contentFile.xaml", UriKind.Relative);
            StreamResourceInfo info = Application.GetContentStream(uri);
            System.Windows.Markup.XamlReader reader = new System.Windows.Markup.XamlReader();
            Page page = (Page)reader.LoadAsync(info.Stream);
            this.contentfileframe.Content = page;
            
            this.contentfileframe.Source = new Uri("/contentFile.xaml", UriKind.Relative);
            */

            // 3 引用site of origin文件 

            Uri uri = new Uri("/siteoforiginfile.xaml", UriKind.Relative);
            StreamResourceInfo info = Application.GetRemoteStream(uri);
            System.Windows.Markup.XamlReader reader = new System.Windows.Markup.XamlReader();
            Page page = (Page)reader.LoadAsync(info.Stream);
            this.siteoforiginfileframe.Content = page;

        }
    }
}
