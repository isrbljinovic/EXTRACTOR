using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using EXTRACTOR.Models;
using EXTRACTOR.Utilities;
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

        private string _progress;

        public string Progress
        {
            get { return _progress; }
            set { _progress = value; HandlePropertyChanged(); }
        }

        #endregion Properties

        #region Commands

        public async Task Generate()
        {
            await Task.Run(() => DoWork());

            Progress = "Generiranje završeno";
            Pdfs = new ObservableCollection<DocumentOptions>();
            SelectedItem = null;
            HandlePropertyChanged();
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
        private IInitializationService initializationService;

        public MainWindowViewModel()
        {
            Pdfs = new ObservableCollection<DocumentOptions>();
            pythonScriptRunner = new PythonScriptRunner();
            initializationService = new InitializationService();
            Init();
        }

        private void Init()
        {
            try
            {
                ApplicationParameters.PythonPath = initializationService.GetPythonPath();
                initializationService.Initialize();
            }
            catch (ApplicationException ae)
            {
                MessageBox.Show("Došlo je do problema prilikom inicijalizacije paketa.\n Molimo pokušajte ponovno.");
            }
        }

        private void DoWork()
        {
            foreach (var doc in Pdfs)
            {
                pythonScriptRunner.RunScript(@"C:\Python310\python.exe", $"{doc.DocumentPath} {doc.Name} {doc.Tables}", ActionsExtensions.GetAction(doc.Conversion));
            }
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