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


using System.Windows.Markup;
using System.Windows.Documents.Serialization;
using System.IO;
using System.Diagnostics;
using System.Windows.Threading;
using System.Windows.Controls.Primitives;
using System.Reflection;
using System.Windows.Annotations;
using System.Windows.Annotations.Storage;

using System.Threading;

namespace mumu_documentreader
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        #region 文件过滤
        private static string xamlfileFilter = "XAML 流文档 (*.xaml)|*.xaml";


        private string PlugInFileFilter
        {
            get
            {   // 创建一个SerializerProvider
                SerializerProvider serializerProvider = new SerializerProvider();
                string filter = "";

                // 
                foreach (SerializerDescriptor serializerDescriptor in
                    serializerProvider.InstalledSerializers)
                {
                    if (serializerDescriptor.IsLoadable)
                    {

                        if (filter.Length > 0) filter += "|";


                        filter += serializerDescriptor.DisplayName + " (*" +
                            serializerDescriptor.DefaultFileExtension + ")|*" +
                            serializerDescriptor.DefaultFileExtension;
                    }
                }


                return filter;
            }
        }
        #endregion

        #region
        private string _fileName;
        #endregion

        #region 缩略图所需要的字段

        private int _thumbnailHeight = 72;
        private int _thumbnailWidth = 60;
        private int _maxThumbnails;
        private int _borderIncrement = 20;
        private int _currentThumbnail = 0;


        #endregion

        #region 书签和标注所需要的字段

        AnnotationService _annServ = null;
        AnnotationStore _annStore = null;
        MemoryStream _annotationBuffer = null;
        #endregion
        public Window1()
        {
            InitializeComponent();
        }



        #region 命令
        public static RoutedCommand AddBookmark
        { get { return DeclareCommand(ref _AddBookmark, "AddBookmark"); } }

        private static RoutedCommand _AddBookmark;

        public static RoutedCommand AddComment
        { get { return DeclareCommand(ref _AddComment, "AddComment"); } }
        private static RoutedCommand _AddComment;


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
                                            typeof(Window1), collection);
            }
            return command;
        }

        private static void OnNewQuery(
                         object target, CanExecuteRoutedEventArgs args)
        {
            args.CanExecute = true;
        }

        #endregion

        #region 书签
        void OnAddBookmark(object sender, RoutedEventArgs args)
        {
            try
            {
                System.Windows.Media.Color col = (System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#A6FFFF00");
                System.Windows.Media.Brush myBrush = new SolidColorBrush(col);
                string userName = System.Windows.Forms.SystemInformation.UserName;
                AnnotationHelper.CreateHighlightForSelection(
                                            _annServ, userName, myBrush);
            }
            catch (InvalidOperationException)
            {
                return;
            }

        }

        void _annStore_StoreContentChanged(object sender, StoreContentChangedEventArgs e)
        {
            if (e.Action == StoreContentAction.Deleted) return;
            Annotation ann = e.Annotation;
            if (ann.AnnotationType.Name == "Highlight")
            {
                AddBookmarkOrComment(BookmarkList, ann);
            }
            else
            {
                AddBookmarkOrComment(CommentsList, ann);
            }
            
        }

        private void AddBookmarkOrComment(ListBox collection, Annotation ann)
        {
            if (ann.Cargos.Count <= 1)
            {
                ann.Cargos.Add(
                    new AnnotationResource(FDPV.MasterPageNumber.ToString()));
            }

            StackPanel EntryInList ;
            if (collection == BookmarkList)
                EntryInList = new BookOrCommandItem(BookOrCommandItemType.Book);
            else
                EntryInList = new BookOrCommandItem(BookOrCommandItemType.Command);


            EntryInList.Width = BookmarkList.Width - 10;

            Button GoToMark = LogicalTreeHelper.FindLogicalNode(
                                            EntryInList, "GoToMark") as Button;

            if (GoToMark != null)
            {
                GoToMark.Tag = ann;
                GoToMark.Click += new RoutedEventHandler(GoToMark_Click);
            }

            MenuItem GoToMenu = LogicalTreeHelper.FindLogicalNode(
                                  GoToMark.ContextMenu, "GoToMenu") as MenuItem;

            GoToMenu.Click += new RoutedEventHandler(GoToMark_Click);
            GoToMenu.Tag = ann;

            MenuItem DeleteMark = LogicalTreeHelper.FindLogicalNode(
                                  GoToMark.ContextMenu, "DeleteMark") as MenuItem;

            DeleteMark.Click += new RoutedEventHandler(DeleteMark_Click);
            DeleteMark.Tag = ann;


            

            collection.Items.Add(EntryInList);

            TextBlock spText =
                LogicalTreeHelper.FindLogicalNode(EntryInList, "TB") as TextBlock;

            string MarkText = "";
            if (spText != null)
            {
                ContentLocator cloc =
                    ann.Anchors[0].ContentLocators[0] as ContentLocator;
                if (cloc == null) return;
                if (cloc.Parts.Count < 2) return;

                ContentLocatorPart cPart = cloc.Parts[1];
                if (cPart == null) return;
                if (cPart.NameValuePairs["Segment0"] != null)
                {
                    string[] charPos = cPart.NameValuePairs["Segment0"].Split(',');
                    FlowDocument fd = FDPV.Document as FlowDocument;
                    TextPointer tp = fd.ContentStart.GetPositionAtOffset(
                        int.Parse(charPos[0]), LogicalDirection.Forward);

                    if (tp == null) return;
                    if (tp.GetPointerContext(LogicalDirection.Forward)
                        == TextPointerContext.Text)
                    {
                        MarkText += tp.GetTextInRun(LogicalDirection.Forward);
                    }
                    spText.Text = MarkText.Substring(0,
                        (MarkText.Length > 150) ? 150 : MarkText.Length);
                }
            }
        }

        void GoToMark_Click(object sender, RoutedEventArgs e)
        {
            Annotation ann = null;
            if (sender is Button)
                ann = ((Button)sender).Tag as Annotation;
            else if (sender is MenuItem)
                ann = ((MenuItem)sender).Tag as Annotation;
            if (ann == null) return;

            ContentLocator cloc =
                ann.Anchors[0].ContentLocators[0] as ContentLocator;
            if (cloc == null) return;
            if (cloc.Parts.Count < 2) return;

            ContentLocatorPart cPart = cloc.Parts[1];
            if (cPart == null) return;
            if (cPart.NameValuePairs["Segment0"] != null)
            {
                string[] charPos = cPart.NameValuePairs["Segment0"].Split(',');
                FlowDocument fd = FDPV.Document as FlowDocument;
                TextPointer tp = fd.ContentStart.GetPositionAtOffset(
                                 int.Parse(charPos[0]), LogicalDirection.Forward);
                if (tp == null) return;

                FrameworkContentElement fce = tp.Parent as FrameworkContentElement;
                if (fce == null) return;

                fce.BringIntoView();
            }
        }

        void DeleteMark_Click(object sender, RoutedEventArgs e)
        {
            Annotation ann = ((MenuItem)sender).Tag as Annotation;
            _annStore.DeleteAnnotation(ann.Id);
            _annStore.Flush();

            MenuItem thisMenu = sender as MenuItem;
            ContextMenu parentMenu = thisMenu.Parent as ContextMenu;
            FrameworkElement dObj =
                parentMenu.PlacementTarget as FrameworkElement;
            while (!(dObj is StackPanel))
            {
                dObj = dObj.Parent as FrameworkElement;
            }

            ListBox collection = dObj.Parent as ListBox;
            collection.Items.Remove(dObj);
            // Util.FlushDispatcher();
        }


        void OnAddComment(object sender, RoutedEventArgs args)
        {
            try
            {
                string userName = System.Windows.Forms.SystemInformation.UserName;
                //AnnotationHelper.CreateTextStickyNoteForSelection(
                //                                            _annServ, userName);
                AnnotationHelper.CreateInkStickyNoteForSelection(_annServ, userName);
            }
            catch (InvalidOperationException)
            {
                return;
            }

            //AddBookmarkOrComment(CommentsList);
            //Annotation ann1 = _annStore.GetAnnotations()[0];
            //ColorConverter converter = new ColorConverter();
            //Nullable<Color> color = ((SolidColorBrush)Brushes.Yellow).Color;
            //ann1.Cargos[0].Contents[0].Attributes[0].Value =
            //    converter.ConvertToInvariantString(color.Value);
        }
        #endregion

        #region  打开文件
        private Thread thread;
        private void OnOpen(object sender, ExecutedRoutedEventArgs e)
        {

            Window1 win = (Window1)sender;
            // 打开文档成功 ，则创建Thumbs
            if (win.OpenDocument())
            {
                win.CreateThumbs();

                (FDPV.Document.DocumentPaginator as
                    DynamicDocumentPaginator).PaginationCompleted +=
                                        new EventHandler(PaginationCompleted);
                // FDPV.Document.DocumentPaginator.GetPageCompleted += new GetPageCompletedEventHandler(DocumentPaginator_GetPageCompleted);
            }
        }



        public bool OpenDocument()
        {
            // 如果有文档 则需要关闭掉 再打开
            if (FDPV.Document != null) CloseFile();

            // 弹出一个打开对话框
            Microsoft.Win32.OpenFileDialog dialog;
            dialog = new Microsoft.Win32.OpenFileDialog();
            dialog.CheckFileExists = true;
            dialog.InitialDirectory = Directory.GetCurrentDirectory();
            dialog.Filter = xamlfileFilter;
            bool result = (bool)dialog.ShowDialog(null);
            if (result == false) return false;
            string fileName = dialog.FileName;

            object newDocument = null;
            try
            {
                if (fileName.EndsWith(".xaml"))
                {
                    using (FileStream inputStream = File.OpenRead(fileName))
                    {
                        ParserContext pc = new ParserContext();
                        pc.BaseUri =
                            new Uri(System.Environment.CurrentDirectory + "/");

                        // 如果这个地方不传ParserContext 外面两个图片就显示不出来
                        newDocument = XamlReader.Load(inputStream, pc) as object;
                        // newDocument = XamlReader.Load(inputStream) as object;

                        if (newDocument == null)
                        {
                            MessageBox.Show(
                                "无法解释该XAML文件" +
                                fileName, "错误提示",
                                MessageBoxButton.OK, MessageBoxImage.Error);
                            return false;
                        }

                        

                  
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(
                    "该文件可能不是正确的流文档格式: " +
                    fileName + "\n", "错误提示",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            if (newDocument is IDocumentPaginatorSource)
            {
                if (FDPV.Document != null)
                {

                    (FDPV.Document.DocumentPaginator as
                        DynamicDocumentPaginator).PaginationCompleted -=
                                                          PaginationCompleted;
                }
                FDPV.Document = (IDocumentPaginatorSource)newDocument;

                // 调用这句话的意义在哪儿 意义在于阻塞一下当前执行。因为执行的是一个SystemIdle
                // 因此只有别的任务执行完毕 才会继续往下执行

                //Util.FlushDispatcher();

                Dispatcher dis = this.Dispatcher;
                dis.Invoke(DispatcherPriority.SystemIdle, new DispatcherOperationCallback(delegate { return null; }), null);


                //(FDPV.Document.DocumentPaginator as
                //    DynamicDocumentPaginator).PaginationCompleted +=
                //                        new EventHandler(PaginationCompleted);

                _fileName = fileName;
            }

            else // if !(newDocument is IDocumentPaginatorSource)
            {
                throw new InvalidDataException(
                    "Thumbnail viewer only supports IDocumentPaginatorSource");
                //return false;
            }

            return true;
        }


        #endregion


        #region 另存为

        private void CanSaveAs(object sender, CanExecuteRoutedEventArgs e)
        {
            Window1 win = (Window1)sender;
            if (win == null) return;
            if (win.FDPV == null) return;
            if (win.FDPV.Document != null)
                e.CanExecute = true;

        }

        private void SaveAs(object sender, ExecutedRoutedEventArgs e)
        {
            SaveDocumentAsFile(null);
        }

        public bool SaveDocumentAsFile(string fileName)
        {
            // 如果文件名为空 则需要弹出一个Save对话框
            if (fileName == null)
            {
                // Create a File | Save As... dailog.
                Microsoft.Win32.SaveFileDialog dialog;
                dialog = new Microsoft.Win32.SaveFileDialog();
                dialog.CheckFileExists = false;
                //dialog.Filter = this.PlugInFileFilter +
                //                "|XAML FlowDocument (*.xaml)|*.xaml" +
                //                "|HTML Document (*.html; *.htm)|*.html; *.htm" +
                //                "|WordXML Document (*.xml)|*.xml" +
                //                "|RTF Document (*.rtf)|*.rtf" +
                //                "|Plain Text (*.txt)|*.txt";
                dialog.Filter = this.PlugInFileFilter;

                bool result = (bool)dialog.ShowDialog(null);


                if (result == false) return false;
                fileName = dialog.FileName;
            }


            return SaveToFile(fileName);
        }

        private bool SaveToFile(string fileName)
        {
            if (fileName == null) throw new ArgumentNullException("文件名为空");
            if (File.Exists(fileName)) File.Delete(fileName);

            FlowDocument flowDocument = FDPV.Document as FlowDocument;

            try
            {
                SerializerProvider serializerProvider = new SerializerProvider();
                SerializerDescriptor selectedPlugIn = null;
                foreach (SerializerDescriptor serializerDescriptor in
                                serializerProvider.InstalledSerializers)
                {
                    // 如果IsLoadable属性为真，且后缀名一致
                    if (serializerDescriptor.IsLoadable &&
                         fileName.EndsWith(serializerDescriptor.DefaultFileExtension))
                    {
                        selectedPlugIn = serializerDescriptor;
                        break;
                    }
                }


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

            }
            catch (Exception e)
            {
                MessageBox.Show(
                    "格式转换出错" +
                    fileName + "\n", "错误提示",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            return true;
        }
        #endregion
        private void CloseFile()
        {
            
             
            FDPV.Document = null;
        }
        private void CopyStream(Stream src, Stream dest)
        {
            long originalPosition = src.Position;

            src.Seek(0, SeekOrigin.Begin);
            dest.Seek(0, SeekOrigin.Begin);
            dest.SetLength(0); // Erase destination.
            StreamReader reader = new StreamReader(src);
            StreamWriter writer = new StreamWriter(dest);
            while (!reader.EndOfStream)
            {
                char[] buffer = new char[50];
                reader.Read(buffer, 0, buffer.Length);
                writer.Write(buffer);
            }
            writer.Flush();
            dest.Seek(0, SeekOrigin.Begin);

            src.Seek(originalPosition, SeekOrigin.Begin);
        }// end:CopyStream()


        #region Thumbs

        void PaginationCompleted(object sender, EventArgs e)

        {
            Dispatcher.BeginInvoke(DispatcherPriority.Normal,
                new DispatcherOperationCallback(
                    delegate { RefreshThumbnails(); return null; }), null);
        }


        public void RefreshThumbnails()
        {            
            if ((ThumbList.Items != null) && (ThumbList.Items.Count > 0))
                ThumbList.Items.Clear();

            _currentThumbnail = 0;
            _maxThumbnails = FDPV.PageCount;

            for (int i = _currentThumbnail; i < _maxThumbnails; i++)
                AddPageThumb(i);

            ThumbList.Items.MoveCurrentToPosition(_currentThumbnail);

        }

        public void CreateThumbs()
        {
            if ((ThumbList.Items != null) && (ThumbList.Items.Count > 0))
                ThumbList.Items.Clear();

            _currentThumbnail = 0;
            _maxThumbnails = FDPV.PageCount;

            for (int i = _currentThumbnail; i < _maxThumbnails; i++)
                AddPageThumb(i);

            ThumbList.Items.MoveCurrentToPosition(_currentThumbnail);
            FDPV.Focus();
        }



        private void AddPageThumb(int pageIndex)
        {
            Border thumbBorder = CreateThumbBorder(pageIndex + 1);
            DocumentPage docPage =
                FDPV.Document.DocumentPaginator.GetPage(pageIndex);
            StackPanel pageThumb =
                CreatePageThumb(docPage.Visual, docPage.Size, pageIndex + 1);
            if (pageThumb != null)
            {
                PrepareBorderForDisplay(thumbBorder);
                thumbBorder.Child = pageThumb;
            }

            ThumbList.Items.Add(thumbBorder);
        }

        private Border CreateThumbBorder(int pageNumber)
        {
            Border OuterBorder = new Border();
            OuterBorder.BorderThickness = new Thickness(2);
            OuterBorder.Margin = new Thickness(3);
            OuterBorder.Width = _thumbnailWidth + _borderIncrement;
            OuterBorder.Height = _thumbnailHeight + _borderIncrement;
            OuterBorder.Background = System.Windows.Media.Brushes.White;
            OuterBorder.Tag = pageNumber;
            return OuterBorder;
        }

        private void PrepareBorderForDisplay(Border border)
        {
            border.BorderBrush = System.Windows.Media.Brushes.Black;
            border.MouseLeftButtonDown +=
                new MouseButtonEventHandler(ThumbnailClick);

            // add ContextMenu.
            ContextMenu cm = new ContextMenu();
            MenuItem miReduce = new MenuItem();
            miReduce.Header = "小视图";
            miReduce.Click += new RoutedEventHandler(miReduce_Click);
            MenuItem miEnlarge = new MenuItem();
            miEnlarge.Header = "大视图";
            miEnlarge.Click += new RoutedEventHandler(miEnlarge_Click);

            cm.Items.Add(miReduce);
            cm.Items.Add(miEnlarge);

            border.ContextMenu = cm;
        }
        void miEnlarge_Click(object sender, RoutedEventArgs e)
        {
            _thumbnailHeight *= 2;
            _thumbnailWidth *= 2;
            RefreshThumbnailSizes();
        }

        void miReduce_Click(object sender, RoutedEventArgs e)
        {
            _thumbnailHeight /= 2;
            _thumbnailWidth /= 2;
            RefreshThumbnailSizes();
        }
        void ThumbnailClick(object sender, MouseButtonEventArgs e)
        {
            Border OuterBorder = sender as Border;
            _currentThumbnail = int.Parse(OuterBorder.Tag.ToString());
            FDPV.GoToPage(_currentThumbnail);
        }
        private void RefreshThumbnailSizes()
        {

            foreach (Border OuterBorder in ThumbList.Items)
            {
                if (OuterBorder.Child != null)
                {
                    StackPanel sp = OuterBorder.Child as StackPanel;
                    System.Windows.Controls.Image img = sp.Children[0] as System.Windows.Controls.Image;
                    img.Height = _thumbnailHeight;
                }
                OuterBorder.Width = _thumbnailWidth + _borderIncrement;
                OuterBorder.Height = _thumbnailHeight + _borderIncrement;
            }
            RefreshThumbnails();
        }
        private StackPanel CreatePageThumb(
                            Visual visual, System.Windows.Size size, int pageNumber)
        {
            if (visual != null)
            {
                Rect pageRect = new Rect(size);
                RenderTargetBitmap renderTargetBitmap = new RenderTargetBitmap(
                        (int)pageRect.Width, (int)pageRect.Height,
                         96.0, 96.0, PixelFormats.Pbgra32);
                renderTargetBitmap.Render(visual);

                StackPanel sp = new StackPanel();
                System.Windows.Controls.Image img = new System.Windows.Controls.Image();
                img.Source = renderTargetBitmap;
                img.Height = _thumbnailHeight;

                TextBlock tb = new TextBlock(new Run(pageNumber.ToString()));
                tb.FontSize = 10;

                sp.Orientation = Orientation.Vertical;
                sp.Children.Add(img);
                sp.Children.Add(tb);
                return sp;
            }
            return null;
        }

        #endregion


        #region GridSplitter事件
        void SplitterEndResize(object sender, DragCompletedEventArgs e)
        {
            LeftTabControl.Width = MainGrid.ColumnDefinitions[0].ActualWidth;
            ThumbList.Width = LeftTabControl.Width - 24;
            BookmarkList.Width = LeftTabControl.Width - 24;
            CommentsList.Width = LeftTabControl.Width - 24;
        }



        #endregion
        #region Loaded事件
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.CommandBindings.Add(
                 new CommandBinding(Window1.AddBookmark,
                    new ExecutedRoutedEventHandler(OnAddBookmark),
                     new CanExecuteRoutedEventHandler(OnNewQuery)));

            this.CommandBindings.Add(
                new CommandBinding(Window1.AddComment,
                            new ExecutedRoutedEventHandler(OnAddComment),
                            new CanExecuteRoutedEventHandler(OnNewQuery)));


            _annotationBuffer = new MemoryStream();
            _annStore = new XmlStreamStore(_annotationBuffer);
            _annServ = new AnnotationService(FDPV);
            
            _annStore.StoreContentChanged +=
                new StoreContentChangedEventHandler(_annStore_StoreContentChanged);
            _annServ.Enable(_annStore);
        }
        #endregion

        private void Window_Closed(object sender, EventArgs e)
        {
            // CloseFile();
            if (_annStore.GetAnnotations().Count > 0)
            {

                _annStore.Flush();
                _annServ.Disable();
                _annotationBuffer.Close();
            }
        }
    }
}
