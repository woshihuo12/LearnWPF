using System;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace mumu_Button02
{
    public class SpaceButton : Button
    {
        // 传统.Net做法 私有字段搭配一个公开属性
        string txt;

        public string Text
        {
            set
            {
                txt = value;
                Content = SpaceOutText(txt);
            }
            get
            {
                return txt;
            }
        }
        // 依赖属性
        public static readonly DependencyProperty SpaceProperty;

        //.Net属性包装器
        public int Space
        {
            set
            {
                SetValue(SpaceProperty, value);
            }
            get
            {
                return (int)GetValue(SpaceProperty);
            }
        }

        // 静态的构造函数
        static SpaceButton()
        {
            // 定义元数据
            FrameworkPropertyMetadata metadata = new FrameworkPropertyMetadata();
            metadata.DefaultValue = 0;
            metadata.Inherits = true;
            metadata.PropertyChangedCallback += OnSpacePropertyChanged;

            // 注册依赖属性
            SpaceProperty = DependencyProperty.Register("Space", typeof(int), typeof(SpaceButton), metadata, ValidateSpaceValue);
        }

        // 值验证的回调函数
        static bool ValidateSpaceValue(object obj)
        {
            int i = (int)obj;
            return i >= 0;
        }

        // 属性值改变的回调函数
        static void OnSpacePropertyChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            SpaceButton btn = obj as SpaceButton;
            string txt = btn.Content as string;
            if (txt == null) return;
            btn.Content = btn.SpaceOutText(txt);
        }

        // 该方法给字符串间距加上空格
        string SpaceOutText(string str)
        {
            if (str == null)
                return null;

            StringBuilder build = new StringBuilder();

            // 往里面加上Space个空格
            foreach (char ch in str)
                build.Append(ch + new string(' ', Space));

            return build.ToString();
        }
    }
}

