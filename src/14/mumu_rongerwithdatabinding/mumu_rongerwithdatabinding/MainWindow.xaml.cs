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
using System.Windows.Shapes;

using System.ComponentModel;

namespace mumu_rongerwithdatabinding
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        CollectionViewSource listingDataView;
        public MainWindow()
        {
            InitializeComponent();
            listingDataView = (CollectionViewSource)(this.Resources["listingDataView"]);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AddPerson addPersonWindow = new AddPerson();
            addPersonWindow.ShowDialog();
        }

        private void Grouping_Checked(object sender, RoutedEventArgs e)
        {
            // ICollectionView  view = CollectionViewSource.GetDefaultView(listbox.ItemsSource);
            PropertyGroupDescription groupDescription = new PropertyGroupDescription();
            groupDescription.PropertyName = "Age";
            listingDataView.GroupDescriptions.Add(groupDescription);
            //view.GroupDescriptions.Add(groupDescription);
        }

        private void Grouping_Unchecked(object sender, RoutedEventArgs e)
        {
            listingDataView.GroupDescriptions.Clear();
        }

        private void Sorting_Checked(object sender, RoutedEventArgs e)
        {
            listingDataView.SortDescriptions.Add(
               new SortDescription("Age", ListSortDirection.Ascending));
        }

        private void Sorting_Unchecked(object sender, RoutedEventArgs e)
        {
            listingDataView.SortDescriptions.Clear();
        }


        private void ShowOnlyGreater30Filter(object sender, FilterEventArgs e)
        {
            Person person = e.Item as Person;
            if (person != null)
            {
                if (person.Age > 30)
                {
                    e.Accepted = true;
                }
                else
                {
                    e.Accepted = false;
                }
            }
        }

        private void Filtering_Checked(object sender, RoutedEventArgs e)
        {
            listingDataView.Filter += new FilterEventHandler(ShowOnlyGreater30Filter);
        }

        private void Filtering_Unchecked(object sender, RoutedEventArgs e)
        {
            listingDataView.Filter -= new FilterEventHandler(ShowOnlyGreater30Filter);  
        }
    }
}
