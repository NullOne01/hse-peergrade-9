using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using TreeWarehouse.Annotations;

namespace TreeWarehouse.Model
{
    public class Product : INotifyPropertyChanged
    {
        private string _name;
        private string _vendorCode;
        private uint _cost;
        private uint _stockNum;
        private uint _needStockNum;
        private string _description;
        private ProductImage _image;

        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        public string VendorCode
        {
            get => _vendorCode;
            set
            {
                _vendorCode = value;
                OnPropertyChanged(nameof(VendorCode));
            }
        }

        public uint Cost
        {
            get => _cost;
            set
            {
                _cost = value;
                OnPropertyChanged(nameof(Cost));
            }
        }

        public uint StockNum
        {
            get => _stockNum;
            set
            {
                _stockNum = value;
                OnPropertyChanged(nameof(StockNum));
            }
        }

        public uint NeedStockNum
        {
            get => _needStockNum;
            set
            {
                _needStockNum = value;
                OnPropertyChanged(nameof(NeedStockNum));
            }
        }

        public string Description
        {
            get => _description;
            set
            {
                _description = value;
                OnPropertyChanged(nameof(Description));
            }
        }

        public ProductImage Image
        {
            get => _image;
            set
            {
                _image = value;
                OnPropertyChanged(nameof(Image));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}