﻿using System;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Printing;
using System.Reflection.Metadata.Ecma335;
using System.Windows;
using System.Windows.Controls;

namespace dotnetCampus.OfficeDocumentZipper
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void OpenOfficeFile_OnClick(object sender, RoutedEventArgs e)
        {
            if (!CheckFileExists())
            {
                return;
            }

            var file = GetFile();

            Process.Start(new ProcessStartInfo("explorer")
            {
                ArgumentList =
                {
                    file
                }
            });
        }

        private void UnZip_OnClick(object sender, RoutedEventArgs e)
        {
            if (!CheckFileExists())
            {
                return;
            }

            if (string.IsNullOrEmpty(OfficeFolder.Text))
            {
                Warn($"OfficeFolder can not be empty");
                return;
            }

            var file = GetFile();

            var directory = OfficeFolder.Text;

            if (Directory.Exists(directory))
            {
                try
                {
                    Directory.Delete(directory, true);
                }
                catch (Exception exception)
                {
                    Warn($"Delete {directory} {exception}");
                    return;
                }
            }

            Directory.CreateDirectory(directory);

            ZipFile.ExtractToDirectory(file, directory, true);

            Warn("");
        }

        private bool CheckFileExists()
        {
            var file = GetFile();

            if (string.IsNullOrEmpty(file))
            {
                Warn($"Office File can not be empty");
                return false;
            }

            if (!File.Exists(file))
            {
                Warn($"Office file {file} not found");
                return false;
            }
            Warn("");

            return true;
        }

        private bool CheckFolderExists()
        {
            if (string.IsNullOrEmpty(OfficeFolder.Text))
            {
                Warn($"OfficeFolder can not be empty");
                return false;
            }

            if (!Directory.Exists(OfficeFolder.Text))
            {
                Warn($"Office Folder {OfficeFolder.Text} not found");
                return false;
            }
            Warn("");

            return true;
        }

        private void Explorer_OnClick(object sender, RoutedEventArgs e)
        {
            if (!CheckFolderExists())
            {
                return;
            }

            Process.Start(new ProcessStartInfo("explorer")
            {
                ArgumentList =
                {
                    OfficeFolder.Text
                }
            });

            Warn("");
        }

        private void Zip_OnClick(object sender, RoutedEventArgs e)
        {
            if (!CheckFolderExists())
            {
                return;
            }

            var file = GetFile();

            if (string.IsNullOrEmpty(file))
            {
                Warn($"Office File can not be empty");
                return;
            }

            if (File.Exists(file))
            {
                file = CreateFileName(file);
            }

            ZipFile.CreateFromDirectory(OfficeFolder.Text, file, CompressionLevel.NoCompression, false);

            OfficeFile.Text = file;

            Warn("");
        }

        private string CreateFileName(string file)
        {
            var name = Path.GetFileNameWithoutExtension(file);
            var extension = Path.GetExtension(file);

            string directory = Path.GetDirectoryName(file)!;

            for (int i = 0; true; i++)
            {
                var fileName = Path.Combine(directory!, $"{name}({i}){extension}");

                if (!File.Exists(fileName))
                {
                    return fileName;
                }
            }
        }

        private void Warn(string text)
        {
            if (Dispatcher.CheckAccess())
            {
                Warning.Text = text;
            }
            else
            {
                Dispatcher.InvokeAsync(() =>
                {
                    Warning.Text = text;
                });
            }
        }

        private void OfficeFile_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            var file = GetFile();

            if (string.IsNullOrEmpty(file))
            {
                return;
            }

            var name = Path.GetFileNameWithoutExtension(file);

            string directory = Path.GetDirectoryName(file) ?? string.Empty;
            directory = Path.Combine(directory, name);

            OfficeFolder.Text = directory;
        }

        private string GetFile()
        {
            var file = OfficeFile.Text;

            if (string.IsNullOrEmpty(file))
            {
                return string.Empty;
            }

            if (file.StartsWith("\""))
            {
                file = file.Substring(1);
            }

            if (file.EndsWith("\""))
            {
                file = file.Remove(file.Length - 1);
            }

            file = Path.GetFullPath(file);
            return file;
        }
    }
}