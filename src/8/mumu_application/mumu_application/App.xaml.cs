using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;

namespace mumu_application
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            MainWindow win = new MainWindow();
            win.Show();
        }

        private void Application_Activated(object sender, EventArgs e)
        {
            MessageBox.Show("activated");
        }

        private void Application_Deactivated(object sender, EventArgs e)
        {
            MessageBox.Show("deactivated");
        }

        private void Application_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            e.Handled = true;
            if (e.Exception is DivideByZeroException)
            {
                MessageBox.Show("嗨,兄弟你遇到麻烦了！不过还好,程序仍可以恢复运行！");
            }
            else
            {
                MessageBox.Show("嗨,兄弟你遇到大麻烦了！程序马上退出。啊~~~");

                this.Shutdown(-1);
            }
        }
    }
}
