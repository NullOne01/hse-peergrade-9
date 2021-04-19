using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using TreeWarehouse.ViewModel.Utilities;

namespace TreeWarehouse.Model {
    [DataContract]
    public class Product : INotifyPropertyChanged {
        private string _name;
        private string _vendorCode;
        private uint _cost;
        private uint _stockNum;
        private uint _needStockNum;
        private string _description;
        private ProductImage _image;

        /// <summary>
        /// Generates Product with random property values.
        /// </summary>
        /// <param name="random"> Random object. </param>
        /// <returns> Randomized product. </returns>
        public static Product CreateRandomProduct(Random random) {
            return new Product()
            {
                Name = random.RandomString(10),
                VendorCode = random.RandomString(5),
                Cost = (uint) random.Next(1, 101),
                StockNum = (uint) random.Next(1, 101),
                NeedStockNum = (uint) random.Next(1, 101),
                Description = random.RandomString(15)
            };
        }

        [DataMember]
        public string Name {
            get => _name;
            set {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        [DataMember]
        public string VendorCode {
            get => _vendorCode;
            set {
                _vendorCode = value;
                OnPropertyChanged(nameof(VendorCode));
            }
        }

        [DataMember]
        public uint Cost {
            get => _cost;
            set {
                _cost = value;
                OnPropertyChanged(nameof(Cost));
            }
        }

        [DataMember]
        public uint StockNum {
            get => _stockNum;
            set {
                _stockNum = value;
                OnPropertyChanged(nameof(StockNum));
            }
        }

        [DataMember]
        public uint NeedStockNum {
            get => _needStockNum;
            set {
                _needStockNum = value;
                OnPropertyChanged(nameof(NeedStockNum));
            }
        }

        // Additional functionality.
        [DataMember]
        public string Description {
            get => _description;
            set {
                _description = value;
                OnPropertyChanged(nameof(Description));
            }
        }

        // Additional functionality.
        [DataMember]
        public ProductImage Image {
            get => _image;
            set {
                _image = value;
                OnPropertyChanged(nameof(Image));
            }
        }

        public bool IsStockEnough => StockNum >= NeedStockNum;

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}