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
    /// Interaction logic for Page1.xaml
    /// </summary>
    public partial class RegisterPage : PageFunction<User>, IProvideCustomContentState
    {
        private User user;

        public RegisterPage() 
        {
            InitializeComponent();
            
            user = new User();
        }
        public RegisterPage(bool isload):this()
        {
            this.isload = isload;
        }

        bool isload = false;
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            if (isload)
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
        }


        private void cmdAdd_Click(object sender, RoutedEventArgs e)
        {

            if (lstSource.SelectedIndex != -1)
            {
                // Determine the best name to use in the navigation history.
                NavigationService nav = NavigationService.GetNavigationService(this);
                string itemText = lstSource.SelectedItem.ToString();
                string journalName = "Added " + itemText;

                // Update the journal (using the method shown below.)        
                 nav.AddBackEntry(GetJournalEntry(journalName));

                // Now perform the change.
                lstTarget.Items.Add(itemText);
                lstSource.Items.Remove(itemText);
            }


        }
        private void cmdRemove_Click(object sender, RoutedEventArgs e)
        {
            if (lstTarget.SelectedIndex != -1)
            {
                // Determine the best name to use in the navigation history.
                NavigationService nav = NavigationService.GetNavigationService(this);
                string itemText = lstTarget.SelectedItem.ToString();
                string journalName = "Removed " + itemText;

                // Update the journal (using the method shown below.)        
                 nav.AddBackEntry(GetJournalEntry(journalName));

                // Perform the change.
                lstSource.Items.Add(itemText);
                lstTarget.Items.Remove(itemText);
            }
        }

        private void cmdOK_Click(object sender, RoutedEventArgs e)
        {
            User user = CreateUser();
            if (user == null) return;
            else
                OnReturn(new ReturnEventArgs<User>(user));
            
        }

        private void cmdCancel_Click(object sender, RoutedEventArgs e)
        {
            OnReturn(null);
        }

        private User CreateUser()
        {
            string s = ValidateRegister();
            if (s != null)
            {
                ErrorPage page = new ErrorPage(s);
                NavigationService.Navigate(page);
                return null;
            }
            

            User user = new User(name.Text,pass.Password);
            int count = lstTarget.Items.Count;
            for (int i = 0; i < count; i++)
            {
                // user.AddFavColor(lstTarget.Items[i].ToString());
                user.FavColors.Add(lstTarget.Items[i].ToString());
            }

            return user;
        }

        private string ValidateRegister()
        {
            if (pass.Password != confirmpass.Password)
            {
               return "两次输入的密码不一致！";       
            }
            List<User> users = ((App)(App.Current)).users;
            for (int i = 0; i < users.Count; i++)
            {
                if(name.Text == users[i].Name)
                {
                    string s = "用户名";
                    s += name.Text + "已存在";
                    return s;
                }
            }
            
            return null;
        }

       

        private ListSelectionJournalEntry GetJournalEntry(string journalName)
        {
            // Get the state of both lists (using a helper method).
            List<String> source = GetListState(lstSource);
            List<String> target = GetListState(lstTarget);

            // Create the custom state object with this information.
            // Point the callback to the Replay method in this class.
            return new ListSelectionJournalEntry(
              source, target, journalName);
        }

        private List<String> GetListState(ListBox list)
        {
            List<string> items = new List<string>();
            foreach (string item in list.Items)
            {
                items.Add(item);
            }
            return items;
        }

        public string restoredStateName;

        public CustomContentState GetContentState()
        {
            string journalName;
            if (restoredStateName != "")
                journalName = restoredStateName;
            else
                journalName = "RegisterPage";

            return GetJournalEntry(journalName);
        }
    }
}
