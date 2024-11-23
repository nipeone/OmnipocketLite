using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Platform.Storage;
using Avalonia.Styling;
using System;
using System.Collections.Generic;
using System.IO;

namespace OmnipocketLite.AvaloniaMvvm.Views
{
    public partial class MainWindow : Window
    {
        private bool isBackground = false;
        public MainWindow()
        {
            InitializeComponent();

            //if (OperatingSystem.IsWindows())
            //{
            //    TransparencyLevelHint = new[] { WindowTransparencyLevel.AcrylicBlur };
            //}
            //else if (OperatingSystem.IsMacOS())
            //{
            //    TransparencyLevelHint = new[] { WindowTransparencyLevel.Blur };
            //}
        }

        private void MainWindow_KeyDown(object sender, KeyEventArgs e)
        {
            // 检查是否按下了 Alt+X
            if (e.Key == Key.X && e.KeyModifiers == KeyModifiers.Alt)
            {
                ToggleBackgroundMode();
            }
        }

        private void MainWindow_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.Contains(DataFormats.Files))
            {
                e.DragEffects = DragDropEffects.Copy;
            }
            else
            {
                e.DragEffects = DragDropEffects.None;
            }
        }
        private void MainWindow_OnDrop(object sender, DragEventArgs e)
        {
            if (e.Data.Contains(DataFormats.Files))
            {
                var files = e.Data.GetFiles();
                ProcessDroppedFiles(files);
            }
        }

        private void ProcessDroppedFiles(IEnumerable<IStorageItem> files)
        {
            foreach (var file in files)
            {
                if (Directory.Exists(file.TryGetLocalPath()))
                {
                    // 处理文件夹
                    this.SettingsTextBlock.Text = $"Dropped folder: {file.Path}";
                    // 你可以在这里添加更多处理文件夹的逻辑
                }
                else if (File.Exists(file.TryGetLocalPath()))
                {
                    // 处理文件
                    this.SettingsTextBlock.Text = $"Dropped file: {file.Path}";
                    // 你可以在这里添加更多处理文件的逻辑
                }
            }
        }

        private void ToggleBackgroundMode()
        {
            isBackground = !isBackground;
            if (isBackground)
            {
                this.Hide(); // 隐藏窗口
            }
            else
            {
                this.Show(); // 显示窗口
                this.Activate(); // 激活窗口
            }
        }

        private void SearchBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                PerformSearch();
            }
        }

        private void PerformSearch()
        {
            string searchTerm = this.SearchBox.Text;
            this.ResultsPanel.Children.Clear();

            // 模拟搜索结果
            if (!string.IsNullOrEmpty(searchTerm))
            {
                var searchResults = new List<(string text, string path, string docType)>
            {
                ("这是第1个搜索结果，<Span Foreground=\"Blue\">内容可能</Span>很长很长很长很长很长很长很长<Span Foreground=\"Blue\">很长很长很长很长很长很长很长很长</Span>很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长<Span Foreground=\"Blue\">很长很长很长很长</Span>很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长结束", @"C:\文件1.txt", "txt"),
                ("这是第2个搜索结果，同样内容可能很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长结束", @"D:\文档\文件2.doc", "doc"),
                ("这是第3个搜索结果，依然内容可能很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长结束", @"E:\项目\文件3.pdf", "pdf"),
                ("这是第4个搜索结果，同样内容可能很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长结束", @"D:\文档\文件2.doc", "xls"),
                ("这是第5个搜索结果，同样内容可能很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长结束", @"D:\文档\文件2.doc", "xlsx"),
                ("这是第6个搜索结果，同样内容可能很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长结束", @"D:\文档\文件2.doc", "docx"),
                ("这是第7个搜索结果，同样内容可能很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长结束", @"D:\文档\文件2.doc", "ppt"),
                ("这是第8个搜索结果，同样内容可能很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长结束", @"D:\文档\文件2.doc", "pptx"),
                ("这是第9个搜索结果，同样内容可能很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长结束", @"D:\文档\文件2.doc", "doc"),
                ("这是第10个搜索结果，同样内容可能很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长结束", @"D:\文档\文件2.rtx", "rtx"),
                ("这是第11个搜索结果，同样内容可能很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长结束", @"D:\文档\文件2.doc", "doc"),
                ("这是第12个搜索结果，同样内容可能很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长结束", @"D:\文档\文件2.doc", "doc"),
                ("这是第13个搜索结果，同样内容可能很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长结束", @"D:\文档\文件2.doc", "doc"),
                ("这是第14个搜索结果，同样内容可能很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长结束", @"D:\文档\文件2.doc", "doc"),
                ("这是第15个搜索结果，同样内容可能很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长结束", @"D:\文档\文件2.doc", "doc"),
                ("这是第16个搜索结果，同样内容可能很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长结束", @"D:\文档\文件2.doc", "doc"),
                ("这是第17个搜索结果，同样内容可能很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长结束", @"D:\文档\文件2.doc", "doc"),
                ("这是第18个搜索结果，同样内容可能很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长结束", @"D:\文档\文件2.doc", "doc"),
                ("这是第19个搜索结果，同样内容可能很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长结束", @"D:\文档\文件2.doc", "doc"),
                ("这是第20个搜索结果，同样内容可能很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长结束", @"D:\文档\文件2.doc", "doc"),
                ("这是第21个搜索结果，同样内容可能很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长结束", @"D:\文档\文件2.doc", "doc"),
                ("这是第22个搜索结果，同样内容可能很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长结束", @"D:\文档\文件2.doc", "doc"),
                ("这是第23个搜索结果，同样内容可能很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长结束", @"D:\文档\文件2.doc", "doc"),
                ("这是第12个搜索结果，同样内容可能很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长结束", @"D:\文档\文件2.doc", "doc"),
                ("这是第13个搜索结果，同样内容可能很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长结束", @"D:\文档\文件2.doc", "doc"),
                ("这是第14个搜索结果，同样内容可能很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长结束", @"D:\文档\文件2.doc", "doc"),
                ("这是第15个搜索结果，同样内容可能很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长结束", @"D:\文档\文件2.doc", "doc"),
                ("这是第16个搜索结果，同样内容可能很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长结束", @"D:\文档\文件2.doc", "doc"),
                ("这是第17个搜索结果，同样内容可能很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长结束", @"D:\文档\文件2.doc", "doc"),
                ("这是第18个搜索结果，同样内容可能很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长结束", @"D:\文档\文件2.doc", "doc"),
                ("这是第19个搜索结果，同样内容可能很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长结束", @"D:\文档\文件2.doc", "doc"),
                ("这是第20个搜索结果，同样内容可能很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长结束", @"D:\文档\文件2.doc", "doc"),
                ("这是第21个搜索结果，同样内容可能很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长结束", @"D:\文档\文件2.doc", "doc"),
                ("这是第22个搜索结果，同样内容可能很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长结束", @"D:\文档\文件2.doc", "doc"),
                ("这是第23个搜索结果，同样内容可能很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长结束", @"D:\文档\文件2.doc", "doc"),
                ("这是第12个搜索结果，同样内容可能很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长结束", @"D:\文档\文件2.doc", "doc"),
                ("这是第13个搜索结果，同样内容可能很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长结束", @"D:\文档\文件2.doc", "doc"),
                ("这是第14个搜索结果，同样内容可能很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长结束", @"D:\文档\文件2.doc", "doc"),
                ("这是第15个搜索结果，同样内容可能很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长结束", @"D:\文档\文件2.doc", "doc"),
                ("这是第16个搜索结果，同样内容可能很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长结束", @"D:\文档\文件2.doc", "doc"),
                ("这是第17个搜索结果，同样内容可能很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长结束", @"D:\文档\文件2.doc", "doc"),
                ("这是第18个搜索结果，同样内容可能很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长结束", @"D:\文档\文件2.doc", "doc"),
                ("这是第19个搜索结果，同样内容可能很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长结束", @"D:\文档\文件2.doc", "doc"),
                ("这是第20个搜索结果，同样内容可能很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长结束", @"D:\文档\文件2.doc", "doc"),
                ("这是第21个搜索结果，同样内容可能很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长结束", @"D:\文档\文件2.doc", "doc"),
                ("这是第22个搜索结果，同样内容可能很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长结束", @"D:\文档\文件2.doc", "doc"),
                ("这是第23个搜索结果，同样内容可能很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长结束", @"D:\文档\文件2.doc", "doc"),
                ("这是第12个搜索结果，同样内容可能很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长结束", @"D:\文档\文件2.doc", "doc"),
                ("这是第13个搜索结果，同样内容可能很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长结束", @"D:\文档\文件2.doc", "doc"),
                ("这是第14个搜索结果，同样内容可能很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长结束", @"D:\文档\文件2.doc", "doc"),
                ("这是第15个搜索结果，同样内容可能很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长结束", @"D:\文档\文件2.doc", "doc"),
                ("这是第16个搜索结果，同样内容可能很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长结束", @"D:\文档\文件2.doc", "doc"),
                ("这是第17个搜索结果，同样内容可能很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长结束", @"D:\文档\文件2.doc", "doc"),
                ("这是第18个搜索结果，同样内容可能很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长结束", @"D:\文档\文件2.doc", "doc"),
                ("这是第19个搜索结果，同样内容可能很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长结束", @"D:\文档\文件2.doc", "doc"),
                ("这是第20个搜索结果，同样内容可能很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长结束", @"D:\文档\文件2.doc", "doc"),
                ("这是第21个搜索结果，同样内容可能很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长结束", @"D:\文档\文件2.doc", "doc"),
                ("这是第22个搜索结果，同样内容可能很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长结束", @"D:\文档\文件2.doc", "doc"),
                ("这是第23个搜索结果，同样内容可能很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长结束", @"D:\文档\文件2.doc", "doc"),
                ("这是第12个搜索结果，同样内容可能很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长结束", @"D:\文档\文件2.doc", "doc"),
                ("这是第13个搜索结果，同样内容可能很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长结束", @"D:\文档\文件2.doc", "doc"),
                ("这是第14个搜索结果，同样内容可能很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长结束", @"D:\文档\文件2.doc", "doc"),
                ("这是第15个搜索结果，同样内容可能很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长结束", @"D:\文档\文件2.doc", "doc"),
                ("这是第16个搜索结果，同样内容可能很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长结束", @"D:\文档\文件2.doc", "doc"),
                ("这是第17个搜索结果，同样内容可能很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长结束", @"D:\文档\文件2.doc", "doc"),
                ("这是第18个搜索结果，同样内容可能很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长结束", @"D:\文档\文件2.doc", "doc"),
                ("这是第19个搜索结果，同样内容可能很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长结束", @"D:\文档\文件2.doc", "doc"),
                ("这是第20个搜索结果，同样内容可能很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长结束", @"D:\文档\文件2.doc", "doc"),
                ("这是第21个搜索结果，同样内容可能很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长结束", @"D:\文档\文件2.doc", "doc"),
                ("这是第22个搜索结果，同样内容可能很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长结束", @"D:\文档\文件2.doc", "doc"),
                ("这是第23个搜索结果，同样内容可能很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长结束", @"D:\文档\文件2.doc", "doc"),
            };

                foreach (var (text, path, docType) in searchResults)
                {
                    var resultItem = new SearchResultItem();
                    resultItem.SetContent(text, path, docType);
                    this.ResultsPanel.Children.Add(resultItem);
                }
            }

        }

        private void ThemeToggle_Click(object sender, RoutedEventArgs e)
        {
            if (Application.Current.RequestedThemeVariant == ThemeVariant.Light || Application.Current.RequestedThemeVariant == ThemeVariant.Default)
            {
                Application.Current.RequestedThemeVariant = ThemeVariant.Dark;
                UpdateTheme();
                (sender as Button).Content = "🌛";
            }
            else
            {
                Application.Current.RequestedThemeVariant = ThemeVariant.Light;
                UpdateTheme();
                (sender as Button).Content = "🌞";
            }

        }

        private void MinimizeButton_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void MaximizeRestoreButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.WindowState == WindowState.Normal)
            {
                this.WindowState = WindowState.Maximized;
            }
            else
            {
                this.WindowState = WindowState.Normal;
            }
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private async void SettingsButton_Click(object sender, RoutedEventArgs e)
        {
            this.MainSplitView.IsPaneOpen = !this.MainSplitView.IsPaneOpen;

            var testDocPath = "xxx.doc";
            if (Path.Exists(testDocPath))
            {
                // 弹出消息框
                //var messageBoxStandardWindow = Avalonia.MessageBoxManager
                //    .GetMessageBoxStandardWindow("MessageBox Title", "This is a message box content.");
                //await messageBoxStandardWindow.Show();
            }

        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            PerformSearch();
        }

        private void EnableDragdrop(bool enable)
        {

        }

        private void OnHeaderPointerPressed(object sender, PointerPressedEventArgs e)
        {
            if (e.GetCurrentPoint(this).Properties.IsLeftButtonPressed)
            {
                BeginMoveDrag(e);
            }
        }

        private void UpdateTheme()
        {
            foreach (var child in this.ResultsPanel.Children)
            {
                if (child is SearchResultItem searchResultItem)
                {
                    searchResultItem.UpdateColors();
                }
            }
        }
    }
}