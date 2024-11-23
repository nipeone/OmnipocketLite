using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Interactivity;
using System.Diagnostics;
using System.IO;
using Avalonia.Styling;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using System;
using Avalonia.Platform;
using System.Xml.Linq;
using System.Linq;
using Avalonia.Controls.Documents;

namespace OmnipocketLite.AvaloniaMvvm.Views
{
    public partial class SearchResultItem : UserControl
    {
        public SearchResultItem()
        {
            InitializeComponent();
        }

        public void SetContent(string text, string path, string docType)
        {
            SetFormattedText(text);
            this.FilePathButton.Content = path;
            SetDocTypeImage(docType);

            this.FilePathButton.Click += (sender, e) => OpenFileLocation(path);
        }

        private void SetFormattedText(string text)
        {
            this.TextContent.Inlines.Clear();

            try
            {
                var xmlDoc = XElement.Parse("<root>" + text + "</root>");
                foreach (var node in xmlDoc.Nodes())
                {
                    if (node is XText textNode)
                    {
                        this.TextContent.Inlines.Add(new Run(textNode.Value));
                    }
                    else if (node is XElement elementNode)
                    {
                        switch (elementNode.Name.LocalName.ToLower())
                        {
                            //case "bold":
                            //case "b":
                            //    textContent.Inlines.Add(new Bold(new Run(elementNode.Value)));
                            //    break;
                            //case "italic":
                            //case "i":
                            //    textContent.Inlines.Add(new Italic(new Run(elementNode.Value)));
                            //    break;
                            //case "underline":
                            //case "u":
                            //    textContent.Inlines.Add(new Underline(new Run(elementNode.Value)));
                            //    break;
                            case "span":
                                var run = new Run(elementNode.Value);
                                if (elementNode.Attribute("Foreground") != null)
                                {
                                    run.Foreground = new SolidColorBrush(Color.Parse(elementNode.Attribute("Foreground").Value));
                                }
                                this.TextContent.Inlines.Add(run);
                                break;
                            default:
                                this.TextContent.Inlines.Add(new Run(elementNode.Value));
                                break;
                        }
                    }
                }
            }
            catch (System.Xml.XmlException)
            {
                // 如果解析失败，就直接显示原始文本
                this.TextContent.Inlines.Add(new Run(text));
            }
        }

        private void SetDocTypeImage(string docType)
        {
            if (docType == "doc" || docType == "docx")
            {
                string imagePath = $"avares://OmnipocketLite.AvaloniaMvvm/Assets/{docType.ToLower()}_icon.png";
            }

            var uri = new Uri("avares://OmnipocketLite.AvaloniaMvvm/Assets/default_icon.png");
            switch (docType)
            {
                case "doc":
                case "docx":
                    uri = new Uri($"avares://OmnipocketLite.AvaloniaMvvm/Assets/word_icon.png");
                    break;
                case "xls":
                case "xlsx":
                    uri = new Uri($"avares://OmnipocketLite.AvaloniaMvvm/Assets/excel_icon.png");
                    break;
                case "ppt":
                case "pptx":
                    uri = new Uri($"avares://OmnipocketLite.AvaloniaMvvm/Assets/ppt_icon.png");
                    break;
                case "txt":
                    uri = new Uri($"avares://OmnipocketLite.AvaloniaMvvm/Assets/txt_icon.png");
                    break;
                case "pdf":
                    uri = new Uri($"avares://OmnipocketLite.AvaloniaMvvm/Assets/pdf_icon.png");
                    break;
                default:
                    var defaultUri = new Uri("avares://OmnipocketLite.AvaloniaMvvm/Assets/default_icon.png");
                    break;
            }

            this.DocTypeImage.Source = new Bitmap(AssetLoader.Open(uri));
        }

        private void OpenFileLocation(string path)
        {
            try
            {
                if (File.Exists(path))
                {
                    Process.Start("explorer.exe", $"/select,\"{path}\"");
                }
                else if (Directory.Exists(Path.GetDirectoryName(path)))
                {
                    Process.Start("explorer.exe", Path.GetDirectoryName(path));
                }
                else
                {
                    // 如果文件和目录都不存在，可以显示一个错误消息
                    // 这里您可以添加一个错误处理逻辑
                }
            }
            catch (System.Exception ex)
            {
                // 处理可能发生的异常
                // 这里您可以添加一个错误处理逻辑
            }
        }

        public void UpdateColors()
        {
            if (Application.Current.ActualThemeVariant == ThemeVariant.Dark)
            {
                this.TextContent.Foreground = new SolidColorBrush(Color.FromRgb(255, 255, 255)); // 白色
                this.FilePathButton.Foreground = new SolidColorBrush(Color.FromRgb(144, 238, 144)); // 浅绿色
            }
            else
            {
                this.TextContent.Foreground = new SolidColorBrush(Color.FromRgb(0, 0, 0)); // 黑色
                this.FilePathButton.Foreground = new SolidColorBrush(Color.FromRgb(0, 128, 0)); // 绿色
            }
        }
    }
}
