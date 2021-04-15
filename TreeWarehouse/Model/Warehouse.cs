using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace TreeWarehouse.Model
{
    public class Warehouse : INotifyPropertyChanged
    {
        private ObservableCollection<Folder> _folders = new ObservableCollection<Folder>();

        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<Folder> Folders
        {
            get => _folders;
            set
            {
                _folders = value;
                OnPropertyChanged(nameof(Folders));
            }
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}