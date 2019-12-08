using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Navigation;

namespace mumu_dataretainandtransfer
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private List<User> _users;
        public List<User> users
        {
            get
            {
                if (_users == null)
                {
                    _users = new List<User>();
                    User user = new User("木木", "mumu");
                    user.FavColors.Add("红色");
                    user.FavColors.Add("绿色");
                    _users.Add(user);
                    return _users;
                }
                return _users;
            }
        }
    }
}
