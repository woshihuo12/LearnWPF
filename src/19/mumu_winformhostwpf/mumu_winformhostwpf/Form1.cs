using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;


namespace mumu_winformhostwpf
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private System.Windows.Controls.Button btn;
        private UserControl1 usercontrol = null;
        private System.Windows.Media.Brush backgroundbrush;
        private System.Windows.Media.Brush foregroundbrush;
        private void Form1_Load(object sender, EventArgs e)
        {
            usercontrol=  elementHost1.Child as UserControl1;
            // 在UserControl里面 按钮的定义如下
            // <Button x:Name="btn"  Content="在WinForm项目中嵌入WPF内容" Margin="15"/>
            btn = usercontrol.btn;
            backgroundbrush = btn.Background;
            foregroundbrush = btn.Foreground;
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            btn.Background = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.LightGreen);
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            btn.Background = backgroundbrush;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            btn.Background = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.Blue);
        }

        private void radioButton5_CheckedChanged(object sender, EventArgs e)
        {
            btn.Foreground = foregroundbrush;
        }

        private void radioButton6_CheckedChanged(object sender, EventArgs e)
        {
            btn.Foreground = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.LightGreen);
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            btn.Foreground = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.Blue);
        }

        private void radioButton8_CheckedChanged(object sender, EventArgs e)
        {
            btn.FontSize = 12;
        }

        private void radioButton9_CheckedChanged(object sender, EventArgs e)
        {
            btn.FontSize = 14;
        }

        private void radioButton7_CheckedChanged(object sender, EventArgs e)
        {
            btn.FontSize = 16;
        }
    }
}
