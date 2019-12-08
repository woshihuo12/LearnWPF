using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;
using System.Windows.Media;

namespace mumu_appbyonlycode
{
    public class WindowByOnlyCode :Window
    {
        public WindowByOnlyCode()
        {
            InitializeComponent();
        }
        private Ellipse elip;
        public void InitializeComponent()
        {
            this.Width = 600;
            this.Height = 200;
            this.Title = "AppByOnlyCode";

            StackPanel panel = new StackPanel();
            panel.Width = 500;
            
            Button btn = new Button();
            btn.Content = "MyButton";
            panel.Children.Add(btn);
            

            elip = new Ellipse();
            elip.Stroke = new SolidColorBrush(Colors.Red);
            elip.Height = 60;
            elip.StrokeThickness = 3;
            panel.Children.Add(elip);


            
            this.Content = panel;
            btn.Click += new RoutedEventHandler(btn_Click);

        }

        void  btn_Click(object sender, RoutedEventArgs e)
        {
 	        elip.Stroke = new SolidColorBrush(Colors.Blue);
        }


        [STAThread]
        public static void Main()
        {

            Application app = new Application();
            app.Run(new WindowByOnlyCode());
        }
    }
}
