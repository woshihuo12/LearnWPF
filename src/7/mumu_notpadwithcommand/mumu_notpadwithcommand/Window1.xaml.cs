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

namespace mumu_notpadwithcommand
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        private bool isDirty = false;
        private void SaveCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = isDirty;
        }
        private void SaveCommand(object sender, ExecutedRoutedEventArgs e)
        {
            MessageBox.Show("Save command triggered with " + e.Source.ToString());
            isDirty = false;
        }
        public Window1()
        {
            InitializeComponent();


            CommandBinding SaveCommandBinding = new CommandBinding(ApplicationCommands.Save,SaveCommand,SaveCommand_CanExecute);

            this.CommandBindings.Add(SaveCommandBinding);




        }

        private void NewCommand(object sender, ExecutedRoutedEventArgs e)
        {
            MessageBox.Show("New command triggered with " + e.Source.ToString());
            isDirty = false;
        }

        private void OpenCommand(object sender, ExecutedRoutedEventArgs e)
        {
            isDirty = false;
        }



        private void CloseCommand(object sender, ExecutedRoutedEventArgs e)
        {
            MessageBox.Show("CloseCommand triggered with " + e.Source.ToString());
            App.Current.Shutdown();
        }

        private void TextBox_TextChanged(object sender, RoutedEventArgs e)
        {
            isDirty = true;
        }







    }
}
