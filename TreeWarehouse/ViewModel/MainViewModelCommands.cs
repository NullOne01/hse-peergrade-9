using GalaSoft.MvvmLight.CommandWpf;
using Microsoft.Win32;
using System.Windows;
using TreeWarehouse.Model;
using TreeWarehouse.View;
using TreeWarehouse.ViewModel.Utilities;

namespace TreeWarehouse.ViewModel {
    public partial class MainViewModel {
        // File with commands. Commands are used instead of Events in MVVM. \\

        private RelayCommand<Folder> _selectTreeItemCommand;

        /// <summary>
        /// Command to select folder as CurrentFolder.
        /// </summary>
        public RelayCommand<Folder> SelectTreeItemCommand {
            get {
                _selectTreeItemCommand = new RelayCommand<Folder>((folder) => { CurrentFolder = folder; });
                return _selectTreeItemCommand;
            }
        }

        private RelayCommand<Folder> _addTreeItemCommand;

        /// <summary>
        /// Command to add folder as child to another folder.
        /// </summary>
        public RelayCommand<Folder> AddTreeItemCommand {
            get {
                _addTreeItemCommand = new RelayCommand<Folder>((folder) => {
                    folder.SubFolders.Add(new Folder(folder) {Name = "NewFolder"});
                });
                return _addTreeItemCommand;
            }
        }

        private RelayCommand _addRootItemCommand;

        /// <summary>
        /// Command to add folder to the root folder.
        /// </summary>
        public RelayCommand AddRootItemCommand {
            get {
                _addRootItemCommand = new RelayCommand(() => {
                    CurrentWarehouse.RootFolder.SubFolders.Add(new Folder(CurrentWarehouse.RootFolder)
                        {Name = "NewRootFolder"});
                });
                return _addRootItemCommand;
            }
        }

        private RelayCommand<Folder> _removeTreeItemCommand;

        /// <summary>
        /// Command to remove folder. Command is working if it is possible.
        /// </summary>
        public RelayCommand<Folder> RemoveTreeItemCommand {
            get {
                // Command is checking if we can delete the folder.
                _removeTreeItemCommand = new RelayCommand<Folder>(
                    (folder) => { CurrentWarehouse.ForceDeleteFolder(folder); },
                    (folder) => folder != null && folder.Products.Count <= 0 && folder.SubFolders.Count <= 0);
                return _removeTreeItemCommand;
            }
        }

        private RelayCommand<Warehouse> _saveWarehouseCommand;

        /// <summary>
        /// Command to open SaveFileDialog and save warehouse in XML format.
        /// </summary>
        public RelayCommand<Warehouse> SaveWarehouseCommand {
            get {
                // Dialogues are bad for MVVM here, but no shit's given.
                _saveWarehouseCommand = new RelayCommand<Warehouse>(
                    (warehouse) => {
                        try {
                            SaveFileDialog saveFileDialog = new SaveFileDialog();
                            saveFileDialog.Filter = "xml file(*.xml)|*.xml";
                            if ((bool) saveFileDialog.ShowDialog()) {
                                warehouse.Save(saveFileDialog.FileName);
                            }
                        }
                        catch {
                            MessageBox.Show("Не получилось сохранить :c");
                        }
                    });
                return _saveWarehouseCommand;
            }
        }

        private RelayCommand _loadWarehouseCommand;

        /// <summary>
        /// Command to open OpenFileDialog and load warehouse from XML format.
        /// </summary>
        public RelayCommand LoadWarehouseCommand {
            get {
                // Dialogues are bad for MVVM here, but no shit's given.
                _loadWarehouseCommand = new RelayCommand(
                    () => {
                        try {
                            OpenFileDialog openFileDialog = new OpenFileDialog();
                            openFileDialog.Filter = "xml file(*.xml)|*.xml";
                            if ((bool) openFileDialog.ShowDialog()) {
                                CurrentWarehouse = SaveLoadWarehouse.Load(openFileDialog.FileName);
                            }
                        }
                        catch {
                            MessageBox.Show("Не получилось загрузить :c");
                        }
                    });
                return _loadWarehouseCommand;
            }
        }

        private RelayCommand<Warehouse> _saveReportCSVCommand;

        /// <summary>
        /// Command to open SaveFileDialog and save report in CSV format.
        /// </summary>
        public RelayCommand<Warehouse> SaveReportCSVCommand {
            get {
                // Dialogues are bad for MVVM here, but no shit's given.
                _saveReportCSVCommand = new RelayCommand<Warehouse>(
                    (warehouse) => {
                        try {
                            SaveFileDialog saveFileDialog = new SaveFileDialog();
                            saveFileDialog.Filter = "csv file(*.csv)|*.csv";
                            if ((bool) saveFileDialog.ShowDialog()) {
                                warehouse.SaveReportCSV(saveFileDialog.FileName);
                            }
                        }
                        catch {
                            MessageBox.Show("Не получилось сохранить :c");
                        }
                    });
                return _saveReportCSVCommand;
            }
        }

        private RelayCommand<Warehouse> _onAppCloseCommand;

        /// <summary>
        /// Command to save last session, when app is closing.
        /// </summary>
        public RelayCommand<Warehouse> OnAppCloseCommand {
            get {
                _onAppCloseCommand = new RelayCommand<Warehouse>(
                    (warehouse) => {
                        try {
                            warehouse.Save("last_session.xml");
                        }
                        catch {
                            MessageBox.Show("Не получилось сохранить прогресс");
                        }
                    });
                return _onAppCloseCommand;
            }
        }

        private RelayCommand _onAppLoadCommand;

        /// <summary>
        /// Command to load last session on program start.
        /// </summary>
        public RelayCommand OnAppLoadCommand {
            get {
                _onAppLoadCommand = new RelayCommand(
                    () => {
                        try {
                            CurrentWarehouse = SaveLoadWarehouse.Load("last_session.xml");
                        }
                        catch {
                            // If can't load last session, then just load empty warehouse.
                            CurrentWarehouse = new Warehouse();
                        }
                    });
                return _onAppLoadCommand;
            }
        }

        private RelayCommand _openRandomWindowCommand;

        /// <summary>
        /// Command to open RandomWindow.
        /// </summary>
        public RelayCommand OpenRandomWindowCommand {
            get {
                _openRandomWindowCommand = new RelayCommand(
                    () => {
                        RandomWindow randomWindow = new RandomWindow(this);
                        randomWindow.ShowDialog();
                    });
                return _openRandomWindowCommand;
            }
        }

        private RelayCommand _openHelpWindowCommand;

        /// <summary>
        /// Command to open HelpWindow.
        /// </summary>
        public RelayCommand OpenHelpWindowCommand {
            get {
                _openHelpWindowCommand = new RelayCommand(
                    () => {
                        HelpWindow helpWindow = new HelpWindow();
                        helpWindow.Show();
                    });
                return _openHelpWindowCommand;
            }
        }
    }
}