using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;

namespace mumu_navigationevents
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_Navigating(object sender, System.Windows.Navigation.NavigatingCancelEventArgs e)
        {
            System.Console.WriteLine("------------------------------------------");
            System.Console.Write("触发的事件为：Application_Navigating\n");
            System.Console.WriteLine("导航页面的Uri:" + e.Uri.ToString());
        }

        private void Application_NavigationFailed(object sender, System.Windows.Navigation.NavigationFailedEventArgs e)
        {
            System.Console.WriteLine("------------------------------------------");
            System.Console.Write("触发的事件为：Application_NavigationFailed\n");
            System.Console.WriteLine("失败的异常是: " +e.Exception.ToString());
            // Handled属性设置为true，从而防止异常继续上传转变为一个未处理的应用程序异常
            e.Handled = true;
        }

        private void Application_Navigated(object sender, System.Windows.Navigation.NavigationEventArgs e)
        {
            System.Console.WriteLine("------------------------------------------");
            System.Console.Write("触发的事件为：Application_Navigated\n");
            System.Console.WriteLine("导航页面的Uri:"+e.Uri.ToString());
        }

        private void Application_NavigationProgress(object sender, System.Windows.Navigation.NavigationProgressEventArgs e)
        {
            System.Console.WriteLine("------------------------------------------");
            System.Console.Write("触发的事件为：Application_NavigationProgress\n");
            System.Console.WriteLine("导航页面的Uri:"+e.Uri.ToString());
            System.Console.WriteLine("已经得到的字节数为{0}", e.BytesRead);
            
        }

        private void Application_NavigationStopped(object sender, System.Windows.Navigation.NavigationEventArgs e)
        {
            System.Console.WriteLine("------------------------------------------");
            System.Console.Write("触发的事件为：Application_NavigationStopped\n");
            System.Console.WriteLine("导航页面的Uri:"+e.Uri.ToString());
            
        }

        private void Application_LoadCompleted(object sender, System.Windows.Navigation.NavigationEventArgs e)
        {
            System.Console.WriteLine("------------------------------------------");
            System.Console.Write("触发的事件为：Application_LoadCompleted\n");
            System.Console.WriteLine("导航页面的Uri:"+e.Uri.ToString());
        }

        private void Application_FragmentNavigation(object sender, System.Windows.Navigation.FragmentNavigationEventArgs e)
        {
            System.Console.WriteLine("------------------------------------------");
            System.Console.Write("触发的事件为：Application_FragmentNavigation\n");
            System.Console.WriteLine("导航的段落为:"+e.Fragment);
            
        }

    }
}
