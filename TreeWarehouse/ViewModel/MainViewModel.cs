using GalaSoft.MvvmLight.CommandWpf;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Windows;
using TreeWarehouse.Model;

namespace TreeWarehouse.ViewModel
{
    public partial class MainViewModel : INotifyPropertyChanged
    {
        private Folder _currentFolder = new Folder();
        private Warehouse _currentWarehouse = new Warehouse();

        public Folder CurrentFolder
        {
            get => _currentFolder;
            set
            {
                _currentFolder = value;
                OnPropertyChanged(nameof(CurrentFolder));
            }
        }

        public Warehouse CurrentWarehouse
        {
            get => _currentWarehouse;
            set
            {
                _currentWarehouse = value;
                OnPropertyChanged(nameof(CurrentWarehouse));
            }
        }

        public MainViewModel()
        {
            _currentFolder.Products.Add(new Product() {Name = "Gay", Cost = 300, StockNum = 1});
            _currentFolder.Name = "Test1";
                        
            CurrentWarehouse.Folders.Add(CurrentFolder);
            
            Folder subFolder = new Folder(_currentFolder) {Name = "Test2"};
            _currentFolder.SubFolders.Add(subFolder);

            Folder subFolder2 = new Folder() {Name = "Test3"};
            CurrentWarehouse.Folders.Add(subFolder2);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}