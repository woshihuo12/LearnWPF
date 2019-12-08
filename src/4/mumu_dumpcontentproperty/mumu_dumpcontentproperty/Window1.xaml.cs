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


using System.Reflection;
using System.Windows.Markup;
namespace mumu_dumpcontentproperty
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

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            listbox.Items.Add("类名:   Content属性名");
            foreach (AssemblyName asmblyname in
                        Assembly.GetExecutingAssembly().GetReferencedAssemblies())
            {

                foreach (Type type in Assembly.Load(asmblyname).GetTypes())
                {

                    foreach (object obj in type.GetCustomAttributes(
                                            typeof(ContentPropertyAttribute), true))
                    {

                        if (type.IsPublic && obj as ContentPropertyAttribute != null)
                            listbox.Items.Add(type.Name + ":   " + (obj as ContentPropertyAttribute).Name);
                    }
                }
            }

        }
    }
}
