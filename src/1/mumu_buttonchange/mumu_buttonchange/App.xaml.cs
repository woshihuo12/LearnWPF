using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;

namespace mumu_buttonchange
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            Window myWindow = new Window();
            myWindow.Title = "看我七十二变";
            myWindow.Content = new ButtonViewer();
            myWindow.Show();
        }
    }
}
