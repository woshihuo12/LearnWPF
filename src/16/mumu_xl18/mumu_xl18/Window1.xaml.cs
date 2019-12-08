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

namespace mumu_xl18
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
       
        public Window1()
        {
            InitializeComponent();
            imagelist = new List<Image>();
        }
        Storyboard story;
        private List<Image> imagelist;
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            story = new Storyboard();
            NameScope.SetNameScope(this, new NameScope());
            // 在Grid面板里放置Image控件，顺序应该由18到1，这样才能保证第一掌在最上方
            for (int i = 0; i < 18; i++)
            {              
                string name = "img" + (18-i);
                string strUri;
                strUri = string.Format("pack://application:,,,/img/{0}.jpg", (18-i));
                Image img = new Image();
                img.Name = name;
                BitmapImage myBitmapImage = new BitmapImage();
                myBitmapImage.BeginInit();
                myBitmapImage.UriSource = new Uri(strUri);
                myBitmapImage.EndInit();

                img.Source = myBitmapImage;
                this.RegisterName(name, img);

                grid.Children.Add(img);

                
                Grid.SetRow(img, 0);
                Grid.SetColumn(img, 0);

                imagelist.Add(img);
            }

            imagelist.Reverse();
            for (int i = 0; i < imagelist.Count-1; i++)
            {
                DoubleAnimation anim = new DoubleAnimation();
                anim.BeginTime = TimeSpan.FromSeconds(0.5 * i);
                anim.From = 1;
                anim.To = 0;
                anim.Duration = TimeSpan.FromSeconds(0.5);
                
                Storyboard.SetTargetName(anim, imagelist[i].Name);
                Storyboard.SetTargetProperty(anim, new PropertyPath(Image.OpacityProperty));

                story.Children.Add(anim);

                
            }

        }

        private void Button_Start(object sender, RoutedEventArgs e)
        {
           
            story.Begin(this,true);
        }

        private void Button_Pause(object sender, RoutedEventArgs e)
        {
           
            story.Pause(this);
        }

        private void Button_Resume(object sender, RoutedEventArgs e)
        {
            story.Resume(this);
        }

        private void Button_Stop(object sender, RoutedEventArgs e)
        {
            story.Stop(this);
        }
    }
}
