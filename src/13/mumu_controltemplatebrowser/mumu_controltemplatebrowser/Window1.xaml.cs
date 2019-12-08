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
using System.Reflection;
using System.Windows.Markup;
using System.Xml;

namespace mumu_controltemplatebrowser
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // 获得Control的程序集
            Assembly asbly = Assembly.GetAssembly(typeof(Control));

            // 获得该程序集里的所有类型
            Type[] atype = asbly.GetTypes();

            // 使用该列表存储
            SortedList<string, TreeViewItem> sortlst =
                                    new SortedList<string, TreeViewItem>();
            
            TreeViewItem item = new TreeViewItem();
            item.Header = "Control";
            item.Tag = typeof(Control);
            sortlst.Add("Control", item);
            lstTypes.Items.Add(item);

            // 遍历所有的类型，然后将派生自contorl的类型添加到列表当中
            foreach (Type typ in atype)
            {
                if (typ.IsPublic && (typ.IsSubclassOf(typeof(Control))))
                {
                    item = new TreeViewItem();
                    item.Header = typ.Name;
                    item.Tag = typ;
                    sortlst.Add(typ.Name, item);
                }
            }

            // 构建树
            foreach (KeyValuePair<string, TreeViewItem> kvp in sortlst)
            {
                if (kvp.Key != "Control")
                {
                    string strParent = ((Type)kvp.Value.Tag).BaseType.Name;
                    TreeViewItem itemParent = sortlst[strParent];
                    itemParent.Items.Add(kvp.Value);
                }
            }

            

        }

        private void lstTypes_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            try
            {
                Cursor oldcur = this.Cursor;
                this.Cursor = Cursors.Wait;

                
                
                // 获得选中的类型
                TreeViewItem selectedItem = (TreeViewItem)lstTypes.SelectedItem;
                if (selectedItem.HasItems)
                {
                    this.Cursor = oldcur;
                    //wndBar.Hide();
                    return;
                }
                Type type = (Type)selectedItem.Tag;
                
                // 实例化该type
                ConstructorInfo info = type.GetConstructor(System.Type.EmptyTypes);
                Control control = (Control)info.Invoke(null);

                // 添加该控件 但是将属性状态设置为Collapsed.
                control.Visibility = Visibility.Collapsed;
                grid.Children.Add(control);

                // 获得模板
                ControlTemplate template = control.Template;

                // 获得模板的XAML文件
                XmlWriterSettings settings = new XmlWriterSettings();
                settings.Indent = true;
                StringBuilder sb = new StringBuilder();
                XmlWriter writer = XmlWriter.Create(sb, settings);
                XamlWriter.Save(template, writer);

                // 显示模板
                txtTemplate.Text = sb.ToString();

                txtbar.Text = type.Name + "Control Template";
                // 移出该控件
                grid.Children.Remove(control);
 
                this.Cursor = oldcur;
            }
            catch (Exception err)
            {
                txtTemplate.Text = "<< Error generating template: " + err.Message + ">>";
            }
        }
    }
}
