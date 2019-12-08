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

namespace mumu_testcustomcontrol
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
            this.simpleBtn.ClassHandlerProcessed += new EventHandler(simpleBtn_RaisedClass);
        }
        


        protected int eventCounter = 0;

        private void InsertList(object sender, RoutedEventArgs e)
        {
            eventCounter++;
            string message = "#" + eventCounter.ToString() + ":\r\n"+ 
                "InsertList\r\n"+
                " Sender: " + sender.ToString() + "\r\n" +
                " Source: " + e.Source + "\r\n" +
                " Original Source: " + e.OriginalSource;
            lstMessages.Items.Add(message);
            e.Handled = (bool)chkHandle.IsChecked;
        }

        private void cmdClear_Click(object sender, RoutedEventArgs e)
        {
            eventCounter = 0;
            lstMessages.Items.Clear();
        }

        private  void simpleBtn_RaisedClass(object sender,EventArgs e)
        {
            eventCounter++;
            string message = "#" + eventCounter.ToString() + ":\r\n" +
            "Windows Class Handler\r\n" + "Sender:" + sender.ToString();
            lstMessages.Items.Add(message);          
            
        }

        private void ProcessHandlersToo(Object sender, RoutedEventArgs e)
        {
            eventCounter++;
            string message = "#" + eventCounter.ToString() + ":\r\n" +
                "ProcessHandlersToo\r\n"+
                " Sender: " + sender.ToString() + "\r\n" +
                " Source: " + e.Source + "\r\n" +
                " Original Source: " + e.OriginalSource;
            lstMessages.Items.Add(message);
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            window1.AddHandler(MySimpleButton.CustomClickEvent, new RoutedEventHandler(ProcessHandlersToo), true);

        }
    }
}
