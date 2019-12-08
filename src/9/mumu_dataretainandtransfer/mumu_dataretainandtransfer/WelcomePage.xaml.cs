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
    public partial class WelcomePage : Page
    {
        
        public WelcomePage()
        {
            InitializeComponent();
        }
        public WelcomePage(User user,bool isPasswordVisible) :this()
        {
            welcome.Text = "欢迎你" + user.Name;
            if (isPasswordVisible)
                welcome.Text += "\n你的密码为：" + user.Password; 
        }
    }
}
