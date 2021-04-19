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
        private MainViewModel _mainViewModel;
        private uint _productsNum;
        private uint _foldersNum = 1;

        public RandomViewModel(MainViewModel mainViewModel)
        {
            MainViewModel = mainViewModel;
        }

        public MainViewModel MainViewModel {
            get => _mainViewModel;
            set
            {
                _mainViewModel = value;
                OnPropertyChanged(nameof(MainViewModel));
            }
        }

        /// <summary>
        /// Number of products to randomly generate.
        /// </summary>
        public uint ProductsNum
        {
            get => _productsNum;
            set
            {
                _productsNum = value;
                OnPropertyChanged(nameof(ProductsNum));
            }
        }

        /// <summary>
        /// Number of folders to randomly generate.
        /// </summary>
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