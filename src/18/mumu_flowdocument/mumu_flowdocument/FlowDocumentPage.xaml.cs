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

using System.Windows.Documents.Serialization;

using System.IO;
namespace mumu_flowdocument
{
    /// <summary>
    /// Interaction logic for FlowDocumentPage.xaml
    /// </summary>
    public partial class FlowDocumentPage : Page
    {
        public FlowDocumentPage()
        {
            InitializeComponent();
        }
        private void AddCommandHandlers(FrameworkElement uiScope)
        {


            // Add Command Handlers
            CommandBindingCollection commandBindings = uiScope.CommandBindings;



            commandBindings.Add(
                new CommandBinding(FlowDocumentPage.SaveAs,
                            new ExecutedRoutedEventHandler(OnSaveAs),
                            new CanExecuteRoutedEventHandler(OnNewQuery)));


        }

        public static RoutedCommand SaveAs
        { get { return DeclareCommand(ref _SaveAs, "SaveAs"); } }

        private static RoutedCommand _SaveAs;


        private static RoutedCommand DeclareCommand(ref RoutedCommand command,
                                                      string commandDebugName)
        { return DeclareCommand(ref command, commandDebugName, null); }

        private static RoutedCommand DeclareCommand(ref RoutedCommand command,
                                string commandDebugName, InputGesture gesture)
        {
            if (command == null)
            {
                InputGestureCollection collection = null;

                if (gesture != null)
                {
                    collection = new InputGestureCollection();
                    collection.Add(gesture);
                }

                command = new RoutedCommand(commandDebugName,
                                            typeof(FlowDocumentPage), collection);
            }
            return command;
        }


        private static void OnNewQuery(
                        object target, CanExecuteRoutedEventArgs args)
        {
            args.CanExecute = true;
        }


        private void OnSaveAs(object target, ExecutedRoutedEventArgs args)
        {
            SaveDocumentAsFile(null);
        }
        private string PlugInFileFilter
        {
            get
            {   // Create a SerializerProvider for accessing plug-in serializers.
                SerializerProvider serializerProvider = new SerializerProvider();
                string filter = "";

                // For each loadable serializer, add its display
                // name and extension to the filter string.
                foreach (SerializerDescriptor serializerDescriptor in
                    serializerProvider.InstalledSerializers)
                {
                    if (serializerDescriptor.IsLoadable)
                    {
                        // After the first, separate entries with a "|".
                        if (filter.Length > 0) filter += "|";

                        // Add an entry with the plug-in name and extension.
                        filter += serializerDescriptor.DisplayName + " (*" +
                            serializerDescriptor.DefaultFileExtension + ")|*" +
                            serializerDescriptor.DefaultFileExtension;
                    }
                }

                // Return the filter string of installed plug-in serializers.
                return filter;
            }
        }

        // ----------------------- SaveDocumentAsFile -------------------------
        public bool SaveDocumentAsFile(string fileName)
        {
            // If no filename was specified, prompt the user for one.
            if (fileName == null)
            {
                // Create a File | Save As... dailog.
                Microsoft.Win32.SaveFileDialog dialog;
                dialog = new Microsoft.Win32.SaveFileDialog();
                dialog.CheckFileExists = false;
                dialog.Filter = this.PlugInFileFilter +
                                "|XAML FlowDocument (*.xaml)|*.xaml" +
                                "|HTML Document (*.html; *.htm)|*.html; *.htm" +
                                "|WordXML Document (*.xml)|*.xml" +
                                "|RTF Document (*.rtf)|*.rtf" +
                                "|Plain Text (*.txt)|*.txt";

                // Display the dialog and wait for the user response.
                bool result = (bool)dialog.ShowDialog(null);

                // If the user clicked "Cancel", cancel the saving the file.
                if (result == false) return false;
                fileName = dialog.FileName;
            }

            // Save the document to the specified file.
            return SaveToFile(fileName);
        }// end:SaveDocumentAsFile()
        private bool SaveToFile(string fileName)
        {
            if (fileName == null) throw new ArgumentNullException("fileName");

            // If the file already exists, delete it (replace).
            if (File.Exists(fileName)) File.Delete(fileName);

            FlowDocument flowDocument = FDPV.Document as FlowDocument;
            string fileContent = null;
            try
            {
                // Create a SerializerProvider for accessing plug-in serializers.
                SerializerProvider serializerProvider = new SerializerProvider();

                // Locate the serializer that matches the fileName extension.
                SerializerDescriptor selectedPlugIn = null;
                foreach (SerializerDescriptor serializerDescriptor in
                                serializerProvider.InstalledSerializers)
                {
                    if (serializerDescriptor.IsLoadable &&
                         fileName.EndsWith(serializerDescriptor.DefaultFileExtension))
                    {   // The plug-in serializer and fileName extensions match.
                        selectedPlugIn = serializerDescriptor;
                        break; // foreach
                    }
                }

                // If a match for a plug-in serializer was found,
                // use it to output and store the document.
                if (selectedPlugIn != null)
                {
                    Stream package = File.Create(fileName);
                    SerializerWriter serializerWriter =
                        serializerProvider.CreateSerializerWriter(selectedPlugIn,
                                                                  package);
                    IDocumentPaginatorSource idoc =
                        flowDocument as IDocumentPaginatorSource;
                    serializerWriter.Write(idoc.DocumentPaginator, null);
                    package.Close();
                    return true;
                }
                else if (fileName.EndsWith(".xml"))
                {
                    // Save as a WordXML document.
                    // WordXmlSerializer.SaveToFile(fileName, flowDocument.ContentStart, flowDocument.ContentEnd);
                    return true;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(
                    "Error occurred during a conversion to this file format: " +
                    fileName + "\n" + e.ToString(), this.GetType().Name,
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            if (fileContent == null)
            {
                MessageBox.Show("A serializer for the given file extension" +
                    "could not be found.\n" + fileName, this.GetType().Name,
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            // Output the formatted content to the specified file.
            try
            {   // Write the file content.
                StreamWriter writer = new StreamWriter(fileName);
                writer.WriteLine(fileContent);
                writer.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show("Error occurred during document save: " +
                    fileName + "\n" + e.ToString(), this.GetType().Name,
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            return true;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            AddCommandHandlers(this);
        }// end:SaveToFile()
    }
}
