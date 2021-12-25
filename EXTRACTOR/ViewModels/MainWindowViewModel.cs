using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using EXTRACTOR.Models;
using EXTRACTOR_Engine.Contracts;
using EXTRACTOR_Engine.Enums;
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

        private ObservableCollection<DocumentOptions> _pdfs;

        public ObservableCollection<DocumentOptions> Pdfs
        {
            get { return _pdfs; }
            set { _pdfs = value; HandlePropertyChanged(); }
        }

        #endregion Properties

        #region Commands

        private ICommand _generateCommand;

        public ICommand GenerateCommand
        {
            get { return _generateCommand ?? (_generateCommand = new DelegateCommand(Generate)); }
        }

        private void Generate()
        {
            foreach (var doc in Pdfs)
            {
                pythonScriptRunner.RunScript(@"C:\Python310\python.exe", $"{doc.DocumentPath} {doc.Name} {doc.Tables}", ActionsExtensions.GetAction(doc.Conversion));
            }

            Pdfs = new ObservableCollection<DocumentOptions>();
            SelectedItem = null;
        }

        private ICommand _deleteCommand;

        public ICommand DeleteCommand
        {
            get { return _deleteCommand ?? (_deleteCommand = new DelegateCommand(Delete)); }
        }

        private void Delete()
        {
            if (SelectedItem != null)
            {
                _pdfs.Remove(SelectedItem);
                SelectedItem = null;
                HandlePropertyChanged("Pdfs");
            }
        }

        #endregion Commands

        private IPythonScriptRunner pythonScriptRunner;

        public MainWindowViewModel()
        {
            Pdfs = new ObservableCollection<DocumentOptions>();
            pythonScriptRunner = new PythonScriptRunner();
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