using GalaSoft.MvvmLight.CommandWpf;
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

        public RelayCommand<Folder> RemoveTreeItemCommand
        {
            get
            {
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
                    (warehouse) => { warehouse.Save("test.xml"); });
                return _saveWarehouseCommand;
            }
        }
    }
}