using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using EXTRACTOR.Models;
using EXTRACTOR.ViewModels;
using Microsoft.Win32;

namespace EXTRACTOR
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MainWindowViewModel _vm;

        public MainWindow()
        {
            InitializeComponent();
            _vm = new MainWindowViewModel();
            this.DataContext = _vm;
        }

        private void AddFilesClicked(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Multiselect = true;
            openFileDialog.Filter = "Text files (*.pdf)|*.pdf|All files (*.*)|*.*";
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            var newList = _vm.Pdfs;
            if (openFileDialog.ShowDialog() == true)
            {
                foreach (string filename in openFileDialog.FileNames)
                    newList.Add(new DocumentOptions { Name = Path.GetFileName(filename), DocumentPath = filename });
            }
            _vm.Pdfs = new List<DocumentOptions>(newList);
        }
    }
}