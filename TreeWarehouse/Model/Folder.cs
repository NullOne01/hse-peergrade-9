using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using TreeWarehouse.Annotations;

namespace TreeWarehouse.Model
{
    public class Folder : INotifyPropertyChanged
    {
        private ObservableCollection<Product> _products = new ObservableCollection<Product>();
        private ObservableCollection<Folder> _subFolders = new ObservableCollection<Folder>();

        public ObservableCollection<Product> Products
        {
            get => _products;
            set
            {
                _products = value;
                OnPropertyChanged(nameof(Products));
            }
        }

        public ObservableCollection<Folder> SubFolders
        {
            get => _subFolders;
            set
            {
                _subFolders = value;
                OnPropertyChanged(nameof(SubFolders));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}