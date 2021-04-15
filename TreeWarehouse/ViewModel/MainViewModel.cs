using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using TreeWarehouse.Model;

namespace TreeWarehouse.ViewModel
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private Folder _currentFolder = new Folder();

        public Folder CurrentFolder
        {
            get => _currentFolder;
            set
            {
                _currentFolder = value;
                OnPropertyChanged(nameof(CurrentFolder));
            }
        }

        public MainViewModel()
        {
            _currentFolder.Products.Add(new Product() {Name = "Gay"});
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}