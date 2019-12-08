using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.Integration;
namespace mumu_winformwpfdlg
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MainWindow win = new MainWindow();
            win.Title = "模态对话框";
            win.ShowDialog();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            MainWindow win = new MainWindow();
            ElementHost.EnableModelessKeyboardInterop(win);
            win.Title = "非模态对话框";
            win.Show();

        }
    }
}
