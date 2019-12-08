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

namespace mumu_dataretainandtransfer
{
    /// <summary>
    /// Interaction logic for GetPasswordPage.xaml
    /// </summary>
    public partial class GetPasswordPage : Page
    {
        public GetPasswordPage()
        {
            InitializeComponent();
        }


        private void cmdAdd_Click(object sender, RoutedEventArgs e)
        {
            string itemText = lstSource.SelectedItem.ToString();
            lstTarget.Items.Add(itemText);
            lstSource.Items.Remove(itemText);

        }
        private void cmdRemove_Click(object sender, RoutedEventArgs e)
        {
            string itemText = lstTarget.SelectedItem.ToString();
            lstSource.Items.Add(itemText);
            lstTarget.Items.Remove(itemText);
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            lstSource.Items.Add("红色");
            lstSource.Items.Add("蓝色");
            lstSource.Items.Add("绿色");
            lstSource.Items.Add("黄色");
            lstSource.Items.Add("橙色");
            lstSource.Items.Add("黑色");
            lstSource.Items.Add("粉红色");
            lstSource.Items.Add("紫色");
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            List<User> users = ((App)(App.Current)).users;
            User user = null;
            for (int i = 0; i < users.Count; i++)
            {
                if (name.Text == users[i].Name)
                {
                    int itemcount = lstTarget.Items.Count;
                    if (itemcount != users[i].FavColors.Count)
                    {
                        NavigationService.Navigate(new Uri("pack://application:,,,/ErrorPage.xaml"));
                        return;
                    }
                    for (int j = 0; j < itemcount; j++)
                    {
                        string s = lstTarget.Items[j].ToString();
                        bool res = users[i].FavColors.Contains(s);
                        if (!res)
                        {
                            NavigationService.Navigate(new Uri("pack://application:,,,/ErrorPage.xaml"));
                            return;
                        }

                    }
                    user = users[i];

                    NavigationService.Navigate(new WelcomePage(user,true));
                    return;
                }
            }
            NavigationService.Navigate(new Uri("pack://application:,,,/ErrorPage.xaml"));
            
        }

    }
}
