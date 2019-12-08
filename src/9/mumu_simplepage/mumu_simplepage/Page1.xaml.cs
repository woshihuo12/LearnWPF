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

namespace mumu_simplepage
{
    /// <summary>
    /// Interaction logic for Page1.xaml
    /// </summary>
    public partial class Page1 : Page
    {
        public Page1(double width,double height,double hostwinWidth,double hostwinHeight) :this()
        {
 
            this.Width = width;
            this.Height = height;
            this.WindowHeight = hostwinHeight;
            this.WindowWidth = hostwinWidth;
           
            this.WindowTitle = "对窗口尺寸进行了配置";
            this.text.Text = "Width = " + width + "\n\n" + "Height = " + height + "\n\n"+ 
                             "WindowWidth = " + hostwinWidth + "\n\nWindowHeight = " + hostwinHeight;
        }

        public Page1()
        {
            InitializeComponent();
            this.WindowTitle = "没有对窗口尺寸进行了配置";
        }
    }
}
