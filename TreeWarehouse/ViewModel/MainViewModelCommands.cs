using GalaSoft.MvvmLight.CommandWpf;
using Microsoft.Win32;
using System.Windows;
using TreeWarehouse.Model;
using TreeWarehouse.ViewModel.Utilities;

namespace TreeWarehouse.ViewModel
{
    public partial class MainViewModel
    {
        private RelayCommand<Folder> _selectTreeItemCommand;

        public RelayCommand<Folder> SelectTreeItemCommand
        {
            get
            {
                _selectTreeItemCommand = new RelayCommand<Folder>((folder) => { CurrentFolder = folder; });
                return _selectTreeItemCommand;
            }
        }

        private RelayCommand<Folder> _addTreeItemCommand;

        public RelayCommand<Folder> AddTreeItemCommand
        {
            get
            {
                _addTreeItemCommand = new RelayCommand<Folder>((folder) =>
                {
                    folder.SubFolders.Add(new Folder(folder) {Name = "NewFolder"});
                });
                return _addTreeItemCommand;
            }
        }

        private RelayCommand _addRootItemCommand;

        public RelayCommand AddRootItemCommand
        {
            get
            {
                _addRootItemCommand = new RelayCommand(() =>
                {
                    CurrentWarehouse.Folders.Add(new Folder() {Name = "NewRootFolder"});
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
                    (warehouse) => {
                        try {
                            SaveFileDialog saveFileDialog = new SaveFileDialog();
                            saveFileDialog.Filter = "xml file(*.xml)|*.xml";
                            if ((bool)saveFileDialog.ShowDialog()) {
                                warehouse.Save(saveFileDialog.FileName);
                            }
                        } catch {
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
                    () => {
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
    }
}