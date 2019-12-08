using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Navigation;
using System.IO;

namespace mumu_dataretainandtransfer
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public  List<User> users;

        private void Application_Startup(object sender, StartupEventArgs e)
        {
           
            users = new List<User>();
            User user = new User("木木", "mumu");
            user.FavColors.Add("红色");
            user.FavColors.Add("绿色");
            users.Add(user);
            NavigationWindow win = new NavigationWindow();
           
            win.Width = 480;
            win.Height = 400;

            win.Content = new LoginPage();
            win.Show();
        }

    }
}
