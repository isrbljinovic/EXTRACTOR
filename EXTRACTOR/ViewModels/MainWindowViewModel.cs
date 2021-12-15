using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using EXTRACTOR.Models;
using EXTRACTOR_Engine.Contracts;
using EXTRACTOR_Engine.Services;
using Microsoft.VisualStudio.PlatformUI;

namespace EXTRACTOR.ViewModels
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        #region Properties

        private DocumentOptions _selectedItem;

        public DocumentOptions SelectedItem
        {
            get { return _selectedItem; }
            set { _selectedItem = value; HandlePropertyChanged(); }
        }

        private List<DocumentOptions> _pdfs;

        public List<DocumentOptions> Pdfs
        {
            get { return _pdfs; }
            set { _pdfs = value; HandlePropertyChanged(); }
        }

        public List<string> Conversions { get; set; } = new List<string>
        {
            "Prebaci u CSV",
            "Prebaci u jednu Excel datoteku",
            "Prebaci u različite Excel datoteke",
            "Generiraj SQL"
        };

        #endregion Properties

        private ICommand _generateCommand;

        public ICommand GenerateCommand
        {
            get { return _generateCommand ?? (_generateCommand = new DelegateCommand(Generate)); }
        }

        private IPythonScriptRunner pythonScriptRunner;

        public MainWindowViewModel()
        {
            Pdfs = new List<DocumentOptions>();
            pythonScriptRunner = new PythonScriptRunner();
        }

        private void Generate()
        {
            foreach (var doc in Pdfs)
            {
                pythonScriptRunner.RunScript(@"C:\Python310\python.exe", $"{doc.DocumentPath} {doc.Name}", doc.Conversion);
            }

            Pdfs = new List<DocumentOptions>();
            SelectedItem = null;
        }

        #region PropertyChangedHandler

        public event PropertyChangedEventHandler PropertyChanged;

        private void HandlePropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion PropertyChangedHandler
    }
}