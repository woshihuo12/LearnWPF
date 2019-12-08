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

namespace mumu_drawingbrushdemo
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

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // media.LoadedBehavior = MediaState.Play;
            // media.Play();

            MediaPlayer player = new MediaPlayer();

            player.Open(new Uri(@"xbox.wmv", UriKind.Relative));
            aVideoDrawing.Rect = new Rect(0, 0, btn.ActualWidth, btn.ActualHeight * 0.8);
            aVideoDrawing.Player = player;

            // Play the video once.
            player.Play();


            img.Rect = new Rect(0, btn.ActualHeight * 0.8, btn.ActualWidth, btn.ActualHeight * 0.2);



        }
    }
}
