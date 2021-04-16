using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace TreeWarehouse.Model
{
    public class Warehouse : INotifyPropertyChanged
    {
        private ObservableCollection<Folder> _folders = new ObservableCollection<Folder>();

        public Warehouse()
        {
            //_folders = new ObservableCollection<Folder>();
        }
        
        public ObservableCollection<Folder> Folders
        {
            get => _folders;
            set
            {
                _folders = value;
                OnPropertyChanged(nameof(Folders));
            }
        }

        public bool ForceDeleteFolder(Folder folderToRemove)
        {
            if (folderToRemove.Parent != null)
            {
                return folderToRemove.Parent.SubFolders.Remove(folderToRemove);
            }

            // If folder is in root (no parent), then delete from root.
            return Folders.Remove(folderToRemove);
        }

        /*public bool ForceDeleteFolder(Folder folderToRemove)
        {
            if (Folders.Count <= 0)
                return false;
            var bfsQueue = new Queue<Folder>();
            foreach (var folder in Folders)
            {
                bfsQueue.Enqueue(folder);
                if (folder == folderToRemove)
                {
                    return Folders.Remove(folderToRemove);
                }
            }

            while (bfsQueue.Count > 0)
            {
                Folder newFolder = bfsQueue.Dequeue();
                if (newFolder.SubFolders.Contains(folderToRemove))
                {
                    return newFolder.SubFolders.Remove(folderToRemove);
                }

                foreach (var newSubFolder in newFolder.SubFolders)
                {
                    bfsQueue.Enqueue(newSubFolder);
                }
            }

            return false;
        }*/
        
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}