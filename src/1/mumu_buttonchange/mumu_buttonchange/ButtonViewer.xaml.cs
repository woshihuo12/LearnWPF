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

using System.Windows.Media.Animation;
using System.IO;
using System.Reflection;
using System.Windows.Markup;

namespace mumu_buttonchange
{
    /// <summary>
    /// Interaction logic for ButtonViewer.xaml
    /// </summary>
    public partial class ButtonViewer : Page
    {
        public ButtonViewer()
        {
            InitializeComponent();
            
            // 初始化动画
            Binding widthBinding = new Binding("ActualWidth");
            widthBinding.Source = this;

            // 控制右侧列表的动画
            // 透明度动画
            sampleGridOpacityAnimation = new DoubleAnimation();
            sampleGridOpacityAnimation.To = 0;
            sampleGridOpacityAnimation.Duration = new Duration(TimeSpan.FromSeconds(1));

            // 平移动画
            sampleGridTranslateTransformAnimation = new DoubleAnimation();
            sampleGridTranslateTransformAnimation.BeginTime = TimeSpan.FromSeconds(1);
            BindingOperations.SetBinding(sampleGridTranslateTransformAnimation, DoubleAnimation.ToProperty, widthBinding);
            sampleGridTranslateTransformAnimation.Duration = new Duration(TimeSpan.FromSeconds(1));

            // 控制内容面板的动画
            borderTranslateDoubleAnimation = new DoubleAnimation();
            borderTranslateDoubleAnimation.To = 0;
            borderTranslateDoubleAnimation.Duration = new Duration(TimeSpan.FromSeconds(1));
            borderTranslateDoubleAnimation.BeginTime = TimeSpan.FromSeconds(2);
            borderTranslateDoubleAnimation.FillBehavior = FillBehavior.HoldEnd;
            BindingOperations.SetBinding(borderTranslateDoubleAnimation, DoubleAnimation.FromProperty, widthBinding);     
        }

        private DoubleAnimation sampleGridOpacityAnimation;
        private DoubleAnimation sampleGridTranslateTransformAnimation;
        private DoubleAnimation borderTranslateDoubleAnimation;


        private static Uri _packUri = new Uri("pack://application:,,,/");

        private void selectedSampleChanged(object sender, RoutedEventArgs args)
        {
            if (args.Source is RadioButton)
            {
                RadioButton theButton = (RadioButton)args.Source;
                Frame theFrame = (Frame)theButton.Content;
                
                if (theFrame.HasContent)
                {
                    Uri source = theFrame.CurrentSource;
                    if ((source != null) && !source.IsAbsoluteUri)
                    {
                        source = new Uri(_packUri, source);
                    }
                    // SampleDisplayFrame.Source = source;
                    SampleDisplayFrame.Navigate(source);
                    SampleDisplayBorder.Visibility = Visibility.Visible;
                    
                    Page page = theFrame.Content as Page;
                    if (page != null)
                    {
                        contentitem.Header = page.Title;
                    }
                    string fileName = GetAbsoluteFileName(source);
                    LoadXAML(fileName);                    
                }
            }
        }

        private void LoadXAML(string fileName)
        {
            xamlText.Document.Blocks.Clear();
            Paragraph p = new Paragraph();
            CodeBlockPresenter presenter = new CodeBlockPresenter(CodeLanguage.XAML);
            string text = File.ReadAllText(fileName);
            presenter.FillInlines(text, p.Inlines);
            int index = 0, length = 0, currentIndex = 0;
            do
            {
                currentIndex = text.IndexOf("\r", index);
                length = Math.Max(length, currentIndex - index);
                index = currentIndex + 1;
            }
            while (currentIndex > -1);
            xamlText.Document.PageWidth = length * 8;
            xamlText.Document.Blocks.Add(p);
        }

        public static string GetAbsoluteFileName(Uri source)
        {
            string str = source.OriginalString;
            int index = str.LastIndexOf('/');
            string filename = str.Substring(index + 1);
            string appPath = Assembly.GetExecutingAssembly().Location;
            index = appPath.LastIndexOf("\\");
            int length = appPath.Length;
            appPath = appPath.Substring(0,index+1);
            return appPath + filename;

        }
        private void sampleDisplayFrameLoaded(object sender, EventArgs args)
        {
            SampleGrid.BeginAnimation(Grid.OpacityProperty, sampleGridOpacityAnimation);
            SampleGridTranslateTransform.BeginAnimation(TranslateTransform.XProperty, sampleGridTranslateTransformAnimation);
            SampleDisplayBorderTranslateTransform.BeginAnimation(TranslateTransform.XProperty, borderTranslateDoubleAnimation);
            SampleDisplayBorder.Visibility = Visibility.Visible;
        }


        private void galleryLoaded(object sender, RoutedEventArgs args)
        {
            SampleDisplayBorderTranslateTransform.X = this.ActualWidth;
            SampleDisplayBorder.Visibility = Visibility.Hidden;
        }


        private void pageSizeChanged(object sender, SizeChangedEventArgs args)
        {

            SampleDisplayBorderTranslateTransform.X = this.ActualWidth;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            tabcontrol.SelectedIndex = 0;
        }        
    }
}
