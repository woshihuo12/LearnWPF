using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;
using System.Windows.Media;
using System.Windows.Markup;
using System.Xml;
using System.IO;
namespace mumu_appbyxamlandcod1
{
    public class WindowByXAMLAndCode : Window
    {
        public WindowByXAMLAndCode()
        {
            InitializeComponent();
        }
        private Ellipse elip;
        public void InitializeComponent()
        {
            this.Width = 600;
            this.Height = 200;
            this.Title = "AppByXAMLAndCode1";

            string strXaml = "<StackPanel  xmlns='http://schemas.microsoft.com/winfx/2006/xaml/presentation' Width='500'>" +
                                "<Button Name='btn' Content='MyButton'/>" +
                                "<Ellipse Name='elip' Stroke='Red' Height='60' StrokeThickness='3' />"
                             + "</StackPanel>";
            //StringReader strReader = new StringReader(strXaml);
            //XmlTextReader xmlreader = new XmlTextReader(strReader);
            //StackPanel obj = (StackPanel)XamlReader.Load(xmlreader);

            Uri uri = new Uri("pack://application:,,,/mumu_stackpanel.xaml");
            Stream stream = Application.GetResourceStream(uri).Stream;
            StackPanel obj = (StackPanel)XamlReader.Load(stream);
            Content = obj;
            elip = obj.FindName("elip") as Ellipse;
            Button btn = obj.FindName("btn") as Button;
            //btn.Click += new RoutedEventHandler(btn_Click);
            btn.AddHandler(Button.ClickEvent, new RoutedEventHandler(btn_Click));
        }

        void btn_Click(object sender, RoutedEventArgs e)
        {
            if (elip != null)
                elip.Stroke = new SolidColorBrush(Colors.Blue);
        }


        [STAThread]
        public static void Main()
        {
            Application app = new Application();
            app.Run(new WindowByXAMLAndCode());
        }
    }
}
