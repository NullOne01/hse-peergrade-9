using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using TreeWarehouse.Annotations;

namespace TreeWarehouse.Model
{
    public class Folder : INotifyPropertyChanged
    {
        private ObservableCollection<Product> _products = new ObservableCollection<Product>();
        private ObservableCollection<Folder> _subFolders = new ObservableCollection<Folder>();
        private string _name;
        private Folder _parent;

        public Folder(Folder parent)
        {
            Parent = parent;
        }

        public Folder()
        {
            Parent = null;
            //_products = new ObservableCollection<Product>();
            //_subFolders = new ObservableCollection<Folder>();
        }

        public Folder Parent
        {
            get => _parent;
            set
            {
                _parent = value;
                OnPropertyChanged(nameof(Parent));
            }
        }
        
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
        
        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}