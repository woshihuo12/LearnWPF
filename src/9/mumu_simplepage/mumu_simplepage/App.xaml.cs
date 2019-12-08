using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Navigation;
namespace mumu_simplepage
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            // NavigationWindow win = new NavigationWindow();
            Window win = new Window();
            // win.ShowsNavigationUI = false;

            // 1 没有配置的情况
             win.Content = new Page1();
            // 2 宿主窗口大于Page尺寸
            // win.Content = new Page1(300,300,500,500);
            // 2 宿主窗口小于Page尺寸
            // win.Content = new Page1(500,500,450, 450);
            win.Show();
        }
    }
}
