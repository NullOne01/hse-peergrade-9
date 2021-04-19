using GalaSoft.MvvmLight.CommandWpf;
using Microsoft.Win32;
using System.Windows;
using TreeWarehouse.Model;
using TreeWarehouse.View;
using TreeWarehouse.ViewModel.Utilities;

namespace TreeWarehouse.ViewModel
{
    public partial class MainViewModel
    {
        private RelayCommand<Folder> _selectTreeItemCommand;

        public RelayCommand<Folder> SelectTreeItemCommand {
            get {
                _selectTreeItemCommand = new RelayCommand<Folder>((folder) => { CurrentFolder = folder; });
                return _selectTreeItemCommand;
            }
        }

        private RelayCommand<Folder> _addTreeItemCommand;

        public RelayCommand<Folder> AddTreeItemCommand {
            get {
                _addTreeItemCommand = new RelayCommand<Folder>((folder) =>
                {
                    folder.SubFolders.Add(new Folder(folder) { Name = "NewFolder" });
                });
                return _addTreeItemCommand;
            }
        }

        private RelayCommand _addRootItemCommand;

        public RelayCommand AddRootItemCommand {
            get {
                _addRootItemCommand = new RelayCommand(() =>
                {
                    CurrentWarehouse.RootFolder.SubFolders.Add(new Folder(CurrentWarehouse.RootFolder) { Name = "NewRootFolder" });
                });
                return _addRootItemCommand;
            }
        }

        private RelayCommand<Folder> _removeTreeItemCommand;

        public RelayCommand<Folder> RemoveTreeItemCommand {
            get {
                _removeTreeItemCommand = new RelayCommand<Folder>(
                    (folder) => { CurrentWarehouse.ForceDeleteFolder(folder); },
                    (folder) => folder != null && folder.Products.Count <= 0 && folder.SubFolders.Count <= 0);
                return _removeTreeItemCommand;
            }
        }

        private RelayCommand<Warehouse> _saveWarehouseCommand;

        public RelayCommand<Warehouse> SaveWarehouseCommand {
            get {
                _saveWarehouseCommand = new RelayCommand<Warehouse>(
                    (warehouse) =>
                    {
                        try {
                            SaveFileDialog saveFileDialog = new SaveFileDialog();
                            saveFileDialog.Filter = "xml file(*.xml)|*.xml";
                            if ((bool)saveFileDialog.ShowDialog()) {
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

        public RelayCommand LoadWarehouseCommand {
            get {
                _loadWarehouseCommand = new RelayCommand(
                    () =>
                    {
                        try {
                            OpenFileDialog openFileDialog = new OpenFileDialog();
                            openFileDialog.Filter = "xml file(*.xml)|*.xml";
                            if ((bool)openFileDialog.ShowDialog()) {
                                CurrentWarehouse = SaveLoadWarehouse.Load(openFileDialog.FileName);
                                CurrentFolder = null;
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

        public RelayCommand<Warehouse> SaveReportCSVCommand {
            get {
                _saveReportCSVCommand = new RelayCommand<Warehouse>(
                    (warehouse) =>
                    {
                        try {
                            SaveFileDialog saveFileDialog = new SaveFileDialog();
                            saveFileDialog.Filter = "csv file(*.csv)|*.csv";
                            if ((bool)saveFileDialog.ShowDialog()) {
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

        public RelayCommand<Warehouse> OnAppCloseCommand {
            get {
                _onAppCloseCommand = new RelayCommand<Warehouse>(
                    (warehouse) =>
                    {
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

        public RelayCommand OnAppLoadCommand {
            get {
                _onAppLoadCommand = new RelayCommand(
                    () =>
                    {
                        try {
                            CurrentWarehouse = SaveLoadWarehouse.Load("last_session.xml");
                        }
                        catch {
                            CurrentWarehouse = new Warehouse();
                        }
                    });
                return _onAppLoadCommand;
            }
        }
        
        private RelayCommand _openRandomWindowCommand;

        public RelayCommand OpenRandomWindowCommand {
            get {
                _openRandomWindowCommand = new RelayCommand(
                    () =>
                    {
                        RandomWindow randomWindow = new RandomWindow(this);
                        randomWindow.ShowDialog();
                    });
                return _openRandomWindowCommand;
            }
        }
    }
}