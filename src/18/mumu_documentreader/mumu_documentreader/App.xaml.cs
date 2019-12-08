using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;


using System.Windows.Documents.Serialization;
namespace mumu_documentreader
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            try
            {
                SerializerProvider.RegisterSerializer(SerializerDescriptor.CreateFromFactoryInstance(new XamlSerializerFactory()), false);
                //SerializerProvider.RegisterSerializer(SerializerDescriptor.CreateFromFactoryInstance(new TxtSerializerFactory()), false);
                //SerializerProvider.RegisterSerializer(SerializerDescriptor.CreateFromFactoryInstance(new RtfSerializerFactory()), false);
                SerializerProvider.RegisterSerializer(SerializerDescriptor.CreateFromFactoryInstance(new HtmlSerializerFactory()), false);
            }
            catch (ArgumentException)
            {
            }
        }

        private void Application_Exit(object sender, ExitEventArgs e)
        {

            try
            {
                SerializerProvider.UnregisterSerializer(SerializerDescriptor.CreateFromFactoryInstance(new XamlSerializerFactory()));
                //SerializerProvider.UnregisterSerializer(SerializerDescriptor.CreateFromFactoryInstance(new TxtSerializerFactory()));
                //SerializerProvider.UnregisterSerializer(SerializerDescriptor.CreateFromFactoryInstance(new RtfSerializerFactory()));
                SerializerProvider.UnregisterSerializer(SerializerDescriptor.CreateFromFactoryInstance(new HtmlSerializerFactory()));
            }
            catch (ArgumentException)
            {
            }
        }
    }
}
