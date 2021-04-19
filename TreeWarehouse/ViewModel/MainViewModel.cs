using GalaSoft.MvvmLight.CommandWpf;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Windows;
using TreeWarehouse.Model;

namespace TreeWarehouse.ViewModel {
    public partial class MainViewModel : INotifyPropertyChanged {
        // If CurrentFolder == null, then we don't show it to the user.
        private Folder _currentFolder = null;
        private Warehouse _currentWarehouse = new Warehouse();

        public Folder CurrentFolder {
            get => _currentFolder;
            set {
                _currentFolder = value;
                OnPropertyChanged(nameof(CurrentFolder));
                OnPropertyChanged(nameof(DoProductsShow));
                OnPropertyChanged(nameof(DoSubProductsShow));
            }
        }

        public Warehouse CurrentWarehouse {
            get => _currentWarehouse;
            set {
                _currentWarehouse = value;
                OnPropertyChanged(nameof(CurrentWarehouse));

                // If CurrentWarehouse is changed, then we should forget about previous CurrentFolder.
                CurrentFolder = null;
            }
        }

        /// <summary>
        /// If we can't show products, then we don't show them.
        /// </summary>
        public Visibility DoProductsShow => CurrentFolder != null ? Visibility.Visible : Visibility.Collapsed;

        /// <summary>
        /// If no sub products are found, then don't show them.
        /// </summary>
        public Visibility DoSubProductsShow {
            get {
                if (CurrentFolder != null && CurrentFolder.SubProducts != null && (CurrentFolder.SubProducts.Count > 0))
                    return Visibility.Visible;
                return Visibility.Collapsed;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}