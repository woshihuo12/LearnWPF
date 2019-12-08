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

namespace mumu_timebehaviorsample
{
    /// <summary>
    /// Interaction logic for AccelDecelExample.xaml
    /// </summary>
    public partial class AccelDecelExample : Page
    {
        public AccelDecelExample()
        {
            InitializeComponent();
        }

        private void Storyboard_CurrentStateInvalidated(object sender, EventArgs e)
        {
            if (sender != null)
            {
                elapsedTime.Clock = (Clock)sender;
            }
        }
    }
}
