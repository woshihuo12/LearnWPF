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
    /// Interaction logic for Page2.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        public static readonly DependencyProperty FocusElementProperty;
        public string FocusElement
        {
            get
            {
                return (string)base.GetValue(LoginPage.FocusElementProperty);
            }
            set
            {
                base.SetValue(LoginPage.FocusElementProperty, value);
            }
        }

        static LoginPage()
        {
            
            LoginPage.FocusElementProperty = DependencyProperty.Register("FocusElement", typeof(string), typeof(LoginPage), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.Journal));
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            if (this.FocusElement != null)
            {
                IInputElement element = (IInputElement)LogicalTreeHelper.FindLogicalNode(this, this.FocusElement);
                Keyboard.Focus(element);
            }
        }

        private void Page_PreviewLostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            if (e.NewFocus == this.name || e.NewFocus == this.password)
            {
                this.FocusElement = (string)(((DependencyObject)e.NewFocus).GetValue(FrameworkElement.NameProperty));
            }
        }

        public LoginPage()
        {
            InitializeComponent();
            
            
        }

        private void Hyperlink_Click(object sender, RoutedEventArgs e)
        {
            RegisterPage CalledPageFunction = new RegisterPage(true);
            CalledPageFunction.Return += pageFunction_Return;
            this.NavigationService.Navigate(CalledPageFunction);
        }
        void pageFunction_Return(object sender, ReturnEventArgs<User> e)
        {

            if(e == null)
            {
               
                return;
            }
            User user = (User)e.Result;
            // If page function returned, display result and data
            if (user != null)
            {
                this.name.Text = user.Name;
                this.password.Password = user.Password;
            }
            List<User> users = ((App)(App.Current)).users;
            users.Add(user);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            List<User> users = ((App)(App.Current)).users;
            int usercount = users.Count;
            User user = new User(name.Text, password.ToString());
            for (int i = 0; i < usercount; i++)
            {
                if (name.Text == users[i].Name && password.Password == users[i].Password)
                {
                    WelcomePage page = new WelcomePage(user,false);
                    NavigationService.Navigate(page);
                    return;
                }
            }


            
            NavigationService.Navigate(new Uri("pack://application:,,,/ErrorPage.xaml"));
        }

       

        
    }
}
