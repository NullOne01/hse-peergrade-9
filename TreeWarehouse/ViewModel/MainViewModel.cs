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
        private Folder _currentFolder = null;
        private Warehouse _currentWarehouse = new Warehouse();

        public Folder CurrentFolder
        {
            get => _currentFolder;
            set
            {
                _currentFolder = value;
                OnPropertyChanged(nameof(CurrentFolder));
                OnPropertyChanged(nameof(DoProductsShow));
                OnPropertyChanged(nameof(DoSubProductsShow));
            }
        }

        public Warehouse CurrentWarehouse
        {
            get => _currentWarehouse;
            set
            {
                _currentWarehouse = value;
                OnPropertyChanged(nameof(CurrentWarehouse));
                
                // If CurrentWarehouse is changed, then we should forget about previous CurrentFolder.
                CurrentFolder = null;
            }
        }

        public Visibility DoProductsShow {
            get {
                if (CurrentFolder != null)
                    return Visibility.Visible;
                else
                    return Visibility.Collapsed;
            }
        }

        public Visibility DoSubProductsShow {
            get {
                if (CurrentFolder != null 
                    && CurrentFolder.SubProducts != null 
                    && (CurrentFolder.SubProducts.Count > 0))
                    return Visibility.Visible;
                else
                    return Visibility.Collapsed;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}