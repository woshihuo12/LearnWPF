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
using System.Windows.Media.Animation;

namespace mumu_animationtype
{
    /// <summary>
    /// Interaction logic for SplineExample.xaml
    /// </summary>
    public partial class rotateanimationusingpathexample : Page
    {
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            NameScope.SetNameScope(this, new NameScope());
            //this.RegisterName(MyRotateTransform.Name, MyRotateTransform);
            //this.RegisterName(MyRotateTransform.Name, MyRotateTransform);
        }
        private void BeginButton_Click(object sender, RoutedEventArgs e)
        {
            mystoryboard.Begin(this, true);
        }
        private void PauseButton_Click(object sender, RoutedEventArgs e)
        {
            mystoryboard.Pause(this);
        }
        private void ResumeButton_Click(object sender, RoutedEventArgs e)
        {
            mystoryboard.Resume(this);
        }
        private void StopButton_Click(object sender, RoutedEventArgs e)
        {
            mystoryboard.Stop(this);
        }
    }
}
