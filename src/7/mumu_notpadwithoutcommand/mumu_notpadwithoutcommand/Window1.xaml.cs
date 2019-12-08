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

namespace mumu_notpadwithoutcommand
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        // 声明了四个KeyGesture，分别是Ctrl+X，Ctrl+C，Ctrl+V，delete四种组合方式
        private KeyGesture gestCut = new KeyGesture(Key.X, ModifierKeys.Control);
        private KeyGesture gestCopy = new KeyGesture(Key.C, ModifierKeys.Control);
        private KeyGesture gestPaste = new KeyGesture(Key.V, ModifierKeys.Control);
        private KeyGesture gestDelete = new KeyGesture(Key.Delete);

        public Window1()
        {
            InitializeComponent();
        }
        protected void CutOnClick(object sender, RoutedEventArgs args)
        {
            CopyOnClick(sender, args);
            DeleteOnClick(sender, args);
        }
        protected void CopyOnClick(object sender, RoutedEventArgs args)
        {
            if (text.Text != null && text.Text.Length > 0)
                Clipboard.SetText(text.Text);
        }
        protected void PasteOnClick(object sender, RoutedEventArgs args)
        {
            if (Clipboard.ContainsText())
                text.Text = Clipboard.GetText();
        }
        protected void DeleteOnClick(object sender, RoutedEventArgs args)
        {
            text.Text = null;
        }


        // 重载了窗口的OnPreviewKeyDown函数，在这里比较输入参数是否是预定义好的几种快捷键方式
        protected override void OnPreviewKeyDown(KeyEventArgs e)
        {
            base.OnPreviewKeyDown(e);
            // 添加状态控制代码
            bool iscut, isdelete, iscopy, ispaste;
            iscut = isdelete = iscopy = text.Text != null && text.Text.Length > 0;
            ispaste = Clipboard.ContainsText();

            if (iscut && gestCut.Matches(null, e))
            {
                CutOnClick(this, e);
                e.Handled = true;
            }
            else if (iscopy && gestCopy.Matches(null, e))
            {
                CopyOnClick(this, e);
                e.Handled = true;
            }

            else if (ispaste && gestPaste.Matches(null, e))
            {
                PasteOnClick(this, e);
                e.Handled = true;
            }
            else if (isdelete && gestDelete.Matches(null, e))
            {
                DeleteOnClick(this, e);
                e.Handled = true;
            }

            else
            {

                e.Handled = false;
            }
        }


        private void MenuItem_SubmenuOpened(object sender, RoutedEventArgs e)
        {
            cutitem.IsEnabled = copyitem.IsEnabled = deleteitem.IsEnabled = text.Text != null && text.Text.Length > 0;
            pasteitem.IsEnabled = Clipboard.ContainsText();
        }

        private void text_ContextMenuOpening(object sender, ContextMenuEventArgs e)
        {
            cutitemcontextmenu.IsEnabled = copyitemcontextmenu.IsEnabled = deleteitemcontextmenu.IsEnabled = text.Text != null && text.Text.Length > 0;
            pasteitemcontextmenu.IsEnabled = Clipboard.ContainsText();
        }


    }
}
