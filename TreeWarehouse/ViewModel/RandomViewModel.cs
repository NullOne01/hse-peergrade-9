using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using TreeWarehouse.Model;

namespace TreeWarehouse.ViewModel
{
    public partial class RandomViewModel : INotifyPropertyChanged
    {
        private Warehouse _currentWarehouse = new Warehouse();
        private uint _productsNum;
        private uint _foldersNum = 1;

        public RandomViewModel(Warehouse currentWarehouse)
        {
            CurrentWarehouse = currentWarehouse;
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

        public uint ProductsNum
        {
            get => _productsNum;
            set
            {
                _productsNum = value;
                OnPropertyChanged(nameof(ProductsNum));
            }
        }

        public uint FoldersNum
        {
            get => _foldersNum;
            set
            {
                _foldersNum = value;
                OnPropertyChanged(nameof(FoldersNum));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}