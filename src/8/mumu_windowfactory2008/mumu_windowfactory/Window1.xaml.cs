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

namespace mumu_windowfactory
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ChildWindow childwindow = new ChildWindow();

#region 设置窗口位置和尺寸
            if (startuplocationCheckBox.IsChecked == true) 
                childwindow.WindowStartupLocation = (WindowStartupLocation)Enum.Parse(typeof(WindowStartupLocation), this.startuplocationCombo.Text);
            if (this.setWSCB.IsChecked == true) 
                childwindow.WindowState = (WindowState)Enum.Parse(typeof(WindowState), this.wsLB.Text);

            if (this.setMinWidthCB.IsChecked == true) childwindow.MinWidth = double.Parse(this.minWidthTB.Text);
            if (this.setMinHeightCB.IsChecked == true) childwindow.MinHeight = double.Parse(this.minHeightTB.Text);
            if (this.setMaxWidthCB.IsChecked == true) childwindow.MaxWidth = double.Parse(this.maxWidthTB.Text);
            if (this.setMaxHeightCB.IsChecked == true) childwindow.MaxHeight = double.Parse(this.maxHeightTB.Text);
            if (this.setWidthCB.IsChecked == true) childwindow.Width = double.Parse(this.widthTB.Text);
            if (this.setHeightCB.IsChecked == true) childwindow.Height = double.Parse(this.heightTB.Text);
            if (this.setSTCCB.IsChecked == true) childwindow.SizeToContent = (SizeToContent)Enum.Parse(typeof(SizeToContent), this.stcLB.Text);
#endregion

            #region 设置窗口外观和样式
            if (resizeModeCheckBox.IsChecked == true)
                childwindow.ResizeMode = (ResizeMode)Enum.Parse(typeof(ResizeMode), this.resizeModeCombo.Text);

            if(windowStyleCheckBox.IsChecked == true)
                childwindow.WindowStyle = (WindowStyle)Enum.Parse(typeof(WindowStyle), this.windowStyleCombo.Text);

            
            childwindow.Topmost = (bool)this.IsTopmost.IsChecked;
            childwindow.ShowInTaskbar = (bool)this.IsShowInTaskBar.IsChecked;
            #endregion
            childwindow.Show();

           

        }
    }
}
