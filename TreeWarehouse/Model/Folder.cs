using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using TreeWarehouse.ViewModel.Utilities;

namespace TreeWarehouse.Model
{
    [DataContract(IsReference = true)]
    public class Folder : INotifyPropertyChanged
    {
        private ObservableCollection<Product> _products = new ObservableCollection<Product>();
        private ObservableCollection<Folder> _subFolders = new ObservableCollection<Folder>();
        private string _name;
        private Folder _parent;
        private int _priority;

        public Folder(Folder parent)
        {
            Parent = parent;
        }

        public Folder()
        {
            Parent = null;
        }

        [DataMember]
        public Folder Parent
        {
            get => _parent;
            set
            {
                _parent = value;
                OnPropertyChanged(nameof(Parent));
            }
        }

        [DataMember]
        public ObservableCollection<Product> Products
        {
            get => _products;
            set
            {
                _products = value;
                OnPropertyChanged(nameof(Products));
            }
        }

        [DataMember]
        public ObservableCollection<Folder> SubFolders
        {
            get => _subFolders;
            set
            {
                _subFolders = value;
                OnPropertyChanged(nameof(SubFolders));
            }
        }

        [DataMember]
        public string Name
        {
            get => _name;
            set
            {
                if (Parent != null && Parent.SubFolders.Any((folder) => folder.Name == value))
                {
                    Name = value + "1";
                    OnPropertyChanged(nameof(Name));
                    return;
                }

                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        [DataMember]
        public int Priority
        {
            get => _priority;
            set
            {
                _priority = value;
                OnPropertyChanged(nameof(Priority));
            }
        }
        
        public static Folder CreateRandomFolder(Random random, Folder parent)
        {
            return new Folder(parent)
            {
                Name = random.RandomString(5),
                Priority = random.Next(-5, 6)
            };
        }

        public string GetPath() {
            string path = Name;
            Folder nextParent = Parent;
            // Until root folder is found.
            while (nextParent != null && nextParent.Parent != null) {
                path = nextParent.Name + "/" + path;
                nextParent = nextParent.Parent;
            }

            return path;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}