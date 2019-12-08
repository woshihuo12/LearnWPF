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

using System.Windows.Annotations;
using System.Windows.Annotations.Storage;
using System.IO;
namespace mumu_flowdocument
{
    /// <summary>
    /// Interaction logic for AnnotationSample.xaml
    /// </summary>
    public partial class AnnotationSample : Page
    {
        public AnnotationSample()
        {
            InitializeComponent();
        }


        private FileStream stream;
        private void Page_Unloaded(object sender, RoutedEventArgs e)
        {
            AnnotationService service = AnnotationService.GetService(reader);
            if (service != null && service.IsEnabled)
            {
                service.Store.Flush();
                service.Disable();
                stream.Close();
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            AnnotationService service = AnnotationService.GetService(reader);
            if (service == null)
            {
                stream = new FileStream("storage.xml", FileMode.OpenOrCreate);
                service = new AnnotationService(reader);
                AnnotationStore store = new XmlStreamStore(stream);
                service.Enable(store);
            }

        }
    }
}
