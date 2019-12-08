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

using System.IO;
using System.Windows.Forms;
using System.Windows.Xps.Packaging;

namespace mumu_documentviewer
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();


            AddCommandBindings(ApplicationCommands.Open, OpenCommandHandler);

            AddCommandBindings(ApplicationCommands.Close, CloseCommandHandler);
        }

        #region Private Methods
        // ----------------------- OpenCommandHandler -------------------------
        /// <summary>
        ///   Opens an existing XPS document and displays
        ///   it with a DocumentViewer./// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OpenCommandHandler(
            object sender, ExecutedRoutedEventArgs e)
        {
            
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.InitialDirectory = Directory.GetCurrentDirectory();
            dlg.Filter = "Xps Documents (*.xps)|*.xps";
            dlg.FilterIndex = 1;
            if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                // If there's an existing document open, close it.
                if (_xpsDocument != null)
                    _xpsDocument.Close();

                // Create an XpsDocument for the file specified by the user.
                try
                {
                    _xpsDocument = new
                        XpsDocument(dlg.FileName, System.IO.FileAccess.Read);
                }
                catch (UnauthorizedAccessException)
                {
                    System.Windows.MessageBox.Show(
                        String.Format("Unable to access {0}", dlg.FileName));
                    return;
                }

                docViewer.Document = _xpsDocument.GetFixedDocumentSequence();
                
                _fileName = dlg.FileName;
            }
        }// end:OpenCommandHandler()


        // ------------------------ CloseCommandHandler -----------------------
        /// <summary>
        ///   File|Exit handler - Closes current XPS document.</summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CloseCommandHandler(object sender, ExecutedRoutedEventArgs e)
        {
            this.Close();
        }// end:CloseCommandHandler()


        // ------------------------ AddCommandBindings ------------------------
        /// <summary>
        ///     Registers menu commands (helper method).</summary>
        /// <param name="command"></param>
        /// <param name="handler"></param>
        private void AddCommandBindings(ICommand command, ExecutedRoutedEventHandler handler)
        {
            CommandBinding cmdBindings = new CommandBinding(command);
            cmdBindings.Executed += handler;
            CommandBindings.Add(cmdBindings);
        }// end:AddCommandBindings()


        #endregion Private Methods


        #region Private Members
        private XpsDocument _xpsDocument;
        private string _fileName;
        #endregion  Private Members
    }
}
