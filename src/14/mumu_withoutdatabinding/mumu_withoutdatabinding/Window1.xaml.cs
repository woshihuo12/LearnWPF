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

namespace mumu_withoutdatabinding
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        Person[] persons = null;
        int curIndex = 0;

        public Window1()
        {
            InitializeComponent();

            // 数据集
            persons = new Person[3];
            persons[0] = new Person("木木", 22);
            persons[1] = new Person("黄药师", 48);
            persons[2] = new Person("黄蓉", 20);
            curIndex = 0;
            this.nameTextBox.Text = persons[curIndex].Name;
            this.ageTextBox.Text = persons[curIndex].Age.ToString();

            modifyButton.IsEnabled = false;
        }

        private void nameTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            modifyButton.IsEnabled = true;
        }

        private void ageTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            modifyButton.IsEnabled = true;
        }

        private void nextButton_Click(object sender, RoutedEventArgs e)
        {

            curIndex++;
            if (curIndex >= 3) curIndex = 0;
            this.nameTextBox.Text = persons[curIndex].Name;
            this.ageTextBox.Text = persons[curIndex].Age.ToString();
            modifyButton.IsEnabled = false;

        }
        private void modifyButton_Click(object sender, RoutedEventArgs e)
        {
            persons[curIndex].Name = nameTextBox.Text;
            int age = 0;
            if (int.TryParse(ageTextBox.Text, out age))
            {
                persons[curIndex].Age = age;
            }
            modifyButton.IsEnabled = false;
        }
    }
}
